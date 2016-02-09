using System;
using Hunter6.Models;
using Microsoft.Data.Entity;

namespace Hunter6.Data
{
    public class ProjectContext : DbContext
    {
        public DbSet<Project> Project { get; set; }
        public DbSet<Vacancy> Vacancy { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            //base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Project>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Entity<Vacancy>().HasOne(v => v.Project).WithMany(p => p.Vacancies).HasForeignKey(v => v.ProjectId);
        }
    }
}