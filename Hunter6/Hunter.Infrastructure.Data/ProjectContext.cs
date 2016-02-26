using Hunter.Domain.Core;
using Microsoft.Data.Entity;

namespace Hunter.Infrastructure.Data
{
    public class ProjectContext : DbContext
    {
        public DbSet<Project> Project { get; set; }
        public DbSet<Vacancy> Vacancy { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder.Entity<Project>().HasKey(p => p.Id);
            builder.Entity<Project>().Property(p => p.Name).IsRequired().HasMaxLength(100);

            builder.Entity<Vacancy>().HasOne(v => v.Project).WithMany(p => p.Vacancies);
        }
    }
}