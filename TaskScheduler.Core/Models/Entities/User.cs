
namespace TaskScheduler.Core.Models.Entities
{
    public class User : HasDbIdentityId
    {
        public User()
        {
            Tasks = new HashSet<WorkTask>();
        }
        public required string Name { get; set; }

        public virtual ICollection<WorkTask> Tasks { get; set; }
    }
}
