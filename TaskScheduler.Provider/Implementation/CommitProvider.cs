using TaskScheduler.Core.Interfaces.Base;

namespace TaskScheduler.Provider.Implementation
{
    public class CommitProvider : ICommitProvider
    {
        private readonly AppDbContext _appContext;

        public CommitProvider(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        public async Task SaveChangesAsync() => await _appContext.SaveChangesAsync();

    }
}
