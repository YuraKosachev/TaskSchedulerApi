using TaskScheduler.Core.Enums;
using TaskScheduler.Core.Models;

namespace TaskScheduler.Core.Models.Entities
{
    public class WorkTask : HasDbIdentityId
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public WorkTaskStatus Status { get; set; }

        public Guid? PriorityId { get; set; }
        public virtual Priority? Priority { get; set; }
        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }
        public DateTime? DueDate { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
