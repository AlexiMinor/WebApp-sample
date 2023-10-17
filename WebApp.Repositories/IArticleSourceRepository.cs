using WebApp.Data.Entities;

namespace WebApp.Repositories;

public interface IArticleSourceRepository
{
    Task<ArticleSource?> GetById(Guid id);
    Task<List<ArticleSource?>> Get();
    IQueryable<ArticleSource?> GetAsQueryable();

    Task InsertArticles(IEnumerable<ArticleSource?> articles);
    Task InsertArticle(ArticleSource article);

    Task DeleteById(Guid id);
    Task DeleteArticles(IEnumerable<ArticleSource> articles);
    
}