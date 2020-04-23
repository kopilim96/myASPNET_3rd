﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentManagement.Data;

namespace StudentManagement.Migrations
{
    [DbContext(typeof(SchoolContext))]
    [Migration("20200422125512_initialCreate")]
    partial class initialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StudentManagement.Models.Course", b =>
                {
                    b.Property<int>("courseId")
                        .HasColumnType("int");

                    b.Property<string>("courseTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("credit")
                        .HasColumnType("int");

                    b.HasKey("courseId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("StudentManagement.Models.Enrollment", b =>
                {
                    b.Property<int>("enrollmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("courseId")
                        .HasColumnType("int");

                    b.Property<int?>("grade")
                        .HasColumnType("int");

                    b.Property<int>("studentId")
                        .HasColumnType("int");

                    b.HasKey("enrollmentId");

                    b.HasIndex("courseId");

                    b.HasIndex("studentId");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("StudentManagement.Models.Student", b =>
                {
                    b.Property<int>("sudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("dateEnrollment")
                        .HasColumnType("datetime2");

                    b.Property<string>("firstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("sudentId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("StudentManagement.Models.Enrollment", b =>
                {
                    b.HasOne("StudentManagement.Models.Course", "course")
                        .WithMany("enrollments")
                        .HasForeignKey("courseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentManagement.Models.Student", "student")
                        .WithMany("enrollments")
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
