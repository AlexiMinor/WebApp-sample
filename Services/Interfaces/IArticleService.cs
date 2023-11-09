using WebApp.Core;

namespace WebApp.Services.Interfaces;

public interface IArticleService
{
    public Task<ArticleDto[]?> AggregateDataFromRssByArticleSourceId(Guid sourceId);
    public Task<string[]> GetExistedArticlesUrls();
    public Task<ArticleDto?> GetArticleByUrl(string url, ArticleDto dto);
    public Task<int> InsertParsedArticles(List<ArticleDto> listFulfilledArticles);
}