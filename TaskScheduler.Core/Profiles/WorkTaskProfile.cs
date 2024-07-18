using AutoMapper;
using TaskScheduler.Core.Models.CreateUpdate;
using TaskScheduler.Core.Models.Dto;
using TaskScheduler.Core.Models.Entities;

namespace TaskScheduler.Core.Profiles
{
    public class WorkTaskProfile : Profile
    {
        public WorkTaskProfile()
        {
            CreateMap<WorkTaskCreateUpdateModel, WorkTask>();
            CreateMap<WorkTask, WorkTaskDto>()
                .ForMember(wto => wto.User, action => action.MapFrom(wt => wt.User))
                .ForMember(Wto => Wto.Priority, action => action.MapFrom(wt => wt.Priority));

            CreateMap<AssigmentWorkTaskModel, WorkTask>()
                .ForMember(wt => wt.UserId, action => action.MapFrom(awt => awt.UserId));
        }
    }
}
