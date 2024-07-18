using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskScheduler.Core.Constants;
using TaskScheduler.Core.Models.Entities;

namespace TaskScheduler.Provider.Configurations
{
    public class WorkTaskConfiguration : Configuration<WorkTask>
    {
        public override void ConfigureProperties(EntityTypeBuilder<WorkTask> builder)
        {
            builder.Property(m => m.CreatedAt)
               .HasDefaultValueSql("getdate()"); ;
            builder.Property(m => m.Title).IsRequired().HasMaxLength(FieldConstants.Lenght250);
            builder.Property(m => m.Description);

            builder.HasOne(m => m.User)
                .WithMany(m => m.Tasks)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(m => m.Priority)
                .WithMany(m => m.Tasks)
                .HasForeignKey(m => m.PriorityId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
