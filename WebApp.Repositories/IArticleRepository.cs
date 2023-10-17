using WebApp.Data.Entities;

namespace WebApp.Repositories;

public interface IArticleRepository : IRepository<Article>
{
    Task<List<Article>> GetArticlesSortedByTime();
    //other custom logic methods
}