namespace TaskScheduler.Core.Models.Entities
{
    public class Priority : HasDbIdentityId
    {
        public Priority()
        {
            Tasks = new HashSet<WorkTask>();
        }
        public required string Title { get; set; }
        public required int Level { get; set; }

        public virtual ICollection<WorkTask> Tasks { get; set; }
    }
}
