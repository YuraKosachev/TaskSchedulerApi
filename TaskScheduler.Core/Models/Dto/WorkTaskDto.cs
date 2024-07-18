using TaskScheduler.Core.Enums;

namespace TaskScheduler.Core.Models.Dto
{
    public class WorkTaskDto
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? PriorityId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public WorkTaskStatus Status { get; set; }
        public PriorityDto? Priority { get; set; }
        public UserDto? User { get; set; }
        public DateTime? DueDate { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
