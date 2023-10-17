namespace WebApp.Repositories;

public interface IUnitOfWork
{
    IArticleRepository ArticleRepository { get; }
    IArticleSourceRepository ArticleSourceRepository { get; }

    Task<int> Commit();
}