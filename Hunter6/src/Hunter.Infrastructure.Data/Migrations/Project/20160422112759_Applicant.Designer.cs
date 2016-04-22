using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Hunter.Infrastructure.Data;

namespace Hunter.Infrastructure.Data.Migrations.Project
{
    [DbContext(typeof(DomainContext))]
    [Migration("20160422112759_Applicant")]
    partial class Applicant
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hunter.Domain.Core.Applicant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Hunter.Domain.Core.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Hunter.Domain.Core.Vacancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("ProjectId");

                    b.HasKey("Id");
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
