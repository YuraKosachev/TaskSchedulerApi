using Microsoft.EntityFrameworkCore;
using TaskScheduler.Core.Interfaces.Base;

namespace TaskScheduler.Provider.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        public Repository(AppDbContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> All => _dbSet;

        public IQueryable<TEntity> AllAsNoTracking => _dbSet.AsNoTracking();

        public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);

        public void Remove(TEntity entity) => _dbSet.Remove(entity);

        public void RemoveRange(ICollection<TEntity> entities) => _dbSet.RemoveRange(entities);

        public async Task<TEntity> FindAsync(params object[] values) => await _dbSet.FindAsync(values);

        public void Update(TEntity entity) => _dbSet.Update(entity);

        public async Task AddRangeAsync(ICollection<TEntity> entities) => await _dbSet.AddRangeAsync(entities);

    }
}
