using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Hunter6.Data;

namespace Hunter6.Migrations.Project
{
    [DbContext(typeof(ProjectContext))]
    partial class ProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hunter6.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("ProjectId");
                });

            modelBuilder.Entity("Hunter6.Models.Vacancy", b =>
                {
                    b.Property<int>("VacancyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("ProjectId");

                    b.HasKey("VacancyId");
                });

            modelBuilder.Entity("Hunter6.Models.Vacancy", b =>
                {
                    b.HasOne("Hunter6.Models.Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");
                });
        }
    }
}
