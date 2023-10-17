using System.Linq.Expressions;
using WebApp.Core;
using WebApp.Data.Entities;

namespace WebApp.Repositories;

public interface IRepository<T> where T : class, IBaseEntity
{
    //Task<T?> GetById(Guid id);
    Task<T?> GetById(Guid id, params Expression<Func<T, object>>[] includes);
    Task<T?> GetByIdAsNoTracking(Guid id);

    IQueryable<T> FindBy(Expression<Func<T, bool>> wherePredicate, 
        params Expression<Func<T, object>>[] includes);
    IQueryable<T> GetAsQueryable();

    Task InsertOne(T entity);
    Task InsertMany(IEnumerable<T> entities);

    //very rare use in reality
    Task Update(T entity);

    //used for update
    Task Patch(Guid id, List<PatchDto> patchDtos);

    Task DeleteById(Guid id);
    Task DeleteMany(IEnumerable<T> entities);

    Task<int> Count();
}