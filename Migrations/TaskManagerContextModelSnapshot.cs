﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManagerApi;

#nullable disable

namespace CourseApi.Migrations
{
    [DbContext(typeof(TaskManagerContext))]
    partial class TaskManagerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TaskManagerApi.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Designation")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("EmployeeFirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("EmployeeLastName")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("TaskManagerApi.Models.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AssgineeEmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int?>("ReporterEmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssgineeEmployeeId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ReporterEmployeeId");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("TaskManagerApi.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CreatorEmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CreatorEmployeeId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TaskManagerApi.Models.Issue", b =>
                {
                    b.HasOne("TaskManagerApi.Models.Employee", "Assginee")
                        .WithMany()
                        .HasForeignKey("AssgineeEmployeeId");

                    b.HasOne("TaskManagerApi.Models.Project", "Project")
                        .WithMany("Issues")
                        .HasForeignKey("ProjectId");

                    b.HasOne("TaskManagerApi.Models.Employee", "Reporter")
                        .WithMany()
                        .HasForeignKey("ReporterEmployeeId");

                    b.Navigation("Assginee");

                    b.Navigation("Project");

                    b.Navigation("Reporter");
                });

            modelBuilder.Entity("TaskManagerApi.Models.Project", b =>
                {
                    b.HasOne("TaskManagerApi.Models.Employee", "Creator")
                        .WithMany("Projects")
                        .HasForeignKey("CreatorEmployeeId");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("TaskManagerApi.Models.Employee", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("TaskManagerApi.Models.Project", b =>
                {
                    b.Navigation("Issues");
                });
#pragma warning restore 612, 618
        }
    }
}
