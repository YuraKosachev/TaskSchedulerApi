using System.Linq.Expressions;

namespace TaskScheduler.Core.Interfaces.Managers
{
    public interface IGetManager<TModel, TEntity>
        where TModel : class
        where TEntity : class
    {
        Task<IList<TModel>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params string[] includes);
        Task<TModel> GetAsync(Expression<Func<TEntity, bool>> predicate = null, params string[] includes);
    }
}
