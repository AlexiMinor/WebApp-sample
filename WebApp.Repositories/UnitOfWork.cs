using WebApp.Data;

namespace WebApp.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ArticlesAggregatorDbContext _dbContext;
    private readonly IArticleRepository _articleRepository;
    private readonly IArticleSourceRepository _articleSourceRepository;

    public UnitOfWork(ArticlesAggregatorDbContext dbContext,
        IArticleRepository articleRepository, IArticleSourceRepository articleSourceRepository)
    {
        _dbContext = dbContext;
        _articleRepository = articleRepository;
        _articleSourceRepository = articleSourceRepository;
    }


    public IArticleRepository ArticleRepository => _articleRepository;
    public IArticleSourceRepository ArticleSourceRepository =>_articleSourceRepository;


    public async Task<int> Commit()
    {
        return await _dbContext.SaveChangesAsync();
    }
}
