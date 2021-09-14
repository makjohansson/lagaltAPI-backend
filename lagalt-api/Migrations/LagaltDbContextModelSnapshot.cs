﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using lagalt_api.Data;

namespace lagalt_api.Migrations
{
    [DbContext(typeof(LagaltDbContext))]
    partial class LagaltDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AppliedProjectsByUser", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("AppliedProjectsByUser");
                });

            modelBuilder.Entity("ClickedProjectsByUser", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ClickedProjectsByUser");
                });

            modelBuilder.Entity("ContributedProjectsByUser", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ContributedProjectsByUser");
                });

            modelBuilder.Entity("FieldProject", b =>
                {
                    b.Property<int>("FieldsFieldId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectsProjectId")
                        .HasColumnType("int");

                    b.HasKey("FieldsFieldId", "ProjectsProjectId");

                    b.HasIndex("ProjectsProjectId");

                    b.ToTable("FieldProject");
                });

            modelBuilder.Entity("FieldUser", b =>
                {
                    b.Property<int>("FieldsFieldId")
                        .HasColumnType("int");

                    b.Property<string>("UsersUserId")
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("FieldsFieldId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("FieldUser");
                });

            modelBuilder.Entity("KeywordProject", b =>
                {
                    b.Property<int>("KeywordsKeywordId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectsProjectId")
                        .HasColumnType("int");

                    b.HasKey("KeywordsKeywordId", "ProjectsProjectId");

                    b.HasIndex("ProjectsProjectId");

                    b.ToTable("KeywordProject");
                });

            modelBuilder.Entity("ProjectSkill", b =>
                {
                    b.Property<int>("ProjectsProjectId")
                        .HasColumnType("int");

                    b.Property<int>("SkillsSkillId")
                        .HasColumnType("int");

                    b.HasKey("ProjectsProjectId", "SkillsSkillId");

                    b.HasIndex("SkillsSkillId");

                    b.ToTable("ProjectSkill");
                });

            modelBuilder.Entity("SeenProjectsByUser", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("SeenProjectsByUser");
                });

            modelBuilder.Entity("SkillUser", b =>
                {
                    b.Property<int>("SkillsSkillId")
                        .HasColumnType("int");

                    b.Property<string>("UsersUserId")
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("SkillsSkillId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("SkillUser");
                });

            modelBuilder.Entity("lagalt_api.Models.Domain.Application", b =>
                {
                    b.Property<int>("ApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Approved")
                        .HasColumnType("bit");

                    b.Property<int>("ApprovedByOwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Motivation")
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApplicationId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("lagalt_api.Models.Domain.Field", b =>
                {
                    b.Property<int>("FieldId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FieldType")
                        .HasColumnType("int");

                    b.HasKey("FieldId");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("lagalt_api.Models.Domain.Keyword", b =>
                {
                    b.Property<int>("KeywordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("KeywordId");

                    b.ToTable("Keywords");
                });

            modelBuilder.Entity("lagalt_api.Models.Domain.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MessageId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("lagalt_api.Models.Domain.Photo", b =>
                {
                    b.Property<int>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasMaxLength(2083)
                        .HasColumnType("nvarchar(2083)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("PhotoId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Photos");

                    b.HasData(
                        new
                        {
                            PhotoId = 1,
                            PhotoUrl = "https://avatars.dicebear.com/api/big-smile/emma.svg",
                            ProjectId = 1
                        },
                        new
                        {
                            PhotoId = 2,
                            PhotoUrl = "https://avatars.dicebear.com/api/big-smile/marcus.svg",
                            ProjectId = 1
                        },
                        new
                        {
                            PhotoId = 3,
                            PhotoUrl = "https://avatars.dicebear.com/api/big-smile/my.svg",
                            ProjectId = 2
                        },
                        new
                        {
                            PhotoId = 4,
                            PhotoUrl = "https://avatars.dicebear.com/api/big-smile/henrik.svg",
                            ProjectId = 1
                        });
                });

            modelBuilder.Entity("lagalt_api.Models.Domain.Portfolio", b =>
                {
                    b.Property<int>("PortfolioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeSpanEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeSpanStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("PortfolioId");

                    b.HasIndex("UserId");

                    b.ToTable("Portfolio");
                });

            modelBuilder.Entity("lagalt_api.Models.Domain.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Closed")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("Progress")
                        .HasColumnType("int");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProjectId");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            ProjectId = 1,
                            Closed = new DateTime(2021, 8, 24, 11, 32, 45, 0, DateTimeKind.Unspecified),
                            Created = new DateTime(2021, 7, 21, 10, 30, 3, 0, DateTimeKind.Unspecified),
                            Progress = 3,
                            ProjectName = "Pokemon"
                        },
                        new
                        {
                            ProjectId = 2,
                            Closed = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Created = new DateTime(2021, 8, 12, 10, 30, 3, 0, DateTimeKind.Unspecified),
                            Progress = 0,
                            ProjectName = "Quiz"
                        });
                });

            modelBuilder.Entity("lagalt_api.Models.Domain.ProjectUser", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<bool>("Owner")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectUsers");
                });

            modelBuilder.Entity("lagalt_api.Models.Domain.Skill", b =>
                {
                    b.Property<int>("SkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SkillName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("SkillId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("lagalt_api.Models.Domain.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("Hidden")
                        .HasColumnType("bit");

                    b.Property<string>("ProfilePhoto")
                        .HasMaxLength(2083)
                        .HasColumnType("nvarchar(2083)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = "717f73d0-2823-425f-9184-cc1578a4f8f9",
                            Description = "",
                            Hidden = true,
                            UserName = "emma"
                        },
                        new
                        {
                            UserId = "717f73b0-2823-425f-9184-cc1678a4f7f9",
                            Description = "",
                            Hidden = false,
                            UserName = "marcus"
                        },
                        new
                        {
                            UserId = "417f73d0-2823-425f-91t4-cc1578a4f8f6",
                            Description = "",
                            Hidden = false,
                            UserName = "my"
                        },
                        new
                        {
                            UserId = "717f73f0-2823-425f-9134-cc1588a4f8f9",
                            Description = "",
                            Hidden = false,
                            UserName = "henrik"
                        });
                });

            modelBuilder.Entity("AppliedProjectsByUser", b =>
                {
                    b.HasOne("lagalt_api.Models.Domain.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lagalt_api.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClickedProjectsByUser", b =>
                {
                    b.HasOne("lagalt_api.Models.Domain.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lagalt_api.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContributedProjectsByUser", b =>
                {
                    b.HasOne("lagalt_api.Models.Domain.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lagalt_api.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FieldProject", b =>
                {
                    b.HasOne("lagalt_api.Models.Domain.Field", null)
                        .WithMany()
                        .HasForeignKey("FieldsFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lagalt_api.Models.Domain.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FieldUser", b =>
                {
                    b.HasOne("lagalt_api.Models.Domain.Field", null)
                        .WithMany()
                        .HasForeignKey("FieldsFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lagalt_api.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KeywordProject", b =>
                {
                    b.HasOne("lagalt_api.Models.Domain.Keyword", null)
                        .WithMany()
                        .HasForeignKey("KeywordsKeywordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lagalt_api.Models.Domain.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectSkill", b =>
                {
                    b.HasOne("lagalt_api.Models.Domain.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lagalt_api.Models.Domain.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsSkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SeenProjectsByUser", b =>
                {
                    b.HasOne("lagalt_api.Models.Domain.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lagalt_api.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SkillUser", b =>
                {
                    b.HasOne("lagalt_api.Models.Domain.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsSkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lagalt_api.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("lagalt_api.Models.Domain.Application", b =>
                {
                    b.HasOne("lagalt_api.Models.Domain.Project", null)
                        .WithMany("Applications")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("lagalt_api.Models.Domain.Message", b =>
                {
                    b.HasOne("lagalt_api.Models.Domain.Project", null)
                        .WithMany("Messages")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("lagalt_api.Models.Domain.Photo", b =>
                {
                    b.HasOne("lagalt_api.Models.Domain.Project", null)
                        .WithMany("Photos")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("lagalt_api.Models.Domain.Portfolio", b =>
                {
                    b.HasOne("lagalt_api.Models.Domain.User", null)
                        .WithMany("Portfolios")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("lagalt_api.Models.Domain.ProjectUser", b =>
                {
                    b.HasOne("lagalt_api.Models.Domain.Project", "Project")
                        .WithMany("ProjectUsers")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lagalt_api.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("lagalt_api.Models.Domain.Project", b =>
                {
                    b.Navigation("Applications");

                    b.Navigation("Messages");

                    b.Navigation("Photos");

                    b.Navigation("ProjectUsers");
                });

            modelBuilder.Entity("lagalt_api.Models.Domain.User", b =>
                {
                    b.Navigation("Portfolios");
                });
#pragma warning restore 612, 618
        }
    }
}
