namespace TaskScheduler.Core.Interfaces.Managers
{
    public interface IDeleteManager<TId>
    {
        Task RemoveAsync(TId id);
        Task RemoveRangeAsync(TId[] entities);
    }
}
