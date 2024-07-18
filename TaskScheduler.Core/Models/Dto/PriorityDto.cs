
namespace TaskScheduler.Core.Models.Dto
{
    public class PriorityDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public int Level { get; set; }

        public IList<WorkTaskDto> Tasks { get; set; } = new List<WorkTaskDto>();
    }
}
