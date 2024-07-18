using AutoMapper;
using TaskScheduler.Core.Models.CreateUpdate;
using TaskScheduler.Core.Models.Dto;
using TaskScheduler.Core.Models.Entities;

namespace TaskScheduler.Core.Profiles
{
    public class PriorityProfile : Profile
    {
        public PriorityProfile()
        {
            CreateMap<PriorityCreateUpdateModel, Priority>();
            CreateMap<Priority, PriorityDto>();
        }
    }
}
