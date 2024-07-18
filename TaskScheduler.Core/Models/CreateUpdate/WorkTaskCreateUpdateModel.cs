using TaskScheduler.Core.Enums;

namespace TaskScheduler.Core.Models.CreateUpdate
{
    public class WorkTaskCreateUpdateModel
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public WorkTaskStatus Status { get; set; }

        public Guid? PriorityId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
