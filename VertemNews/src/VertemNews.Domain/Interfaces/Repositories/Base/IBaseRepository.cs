using System.Linq.Expressions;

namespace VertemNews.Domain.Interfaces.Repositories.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> InsertAsync(TEntity entity);

        Task InsertAllAsync(IEnumerable<TEntity> entity);

        Task UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(TEntity entity);

        Task DeleteAllAsync(Expression<Func<TEntity, bool>> predict = null);

        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predict = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> pagination = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> FindByIdAsync(Guid id);

        Task SaveChangesAsync();

        Task SaveChanges();
    }
}