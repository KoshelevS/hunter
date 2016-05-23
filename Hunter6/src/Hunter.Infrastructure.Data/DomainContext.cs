using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Hunter.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Hunter.Infrastructure.Data
{
    public class DomainContext : DbContext
    {
        public DomainContext(DbContextOptions<DomainContext> options) : base(options)
        {
        }

        public DbSet<Project> Project { get; set; }
        public DbSet<Vacancy> Vacancy { get; set; }
        public DbSet<Applicant> Applicant { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasKey(p=>p.Id);
            modelBuilder.Entity<Project>().Property(p => p.Name).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Vacancy>().HasOne(v => v.Project).WithMany(p => p.Vacancies);
            modelBuilder.Entity<Applicant>().HasKey(a => a.Id);
            modelBuilder.Entity<Applicant>().Property(a => a.Birthday).HasColumnType("Date");
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public void EnsureSeedData()
        {
            if (!Project.Any())
            {
                Project.AddRange(new List<Project>
                {
                    new Project { Name = "T360°",
                        Vacancies = new List<Vacancy>
                        {
                            new Vacancy { Name = ".Net"},
                            new Vacancy { Name = "ASP.Net"},
                            new Vacancy { Name = "Angular"},
                            new Vacancy { Name = "MVC6"}
                        }
                    },
                    new Project
                    {
                        Name ="ACE",
                        Vacancies = new List<Vacancy>
                        {
                            new Vacancy { Name = ".Net"},
                            new Vacancy { Name = "ASP.Net"},
                            new Vacancy { Name = "Angular"},
                            new Vacancy { Name = "MVC6"}
                        }
                    },
                    new Project { Name ="VIACode"},
                    new Project { Name ="Angular"},
                    new Project { Name ="M-Packs"}
                });

                SaveChanges();
            }
        }
    }
}