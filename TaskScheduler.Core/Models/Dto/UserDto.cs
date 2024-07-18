
namespace TaskScheduler.Core.Models.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public IList<WorkTaskDto> Tasks { get; set; } = new List<WorkTaskDto>();
    }
}
