using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskScheduler.Core.Constants;
using TaskScheduler.Core.Models.Entities;

namespace TaskScheduler.Provider.Configurations
{
    public class UserConfiguration : Configuration<User>
    {
        public override void ConfigureProperties(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(FieldConstants.Lenght150);
        }
    }
}
