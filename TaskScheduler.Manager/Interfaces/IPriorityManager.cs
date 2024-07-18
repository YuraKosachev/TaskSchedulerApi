using TaskScheduler.Core.Interfaces.Managers;
using TaskScheduler.Core.Models.CreateUpdate;
using TaskScheduler.Core.Models.Dto;
using TaskScheduler.Core.Models.Entities;

namespace TaskScheduler.Manager.Interfaces
{
    public interface IPriorityManager : ICreateManager<PriorityCreateUpdateModel>,
        IUpdateManager<PriorityCreateUpdateModel, Guid>,
        IGetManager<PriorityDto, Priority>,
        IDeleteManager<Guid>
    {
    }
}
