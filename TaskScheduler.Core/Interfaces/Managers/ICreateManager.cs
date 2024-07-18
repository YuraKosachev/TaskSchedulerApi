
namespace TaskScheduler.Core.Interfaces.Managers
{
    public interface ICreateManager<TModel> where TModel : class
    {
        Task CreateAsync(TModel model);
        Task CreateRangeAsync(ICollection<TModel> models);  
    }
}
