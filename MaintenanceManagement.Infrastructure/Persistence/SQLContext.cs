using MaintenanceManagement.Domain.Common;
using MaintenanceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Task = MaintenanceManagement.Domain.Entities.Task;


namespace MaintenanceManagement.Infrastructure.Persistence
{
    public class SQLContext : DbContext
    {

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<EntityBase> entry in ChangeTracker.Entries<EntityBase>())
            {
                string userid;
                string username;
                if (string.IsNullOrEmpty(entry.Entity.CreatorId))
                {
                    userid = "admin";
                    username = "admin";
                }
                else
                {
                    userid = entry.Entity.CreatorId;
                    username = entry.Entity.CreatorName;
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreationTime = DateTime.Now;
                        entry.Entity.CreatorName = username;
                        entry.Entity.CreatorId = userid;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastmodificationTime = DateTime.Now;
                        entry.Entity.LastmodifierName = username;
                        entry.Entity.LastmodifierId = userid;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<Service> Services { get; set; }
        public DbSet<Task> Tasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define relationships
            modelBuilder.Entity<Service>()
                .HasMany(s => s.Tasks)
                .WithOne(t => t.Service)
                .HasForeignKey(t => t.ServiceId);
        }
        public SQLContext(DbContextOptions<SQLContext> options) : base(options) { }
    }
}
