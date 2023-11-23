using WebApp.Data.Entities;

namespace WebApp.Repositories;

public interface IUnitOfWork
{
    IRepository<Article> ArticleRepository { get; }
    IRepository<ArticleSource> ArticleSourceRepository { get; }
    IRepository<Comment> CommentRepository { get; }
    IRepository<Role> RoleRepository { get; }
    IRepository<User> UserRepository { get; }
    
    string GetDataFromConfig();
    Task<int> Commit();
}