using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskScheduler.Core.Models;

namespace TaskScheduler.Provider.Configurations
{
    public abstract class Configuration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : HasDbIdentityId
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();
        }

        public abstract void ConfigureProperties(EntityTypeBuilder<TEntity> builder);
    }
}
