using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Entities;

namespace WebApp.Repositories
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(ArticlesAggregatorDbContext dbContext) 
            : base(dbContext)
        {
        }

        //public override async Task<List<Article>> FindBy()
        //{
        //    return await _dbSet.Where(article => !string.IsNullOrEmpty(article.Title)).ToListAsync();
        //}

        public async Task<List<Article>> GetArticlesSortedByTime()
        {
            throw new NotImplementedException();
        }
    }
}