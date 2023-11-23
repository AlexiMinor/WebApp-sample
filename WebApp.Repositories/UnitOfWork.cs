using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApp.Data;
using WebApp.Data.Entities;

namespace WebApp.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly IConfiguration _configuration;
    private readonly ArticlesAggregatorDbContext _dbContext;
    private readonly IRepository<Article> _articleRepository;
    private readonly IRepository<ArticleSource> _articleSourceRepository;
    private readonly IRepository<Comment> _commentRepository;
    private readonly IRepository<Role> _roleRepository;
    private readonly IRepository<User> _userRepository;

    private readonly ILogger<UnitOfWork> _logger;

    public UnitOfWork(ArticlesAggregatorDbContext dbContext,
        IRepository<Article> articleRepository,
        IRepository<ArticleSource> articleSourceRepository,
        IRepository<User> userRepository, IRepository<Comment> commentRepository, IConfiguration configuration, ILogger<UnitOfWork> logger, IRepository<Role> roleRepository)
    {
        _dbContext = dbContext;
        _articleRepository = articleRepository;
        _articleSourceRepository = articleSourceRepository;
        _userRepository = userRepository;
        _commentRepository = commentRepository;
        _configuration = configuration;
        _logger = logger;
        _roleRepository = roleRepository;
    }


    public IRepository<Article> ArticleRepository => _articleRepository;
    public IRepository<ArticleSource> ArticleSourceRepository => _articleSourceRepository;
    public IRepository<Comment> CommentRepository => _commentRepository;
    public IRepository<Role> RoleRepository => _roleRepository;
    public IRepository<User> UserRepository => _userRepository;

    //Only for sample purpose 
    public string GetDataFromConfig()
    {
        var data = _configuration["AppSettings:Test2"];
        var data2 = _configuration["AppSettings:Test1"];

        return "";
    }

    public async Task<int> Commit()
    {
        return await _dbContext.SaveChangesAsync();
    }
}
