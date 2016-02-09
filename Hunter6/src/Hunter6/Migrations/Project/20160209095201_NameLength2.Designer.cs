using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Hunter6.Models;

namespace Hunter6.Migrations.Project
{
    [DbContext(typeof(ProjectContext))]
    [Migration("20160209095201_NameLength2")]
    partial class NameLength2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hunter6.Models.Project", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("ID");
                });

            modelBuilder.Entity("Hunter6.Models.Vacancy", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("ProjectID");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("Hunter6.Models.Vacancy", b =>
                {
                    b.HasOne("Hunter6.Models.Project")
                        .WithMany()
                        .HasForeignKey("ProjectID");
                });
        }
    }
}
