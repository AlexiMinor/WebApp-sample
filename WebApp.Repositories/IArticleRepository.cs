using WebApp.Data.Entities;

namespace WebApp.Repositories;

public interface IArticleRepository
{
    Task<List<Article?>> GetArticles();
     IQueryable<Article?> GetArticlesWithSource();
    Task InsertArticles(IEnumerable<Article?> articles);
    Task<Article?> GetById(Guid id);
    Task<int> Commit();
}