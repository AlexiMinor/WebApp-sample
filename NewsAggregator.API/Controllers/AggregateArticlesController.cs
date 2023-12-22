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
            var data = await _articleService.AggregateDataFromRssByArticleSourceId(sourceId);

            var existedArticles = await _articleService.GetExistedArticlesUrls();

            var uniqueArticles = data
                .Where(dto => !existedArticles
                    .Any(url => dto.SourceUrl.Equals(url))).ToArray();
            var listFulfilledArticles = new List<ArticleDto>();

            foreach (var articleDto in uniqueArticles)
            {
                var fulFilledArticle = await _articleService.GetArticleByUrl(articleDto.SourceUrl, articleDto);
                if (fulFilledArticle != null)
                {
                    listFulfilledArticles.Add(fulFilledArticle);
                }
            }

            await _articleService.InsertParsedArticles(listFulfilledArticles);

            await _articleService.RateUnratedArticles();

            return Ok();
        }

    }
}
