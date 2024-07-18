using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using TaskScheduler.Core.Models.Entities;
using TaskScheduler.Provider.Configurations;

namespace TaskScheduler.Core.Interfaces.Base
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<WorkTask> Tasks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect())
                        databaseCreator.Create();
                    if (!databaseCreator.HasTables())
                        databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new WorkTaskConfiguration());
            modelBuilder.ApplyConfiguration(new PriorityConfiguration());

            SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        protected virtual void SeedData(ModelBuilder modelBuilder)
        {
            var priorities = new[]
                {
                new Priority
                {
                    Id = Guid.NewGuid(),
                    Title = "Low",
                    Level = 0
                },
                new Priority
                {
                    Id = Guid.NewGuid(),
                    Title = "Medium",
                    Level = 1
                },
                new Priority
                {
                    Id = Guid.NewGuid(),
                    Title = "High",
                    Level = 2
                }
            };

            modelBuilder.Entity<Priority>().HasData(priorities);
        }

    }
}
