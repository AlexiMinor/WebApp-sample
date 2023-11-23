using WebApp.Data;
using WebApp.Data.Entities;

namespace WebApp.Repositories
{
    public class RoleRepository : Repository<Role>
    {
        public RoleRepository(ArticlesAggregatorDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}