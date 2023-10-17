using Microsoft.EntityFrameworkCore;
using WebApp.Data.Entities;

namespace WebApp.Data;

public class ArticlesAggregatorDbContext : DbContext
{

    public DbSet<Article?> Articles { get; set; }
    public DbSet<ArticleSource> ArticleSources { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; }

    public ArticlesAggregatorDbContext(DbContextOptions<ArticlesAggregatorDbContext> options) 
        : base(options)
    {
    }
}