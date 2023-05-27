using Microsoft.EntityFrameworkCore;
using VertemNews.Domain.Interfaces.Repositories.Base;
using VertemNews.Infra.Context;
using System.Linq.Expressions;
using System.Reflection;

namespace VertemNews.Infra.Repositories.Base
{
    public class BaseRepository<TEntity> : IDisposable, IBaseRepository<TEntity> where TEntity : class
    {
        internal NewsContext _context;
        internal DbSet<TEntity> _dbSet;

        public BaseRepository(NewsContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            var newEntry = await _dbSet.AddAsync(entity);

            return newEntry.Entity;
        }

        public virtual async Task InsertAllAsync(IEnumerable<TEntity> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            SetPropertyDynamic(entity, "UpdatedAt", (DateTimeOffset?)DateTime.Now);
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task<bool> DeleteAsync(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
            return Task.FromResult(true);
        }

        public virtual Task DeleteAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            List<TEntity> entries = _dbSet.Where(predicate).ToList();
            _dbSet.RemoveRange(entries);

            return Task.CompletedTask;
        }

        public virtual async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                           Func<IQueryable<TEntity>, IQueryable<TEntity>> pagination = null,
                                                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                           params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    if (include != null)
                    {
                        query = query.Include(include);
                    }
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (pagination != null)
            {
                query = pagination(query);
            }

            await Task.CompletedTask;

            return query.AsNoTracking().ToList();
        }

        public virtual async Task<TEntity> FindByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual Task SaveChanges()
        {
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _dbSet = null;
            _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }

        private void SetPropertyDynamic(TEntity entity, string propertyName, object value)
        {
            var prop = entity.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(entity, value, null);
            }
        }
    }
}