using HtmlAgilityPack;
using System.Reflection;
using System.ServiceModel.Syndication;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using WebApp.Core;
using WebApp.Data.Entities;
using WebApp.Repositories;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ArticleDto[]?> AggregateDataFromRssByArticleSourceId(Guid sourceId)
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


   
        public async Task<ArticleDto?> GetArticleByUrl(string url, ArticleDto dto)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);


            var textNode = doc.DocumentNode.SelectSingleNode("//div[@class=\"news-text\"]");
            var newsRefs = textNode.SelectSingleNode("//div[@class=\"news-reference\"]");
            textNode.RemoveChild(newsRefs);
            var textValue = textNode.InnerHtml;
            //var firstSymbolOutFromArticle = textValue.IndexOf("<div id=\"news-text-end\"></div>");
            //if (firstSymbolOutFromArticle)
            //{
                
            //}
            //textValue= textValue.Remove(firstSymbolOutFromArticle);

            dto.Text = textValue;

            return dto;
        }

        public async Task<string[]> GetExistedArticlesUrls()
        {
            var existedArticlesUrls = await _unitOfWork.ArticleRepository.GetAsQueryable()
                .Select(article => article.SourceUrl).ToArrayAsync();
            return existedArticlesUrls;
        }

        public async Task<int> InsertParsedArticles(List<ArticleDto> listFulfilledArticles)
        {
            var articles = listFulfilledArticles.Select(dto => new Article()
            {
                Id = dto.Id,
                Date = dto.Date,
                Text = dto.Text,
                Title = dto.Title,
                Description = dto.Description,
                ArticleSourceId = dto.ArticleSourceId,
                SourceUrl = dto.SourceUrl,
            }).ToArray();

            await _unitOfWork.ArticleRepository.InsertMany(articles);
            return await _unitOfWork.Commit();
        }

    }
}