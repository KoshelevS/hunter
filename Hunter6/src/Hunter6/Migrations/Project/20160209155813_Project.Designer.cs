using System;
using Hunter.Infrastructure.Data;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;

namespace Hunter6.Migrations.Project
{
    [DbContext(typeof(ProjectContext))]
    [Migration("20160209155813_Project")]
    partial class Project
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hunter6.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Hunter6.Models.Vacancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("ProjectId");

                    b.HasKey("Id");
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
