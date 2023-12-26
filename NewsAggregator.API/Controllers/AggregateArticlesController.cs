using Hangfire;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core;
using WebApp.Services.Interfaces;

namespace WebApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AggregateArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public AggregateArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost]
        public async Task<IActionResult> Aggregate(Guid sourceId)
        {
            //var data = await _articleService.AggregateDataFromRssByArticleSourceId(sourceId);

            //var existedArticles = await _articleService.GetExistedArticlesUrls();

            //var uniqueArticles = data
            //    .Where(dto => !existedArticles
            //        .Any(url => dto.SourceUrl.Equals(url))).ToArray();
            //var listFulfilledArticles = new List<ArticleDto>();

            //foreach (var articleDto in uniqueArticles)
            //{
            //    var fulFilledArticle = await _articleService.GetArticleByUrl(articleDto.SourceUrl, articleDto);
            //    if (fulFilledArticle != null)
            //    {
            //        listFulfilledArticles.Add(fulFilledArticle);
            //    }
            //}

            //await _articleService.ParseArticleText(listFulfilledArticles);

            //await _articleService.RateUnratedArticles();

            RecurringJob.AddOrUpdate("Article Rss Aggregation",
                ()=> _articleService.AggregateArticlesFromRssByArticleSourceId(sourceId),
                "0 0/3 * * *");

            RecurringJob.AddOrUpdate("Web page scrapping",
                () => _articleService.ParseArticleText(),
                "15 0/3 * * *");

            RecurringJob.AddOrUpdate("Article Rating",
                () => _articleService.RateBatchOfUnratedArticles(),
                "0/2 * * * *"); 

            return Ok();
        }

    }
}
