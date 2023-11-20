﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StarWarsProgressBarIssueTracker.Infrastructure.Database;

#nullable disable

namespace StarWarsProgressBarIssueTracker.Infrastructure.Database.Migrations
{
    [DbContext(typeof(IssueTrackerContext))]
    partial class IssueTrackerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DbIssueDbLabel", b =>
                {
                    b.Property<Guid>("IssueId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LabelId")
                        .HasColumnType("uuid");

                    b.HasKey("IssueId", "LabelId");

                    b.HasIndex("LabelId");

                    b.ToTable("DbIssueDbLabel", "issue_tracker");
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbAppearance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AppearanceId")
                        .HasColumnType("uuid");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("GitHubId")
                        .HasColumnType("text");

                    b.Property<string>("GitlabId")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TextColor")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("AppearanceId");

                    b.HasIndex("GitHubId")
                        .IsUnique();

                    b.HasIndex("GitlabId")
                        .IsUnique();

                    b.ToTable("Appearances", "issue_tracker");
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbIssue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(1500)
                        .HasColumnType("character varying(1500)");

                    b.Property<string>("GitHubId")
                        .HasColumnType("text");

                    b.Property<string>("GitlabId")
                        .HasColumnType("text");

                    b.Property<string>("GitlabIid")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("MilestoneId")
                        .HasColumnType("uuid");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.Property<Guid?>("ReleaseId")
                        .HasColumnType("uuid");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid?>("VehicleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("GitHubId")
                        .IsUnique();

                    b.HasIndex("GitlabId")
                        .IsUnique();

                    b.HasIndex("GitlabIid")
                        .IsUnique();

                    b.HasIndex("MilestoneId");

                    b.HasIndex("ReleaseId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Issues", "issue_tracker");
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbIssueLink", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("LinkedIssueId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LinkedIssueId");

                    b.ToTable("IssueLinks", "issue_tracker");
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbJob", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CronInterval")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsPaused")
                        .HasColumnType("boolean");

                    b.Property<int>("JobType")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("NextExecution")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("JobType")
                        .IsUnique();

                    b.ToTable("Jobs", "issue_tracker");
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbLabel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("GitHubId")
                        .HasColumnType("text");

                    b.Property<string>("GitlabId")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TextColor")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("GitHubId")
                        .IsUnique();

                    b.HasIndex("GitlabId")
                        .IsUnique();

                    b.ToTable("Labels", "issue_tracker");
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbMilestone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("GitHubId")
                        .HasColumnType("text");

                    b.Property<string>("GitlabId")
                        .HasColumnType("text");

                    b.Property<string>("GitlabIid")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("GitHubId")
                        .IsUnique();

                    b.HasIndex("GitlabId")
                        .IsUnique();

                    b.HasIndex("GitlabIid")
                        .IsUnique();

                    b.ToTable("Milestones", "issue_tracker");
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("PhotoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PhotoId");

                    b.ToTable("Photos", "issue_tracker");
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbRelease", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("GitHubId")
                        .HasColumnType("text");

                    b.Property<string>("GitlabId")
                        .HasColumnType("text");

                    b.Property<string>("GitlabIid")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Notes")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("GitHubId")
                        .IsUnique();

                    b.HasIndex("GitlabId")
                        .IsUnique();

                    b.HasIndex("GitlabIid")
                        .IsUnique();

                    b.ToTable("Releases", "issue_tracker");
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExecuteAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ExecutedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("Tasks", "issue_tracker");
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbTranslation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid?>("TranslationId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TranslationId");

                    b.ToTable("Translations", "issue_tracker");
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbVehicle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EngineColor")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Vehicles", "issue_tracker");
                });

            modelBuilder.Entity("DbIssueDbLabel", b =>
                {
                    b.HasOne("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbLabel", null)
                        .WithMany()
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbIssue", null)
                        .WithMany()
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbAppearance", b =>
                {
                    b.HasOne("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbVehicle", null)
                        .WithMany("Appearances")
                        .HasForeignKey("AppearanceId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbIssue", b =>
                {
                    b.HasOne("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbMilestone", "Milestone")
                        .WithMany("Issues")
                        .HasForeignKey("MilestoneId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbRelease", "Release")
                        .WithMany("Issues")
                        .HasForeignKey("ReleaseId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbVehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");

                    b.Navigation("Milestone");

                    b.Navigation("Release");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbIssueLink", b =>
                {
                    b.HasOne("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbIssue", "LinkedIssue")
                        .WithMany("LinkedIssues")
                        .HasForeignKey("LinkedIssueId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("LinkedIssue");
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbPhoto", b =>
                {
                    b.HasOne("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbVehicle", null)
                        .WithMany("Photos")
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbTask", b =>
                {
                    b.HasOne("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbJob", "Job")
                        .WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbTranslation", b =>
                {
                    b.HasOne("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbVehicle", null)
                        .WithMany("Translations")
                        .HasForeignKey("TranslationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbIssue", b =>
                {
                    b.Navigation("LinkedIssues");
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbMilestone", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbRelease", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("StarWarsProgressBarIssueTracker.Infrastructure.Models.DbVehicle", b =>
                {
                    b.Navigation("Appearances");

                    b.Navigation("Photos");

                    b.Navigation("Translations");
                });
#pragma warning restore 612, 618
        }
    }
}
