using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hunter.Infrastructure.Data.Migrations
{
    [DbContext(typeof(DomainContext))]
    [Migration("20160525163946_File")]
    partial class File
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hunter.Domain.Core.Applicant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("Date");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.ToTable("Applicant");
                });

            modelBuilder.Entity("Hunter.Domain.Core.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Extension");

                    b.Property<byte[]>("FileContent");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("File");
                });

            modelBuilder.Entity("Hunter.Domain.Core.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Hunter.Domain.Core.Vacancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Vacancy");
                });

            modelBuilder.Entity("Hunter.Domain.Core.Vacancy", b =>
                {
                    b.HasOne("Hunter.Domain.Core.Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");
                });
        }
    }
}
