using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Entities;

namespace WebApp.Repositories
{
    public class ArticleSourceRepository : IArticleSourceRepository
    {
        private readonly ArticlesAggregatorDbContext _dbContext;
        public ArticleSourceRepository(ArticlesAggregatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ArticleSource?> GetById(Guid id)
        {
            return await _dbContext.ArticleSources.FirstOrDefaultAsync(article => article.Id.Equals(id));
        }

        public async Task<List<ArticleSource?>> Get()
        {
            return await _dbContext.ArticleSources.ToListAsync();
        }

        public IQueryable<ArticleSource?> GetAsQueryable()
        {
            return _dbContext.ArticleSources.AsQueryable();
        }

        public async Task InsertArticles(IEnumerable<ArticleSource?> articles)
        {
            await _dbContext.ArticleSources.AddRangeAsync(articles);
        }

        public async Task InsertArticle(ArticleSource article)
        {
            await _dbContext.ArticleSources.AddAsync(article);
        }

        public async Task DeleteById(Guid id)
        {
            var entityToDelete = await GetById(id);
            if (entityToDelete != null)
            {
                _dbContext.ArticleSources.Remove(entityToDelete);
            }
        }

        public async Task DeleteArticles(IEnumerable<ArticleSource> articles)
        {
            if (articles.Any())
            {
                var articlesForDelete = articles
                    .Where(article => _dbContext.ArticleSources.Any(dbA => dbA.Id.Equals(article.Id)))
                    .ToList();
                _dbContext.ArticleSources.RemoveRange(articlesForDelete);
            }

        }
    }
}