using System.Net.Http.Json;
using HtmlAgilityPack;
using System.ServiceModel.Syndication;
using System.Xml;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebApp.Core;
using WebApp.Data.CQS.Commands;
using WebApp.Data.CQS.Queries;
using WebApp.Mappers;
using WebApp.Repositories;
using WebApp.Services.Interfaces;
using System.Text.RegularExpressions;

namespace WebApp.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ArticleMapper _articleMapper;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public ArticleService(IUnitOfWork unitOfWork, 
            ArticleMapper articleMapper, IMediator mediator, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _articleMapper = articleMapper;
            _mediator = mediator;
            _configuration = configuration;
        }

        private async Task<ArticleDto[]?> AggregateDataFromRssByArticleSourceId(Guid sourceId)
        {
            var articleSourceRss = (await _unitOfWork.ArticleSourceRepository.GetById(sourceId))?.RssUrl;
            if (string.IsNullOrWhiteSpace(articleSourceRss)) return null;
            using (var reader = XmlReader.Create(articleSourceRss))
            {
                var feed = SyndicationFeed.Load(reader);
                var rssArticles = feed.Items.Select(item => new ArticleDto()
                {
                    Id = Guid.NewGuid(),
                    ArticleSourceId = sourceId,
                    Date = item.PublishDate.UtcDateTime,
                    Title = item.Title.Text,
                    Description = item.Summary.Text,
                    SourceUrl = item.Id
                }).ToArray();
                return rssArticles;
            }
        }

        public async Task AggregateArticlesFromRssByArticleSourceId(Guid sourceId)
        {
            var data = await AggregateDataFromRssByArticleSourceId(sourceId);

            var existedArticles = await GetExistedArticlesUrls();

            var uniqueArticles = data
                .Where(dto => !existedArticles
                    .Any(url => dto.SourceUrl.Equals(url))).ToArray();

            var command = new InsertRssDataCommand()
            {
                Articles = uniqueArticles
            };
            await _mediator.Send(command);
        }

        public async Task<ArticleDto?> GetArticleById(Guid id)
        {
            var articleDto = _articleMapper.ArticleToArticleDto(
                await _mediator.Send(new GetArticlesByIdQuery { Id = id }));

            return articleDto;
            //var article = (await _unitOfWork.ArticleRepository.GetById(id));
            //return article != null
            //    ? _articleMapper.ArticleToArticleDto(article)
            //    : null;
        }
        
        public async Task<ArticleDto[]?> GetArticlesByName(string name)
        {
            var articles = await _unitOfWork.ArticleRepository
                .FindBy(article => EF.Functions.Like(article.Title, $"%{name}"))
                .Select(article => _articleMapper.ArticleToArticleDto(article))
                .ToArrayAsync();
            return articles;
        }

        public async Task<ArticleDto[]?> GetPositive()
        {
            var articles = await _unitOfWork.ArticleRepository
                .GetAsQueryable()
                //.FindBy(article => article.Rate>=0)
                .Select(article => _articleMapper.ArticleToArticleDto(article))
                .ToArrayAsync();
            return articles;
        }

        public async Task DeleteArticle(Guid id)
        {
            await _unitOfWork.ArticleRepository.DeleteById(id);
            await _unitOfWork.Commit();
        }

        public async Task<Guid> CreateArticle(ArticleDto dto)
        {
            var command = new AddArticleCommand() { Article = dto };
            var id = command.Article.Id;
            await _mediator.Send(command);
            return id;
        }

        public async Task CreateArticleAndSource(ArticleDto articleDto, SourceDto? sourceDto)
        {
            throw new NotImplementedException();
        }

        public async Task RateBatchOfUnratedArticles()
        {
            var ids = await _mediator.Send(new GetUnratedArticleIdsQuery());
            foreach (var id in ids)
            {
                await Rate(id);
            }
        }

        private async Task Rate(Guid id)
        {
            //get text
            var text = await _mediator.Send(new GetArticleTextQuery() { Id = id });

            //get dictionary of keywords
            var dictionary = _configuration.GetSection("Dictionary").GetChildren()
                .ToDictionary(section => section.Key, section => Convert.ToInt32(section.Value));

            //prepare(?) text
            //remove html(optional)
            var textWithoutHtml = RemoveHTMLTags(text);
            var lemmas = await GetLemmas(textWithoutHtml);

            int? rate = null;
            
            foreach (var lemma in lemmas)
            {
                
                if (dictionary.TryGetValue(lemma, out var value))
                {
                    if (rate == null)
                    {
                        rate = value;
                    }
                    else 
                        rate += value;
                }
            }

            await _mediator.Send(new SetArticleRateCommand() { Id = id, Rate = rate });
            // find correct form
        }

        private string RemoveHTMLTags(string html)
        {
            return Regex.Replace(html, "<.*?>", string.Empty);
        }

        private async Task<string[]> GetLemmas(string text)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post,
                    $"http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey={_configuration["AppSettings:IsprasKey"]}");

                request.Headers.Add("Accept", "application/json");

                request.Content = JsonContent.Create(new[]
                {
                    new { Text = text }
                });

                var response = await httpClient.SendAsync(request);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var lemmas = JsonConvert.DeserializeObject<IsprasLemmatizationResponse[]>(responseString)?
                        .FirstOrDefault()?
                        .Annotations
                        .Lemma
                        .Select(l => l.Value)
                        .ToArray();

                    return lemmas;
                }
                return new string[] {  };
            }
        }

        private async Task<string[]> GetLemmas(string text, IsprasKeys key)
        {
            using (var httpClient = new HttpClient())
            {
                //depends on key - diff url
                var request = new HttpRequestMessage(HttpMethod.Post,
                    $"http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey={_configuration["AppSettings:IsprasKey"]}");

                request.Headers.Add("Accept", "application/json");

                request.Content = JsonContent.Create(new[]
                {
                    new { Text = text }
                });

                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var lemmas = JsonConvert.DeserializeObject<IsprasLemmatizationResponse[]>(responseString)?
                        .FirstOrDefault()?
                        .Annotations
                        .Lemma
                        .Select(l => l.Value)
                        .ToArray();

                    return lemmas;
                }
                //else try with another key
                return new string[] { };
            }
        }

        private (Guid, string) GetArticleTextByUrl((Guid, string) articleInfo)
        {
            var web = new HtmlWeb();
            var doc = web.Load(articleInfo.Item2);

            var textNode = doc.DocumentNode.SelectSingleNode("//div[@class=\"news-text\"]");
            var newsRefs = textNode.SelectSingleNode("//div[@class=\"news-reference\"]");
            textNode.RemoveChild(newsRefs);
            var textValue = textNode.InnerHtml;

            //var firstSymbolOutFromArticle = textValue.IndexOf("<div id=\"news-text-end\"></div>");
            //if (firstSymbolOutFromArticle)
            //{
                
            //}
            //textValue= textValue.Remove(firstSymbolOutFromArticle);

            var tuple = (articleInfo.Item1,  textValue);
            return tuple;
        }

        private async Task<string[]> GetExistedArticlesUrls()
        {
            var existedArticlesUrls = await _unitOfWork.ArticleRepository.GetAsQueryable()
                .Select(article => article.SourceUrl).ToArrayAsync();
            return existedArticlesUrls;
        }

        public async Task ParseArticleText()
        {
            var articlesWithoutText = await _mediator
                .Send(new GetArticlesWithoutTextQuery());

            var dict = new Dictionary<Guid, string>(); //need to be check is threadsafe 
            var result = Parallel.ForEach(articlesWithoutText, tuple =>
            {
                var data = GetArticleTextByUrl(tuple);
                dict.Add(data.Item1, data.Item2);
            }); 


            await _mediator.Send(new UpdateArticlesText() { ArticlesData = dict });
        }

    }

    internal enum IsprasKeys
    { Key1,
        Key2,
        Key3
    }
}