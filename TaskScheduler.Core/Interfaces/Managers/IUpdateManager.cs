namespace TaskScheduler.Core.Interfaces.Managers
{
    public interface IUpdateManager<TModel, TId> where TModel : class
    {
        Task UpdateAsync(TId id, TModel model);
    }

}
