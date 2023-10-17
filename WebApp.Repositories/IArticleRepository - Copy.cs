using WebApp.Data.Entities;

namespace WebApp.Repositories;

public interface IArticleSourceRepository
{
  
    Task<ArticleSource?> GetById(Guid id);
    Task<List<ArticleSource>> GetAll();
    IQueryable<ArticleSource> GetAsQueryable();
    Task<int> Commit();
}