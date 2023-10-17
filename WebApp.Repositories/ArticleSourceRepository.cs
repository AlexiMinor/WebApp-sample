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
            return await _dbContext.ArticleSources.FirstOrDefaultAsync(source => source.Id.Equals(id));
        }

        public async Task<List<ArticleSource>> GetAll()
        {
            return await _dbContext.ArticleSources.ToListAsync();

        }
        public IQueryable<ArticleSource> GetAsQueryable()
        {
            return _dbContext.ArticleSources.AsQueryable();
        }

        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}