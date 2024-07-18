using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskScheduler.Core.Constants;
using TaskScheduler.Core.Models.Entities;

namespace TaskScheduler.Provider.Configurations
{
    public class PriorityConfiguration : Configuration<Priority>
    {
        public override void ConfigureProperties(EntityTypeBuilder<Priority> builder)
        {
            builder.Property(m => m.Title).IsRequired().HasMaxLength(FieldConstants.Lenght50);
            builder.Property(m => m.Level).IsRequired();
        }
    }
}
