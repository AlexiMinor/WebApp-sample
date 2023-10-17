using WebApp.Data;
using WebApp.Data.Entities;

namespace WebApp.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ArticlesAggregatorDbContext _dbContext;
    private readonly IRepository<Article> _articleRepository;
    private readonly IRepository<ArticleSource> _articleSourceRepository;
    private readonly IRepository<Comment> _commentRepository;
    private readonly IRepository<User> _userRepository;

    public UnitOfWork(ArticlesAggregatorDbContext dbContext,
        IRepository<Article> articleRepository, 
        IRepository<ArticleSource> articleSourceRepository, 
        IRepository<User> userRepository)
    {
        _dbContext = dbContext;
        _articleRepository = articleRepository;
        _articleSourceRepository = articleSourceRepository;
        _userRepository = userRepository;
    }


    public IRepository<Article> ArticleRepository => _articleRepository;
    public IRepository<ArticleSource> ArticleSourceRepository =>_articleSourceRepository;
    public IRepository<Comment> CommentRepository => _commentRepository;
    public IRepository<User> UserRepository => _userRepository;


    public async Task<int> Commit()
    {
        return await _dbContext.SaveChangesAsync();
    }
}
