using TaskScheduler.Core.Interfaces.Base;

namespace TaskScheduler.Manager.Managers
{
    public abstract class CommitManager
    {
        protected readonly ICommitProvider _commitProvider;
        public CommitManager(ICommitProvider commitProvider)
        {
            _commitProvider = commitProvider;
        }
    }
}
