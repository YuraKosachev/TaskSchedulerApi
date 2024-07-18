
namespace TaskScheduler.Core.Interfaces.Base
{
    public interface ICommitProvider
    {
        Task SaveChangesAsync();
    }
}
