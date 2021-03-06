using DevJobs.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevJobs.API.Persistence
{
    public class DevJobsContext : DbContext
    {
        public DevJobsContext(DbContextOptions<DevJobsContext> options) : base(options)
        {
        }

        public DbSet<JobVacancy> JobVacancies { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobVacancy>(e =>
            {
                e.HasKey(jv => jv.Id);
                //e.ToTable("tb_JobVacancies");
                e.HasMany(jv => jv.JobApplications)
                    .WithOne()
                    .HasForeignKey(ja => ja.IdJobVacancy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<JobApplication>(e =>
            {
                e.HasKey(ja => ja.Id);
            });
        }
    }
}
