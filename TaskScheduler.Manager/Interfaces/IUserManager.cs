using TaskScheduler.Core.Interfaces.Managers;
using TaskScheduler.Core.Models.CreateUpdate;
using TaskScheduler.Core.Models.Dto;
using TaskScheduler.Core.Models.Entities;

namespace TaskScheduler.Manager.Interfaces
{
    public interface IUserManager : ICreateManager<UserCreateUpdateModel>,
        IUpdateManager<UserCreateUpdateModel, Guid>,
        IGetManager<UserDto, User>,
        IDeleteManager<Guid>
    {

    }
}
