using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace lagalt_api.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    FieldId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.FieldId);
                });

            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    KeywordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tag = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.KeywordId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Closed = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Hidden = table.Column<bool>(type: "bit", nullable: false),
                    ProfilePhoto = table.Column<string>(type: "nvarchar(2083)", maxLength: 2083, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Motivation = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedByOwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_Applications_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldProject",
                columns: table => new
                {
                    FieldsFieldId = table.Column<int>(type: "int", nullable: false),
                    ProjectsProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldProject", x => new { x.FieldsFieldId, x.ProjectsProjectId });
                    table.ForeignKey(
                        name: "FK_FieldProject_Fields_FieldsFieldId",
                        column: x => x.FieldsFieldId,
                        principalTable: "Fields",
                        principalColumn: "FieldId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FieldProject_Projects_ProjectsProjectId",
                        column: x => x.ProjectsProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeywordProject",
                columns: table => new
                {
                    KeywordsKeywordId = table.Column<int>(type: "int", nullable: false),
                    ProjectsProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordProject", x => new { x.KeywordsKeywordId, x.ProjectsProjectId });
                    table.ForeignKey(
                        name: "FK_KeywordProject_Keywords_KeywordsKeywordId",
                        column: x => x.KeywordsKeywordId,
                        principalTable: "Keywords",
                        principalColumn: "KeywordId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeywordProject_Projects_ProjectsProjectId",
                        column: x => x.ProjectsProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Messages_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    PhotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(2083)", maxLength: 2083, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.PhotoId);
                    table.ForeignKey(
                        name: "FK_Photos_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSkill",
                columns: table => new
                {
                    ProjectsProjectId = table.Column<int>(type: "int", nullable: false),
                    SkillsSkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSkill", x => new { x.ProjectsProjectId, x.SkillsSkillId });
                    table.ForeignKey(
                        name: "FK_ProjectSkill_Projects_ProjectsProjectId",
                        column: x => x.ProjectsProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSkill_Skills_SkillsSkillId",
                        column: x => x.SkillsSkillId,
                        principalTable: "Skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppliedProjectsByUser",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedProjectsByUser", x => new { x.UserId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_AppliedProjectsByUser_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppliedProjectsByUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClickedProjectsByUser",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClickedProjectsByUser", x => new { x.UserId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ClickedProjectsByUser_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClickedProjectsByUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContributedProjectsByUser",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributedProjectsByUser", x => new { x.UserId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ContributedProjectsByUser_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContributedProjectsByUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldUser",
                columns: table => new
                {
                    FieldsFieldId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldUser", x => new { x.FieldsFieldId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_FieldUser_Fields_FieldsFieldId",
                        column: x => x.FieldsFieldId,
                        principalTable: "Fields",
                        principalColumn: "FieldId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FieldUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Portfolio",
                columns: table => new
                {
                    PortfolioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeSpanStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeSpanEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrlReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolio", x => x.PortfolioId);
                    table.ForeignKey(
                        name: "FK_Portfolio_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUsers",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Owner = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUsers", x => new { x.UserId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectUsers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeenProjectsByUser",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeenProjectsByUser", x => new { x.UserId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_SeenProjectsByUser_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeenProjectsByUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillUser",
                columns: table => new
                {
                    SkillsSkillId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillUser", x => new { x.SkillsSkillId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_SkillUser_Skills_SkillsSkillId",
                        column: x => x.SkillsSkillId,
                        principalTable: "Skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "Closed", "Created", "Progress", "ProjectName" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 8, 24, 11, 32, 45, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 21, 10, 30, 3, 0, DateTimeKind.Unspecified), 3, "Pokemon" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 12, 10, 30, 3, 0, DateTimeKind.Unspecified), 0, "Quiz" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Description", "Hidden", "ProfilePhoto", "UserName" },
                values: new object[,]
                {
                    { "717f73d0-2823-425f-9184-cc1578a4f8f9", "", true, null, "emma" },
                    { "717f73b0-2823-425f-9184-cc1678a4f7f9", "", false, null, "marcus" },
                    { "417f73d0-2823-425f-91t4-cc1578a4f8f6", "", false, null, "my" },
                    { "717f73f0-2823-425f-9134-cc1588a4f8f9", "", false, null, "henrik" }
                });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "PhotoId", "PhotoUrl", "ProjectId" },
                values: new object[,]
                {
                    { 1, "https://avatars.dicebear.com/api/big-smile/emma.svg", 1 },
                    { 2, "https://avatars.dicebear.com/api/big-smile/marcus.svg", 1 },
                    { 4, "https://avatars.dicebear.com/api/big-smile/henrik.svg", 1 },
                    { 3, "https://avatars.dicebear.com/api/big-smile/my.svg", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ProjectId",
                table: "Applications",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedProjectsByUser_ProjectId",
                table: "AppliedProjectsByUser",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClickedProjectsByUser_ProjectId",
                table: "ClickedProjectsByUser",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ContributedProjectsByUser_ProjectId",
                table: "ContributedProjectsByUser",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldProject_ProjectsProjectId",
                table: "FieldProject",
                column: "ProjectsProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldUser_UsersUserId",
                table: "FieldUser",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordProject_ProjectsProjectId",
                table: "KeywordProject",
                column: "ProjectsProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ProjectId",
                table: "Messages",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ProjectId",
                table: "Photos",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolio_UserId",
                table: "Portfolio",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSkill_SkillsSkillId",
                table: "ProjectSkill",
                column: "SkillsSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_ProjectId",
                table: "ProjectUsers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SeenProjectsByUser_ProjectId",
                table: "SeenProjectsByUser",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillUser_UsersUserId",
                table: "SkillUser",
                column: "UsersUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "AppliedProjectsByUser");

            migrationBuilder.DropTable(
                name: "ClickedProjectsByUser");

            migrationBuilder.DropTable(
                name: "ContributedProjectsByUser");

            migrationBuilder.DropTable(
                name: "FieldProject");

            migrationBuilder.DropTable(
                name: "FieldUser");

            migrationBuilder.DropTable(
                name: "KeywordProject");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Portfolio");

            migrationBuilder.DropTable(
                name: "ProjectSkill");

            migrationBuilder.DropTable(
                name: "ProjectUsers");

            migrationBuilder.DropTable(
                name: "SeenProjectsByUser");

            migrationBuilder.DropTable(
                name: "SkillUser");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "Keywords");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
