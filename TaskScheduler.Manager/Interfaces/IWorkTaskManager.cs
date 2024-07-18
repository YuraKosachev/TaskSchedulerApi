using TaskScheduler.Core.Interfaces.Managers;
using TaskScheduler.Core.Models.CreateUpdate;
using TaskScheduler.Core.Models.Dto;
using TaskScheduler.Core.Models.Entities;

namespace TaskScheduler.Manager.Interfaces
{
    public interface IWorkTaskManager : ICreateManager<WorkTaskCreateUpdateModel>,
        IUpdateManager<WorkTaskCreateUpdateModel, Guid>,
        IGetManager<WorkTaskDto, WorkTask>,
        IDeleteManager<Guid>
    {
        Task<WorkTaskDto> AssigmentTo(AssigmentWorkTaskModel model);
    }
}
