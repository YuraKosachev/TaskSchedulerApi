using AutoMapper;
using TaskScheduler.Core.Interfaces.Base;
using TaskScheduler.Core.Models.CreateUpdate;
using TaskScheduler.Core.Models.Dto;
using TaskScheduler.Core.Models.Entities;
using TaskScheduler.Manager.Interfaces;

namespace TaskScheduler.Manager.Managers
{
    public class PriorityManager : CrudManager<IRepository<Priority>, Priority, PriorityCreateUpdateModel, PriorityDto>,
        IPriorityManager
    {
        public PriorityManager(IRepository<Priority> repository, IMapper mapper, ICommitProvider commitProvider)
            : base(repository, mapper, commitProvider)
        {
        }

       
    }
}
