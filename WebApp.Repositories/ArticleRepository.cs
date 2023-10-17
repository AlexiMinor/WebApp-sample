using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Entities;

namespace WebApp.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        //Aggregate all logic for work with articles in DB in 1 place
        private readonly ArticlesAggregatorDbContext _dbContext;
        //private readonly ITestService _testService;
        //private int info = 10;
        public ArticleRepository(ArticlesAggregatorDbContext dbContext)
        {
            _dbContext = dbContext;
            //_testService = testService;
        }

        public async Task<List<Article?>> GetArticles()
        {
            //info++;
            return await _dbContext.Articles.ToListAsync();
        }

        public IQueryable<Article?> GetArticlesWithSource()
        {
            //_testService.Do();
            //info++;
            return _dbContext.Articles.Include(article => article.ArticleSource);
        }

        public async Task InsertArticles(IEnumerable<Article?> articles)
        {
            //info++;
            await _dbContext.Articles.AddRangeAsync(articles);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Article?> GetById(Guid id)
        {
            //info++;
            return await _dbContext.Articles.FirstOrDefaultAsync(article => article.Id.Equals(id));
        }

        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}