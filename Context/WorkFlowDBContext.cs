using Microsoft.EntityFrameworkCore;
using EasyWorkFlowAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace EasyWorkFlowAPI.Context
{
    public class WorkFlowDBContext : DbContext
    {
        public WorkFlowDBContext(DbContextOptions<WorkFlowDBContext> options) : base (options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<JobRole> JobRoles { get; set; }
        public DbSet<WorkScheduleInterface> WorkScheduleInterfaces { get; set; }
        public DbSet<CollaboratorWorkSchedule> CollaboratorWorkSchedules { get; set; }

        public override int SaveChanges()
        {
            var timeStamp = DateTime.Now;
            var entities = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                            e.State == EntityState.Added || 
                            e.State == EntityState.Modified)
                      );

            foreach ( var entity in entities ) 
            { 
                ((BaseEntity) entity.Entity).UpdatedAt = timeStamp;                

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = timeStamp;
                }
            }




            return base.SaveChanges();
        }

    }
}
