using System;
using Microsoft.Data.Entity;

namespace Hunter6.Models
{
    public class ProjectContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            //base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Project>().Property(p => p.Name).IsRequired();
            builder.Entity<Project>().Property(p => p.Name).HasMaxLength(100);
        }
    }
}