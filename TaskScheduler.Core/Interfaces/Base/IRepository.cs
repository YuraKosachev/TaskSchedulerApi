
namespace TaskScheduler.Core.Interfaces.Base
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllAsNoTracking { get; }
        Task AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task<T> FindAsync(params object[] values);
        void Remove(T entity);
        void RemoveRange(ICollection<T> entities);
        void Update(T entity);
    }
}
