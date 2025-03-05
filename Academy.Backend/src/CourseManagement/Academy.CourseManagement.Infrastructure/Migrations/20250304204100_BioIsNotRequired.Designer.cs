﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Academy.CourseManagement.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Academy.CourseManagement.Infrastructure.Migrations
{
    [DbContext(typeof(CourseManagementWriteDbContext))]
    [Migration("20250304204100_BioIsNotRequired")]
    partial class BioIsNotRequired
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("course_management")
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Academy.CourseManagement.Domain.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Bio")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("bio");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("SocialLinks")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("social_links");

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "Academy.CourseManagement.Domain.Author.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("first_name");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("last_name");
                        });

                    b.HasKey("Id")
                        .HasName("pk_authors");

                    b.ToTable("authors", "course_management");
                });

            modelBuilder.Entity("Academy.CourseManagement.Domain.Authorship", b =>
                {
                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid")
                        .HasColumnName("course_id");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.HasKey("CourseId", "AuthorId")
                        .HasName("pk_authorships");

                    b.HasIndex("AuthorId")
                        .HasDatabaseName("ix_authorships_author_id");

                    b.ToTable("authorships", "course_management");
                });

            modelBuilder.Entity("Academy.CourseManagement.Domain.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("description");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_courses");

                    b.ToTable("courses", "course_management");
                });

            modelBuilder.Entity("Academy.CourseManagement.Domain.Lesson", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Attachments")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("attachments");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<string>("LessonType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("lesson_type");

                    b.Property<int>("Position")
                        .HasColumnType("integer")
                        .HasColumnName("position");

                    b.Property<string>("PracticeLessonData")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("practice_lesson_data");

                    b.Property<string>("Questions")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("questions");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.Property<Guid?>("module_id")
                        .HasColumnType("uuid")
                        .HasColumnName("module_id");

                    b.HasKey("Id")
                        .HasName("pk_lessons");

                    b.HasIndex("module_id")
                        .HasDatabaseName("ix_lessons_module_id");

                    b.ToTable("lessons", "course_management");
                });

            modelBuilder.Entity("Academy.CourseManagement.Domain.Module", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)")
                        .HasColumnName("description");

                    b.Property<int>("Position")
                        .HasColumnType("integer")
                        .HasColumnName("position");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.Property<Guid?>("course_id")
                        .HasColumnType("uuid")
                        .HasColumnName("course_id");

                    b.HasKey("Id")
                        .HasName("pk_modules");

                    b.HasIndex("course_id")
                        .HasDatabaseName("ix_modules_course_id");

                    b.ToTable("modules", "course_management");
                });

            modelBuilder.Entity("Academy.CourseManagement.Domain.Authorship", b =>
                {
                    b.HasOne("Academy.CourseManagement.Domain.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_authorships_authors_author_id");

                    b.HasOne("Academy.CourseManagement.Domain.Course", null)
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_authorships_courses_course_id");
                });

            modelBuilder.Entity("Academy.CourseManagement.Domain.Lesson", b =>
                {
                    b.HasOne("Academy.CourseManagement.Domain.Module", null)
                        .WithMany("Lessons")
                        .HasForeignKey("module_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_lessons_modules_module_id");
                });

            modelBuilder.Entity("Academy.CourseManagement.Domain.Module", b =>
                {
                    b.HasOne("Academy.CourseManagement.Domain.Course", null)
                        .WithMany("Modules")
                        .HasForeignKey("course_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_modules_courses_course_id");
                });

            modelBuilder.Entity("Academy.CourseManagement.Domain.Course", b =>
                {
                    b.Navigation("Modules");
                });

            modelBuilder.Entity("Academy.CourseManagement.Domain.Module", b =>
                {
                    b.Navigation("Lessons");
                });
#pragma warning restore 612, 618
        }
    }
}
