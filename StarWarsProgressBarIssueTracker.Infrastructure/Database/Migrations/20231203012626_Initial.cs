using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWarsProgressBarIssueTracker.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "issue_tracker");

            migrationBuilder.CreateTable(
                name: "Milestones",
                schema: "issue_tracker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    MilestoneState = table.Column<int>(type: "integer", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milestones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Releases",
                schema: "issue_tracker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ReleaseNotes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ReleaseState = table.Column<int>(type: "integer", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Releases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                schema: "issue_tracker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EngineColor = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appearances",
                schema: "issue_tracker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Color = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    TextColor = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    AppearanceId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appearances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appearances_Vehicles_AppearanceId",
                        column: x => x.AppearanceId,
                        principalSchema: "issue_tracker",
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                schema: "issue_tracker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    IssueState = table.Column<int>(type: "integer", nullable: false),
                    IssueType = table.Column<int>(type: "integer", nullable: false),
                    MilestoneId = table.Column<Guid>(type: "uuid", nullable: true),
                    ReleaseId = table.Column<Guid>(type: "uuid", nullable: true),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: true),
                    DbVehicleId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_Milestones_MilestoneId",
                        column: x => x.MilestoneId,
                        principalSchema: "issue_tracker",
                        principalTable: "Milestones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Issues_Releases_ReleaseId",
                        column: x => x.ReleaseId,
                        principalSchema: "issue_tracker",
                        principalTable: "Releases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Issues_Vehicles_DbVehicleId",
                        column: x => x.DbVehicleId,
                        principalSchema: "issue_tracker",
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Issues_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalSchema: "issue_tracker",
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                schema: "issue_tracker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PhotoData = table.Column<byte[]>(type: "bytea", nullable: false),
                    PhotoId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Vehicles_PhotoId",
                        column: x => x.PhotoId,
                        principalSchema: "issue_tracker",
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                schema: "issue_tracker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Country = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Text = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TranslationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translations_Vehicles_TranslationId",
                        column: x => x.TranslationId,
                        principalSchema: "issue_tracker",
                        principalTable: "Vehicles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appearances_AppearanceId",
                schema: "issue_tracker",
                table: "Appearances",
                column: "AppearanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_DbVehicleId",
                schema: "issue_tracker",
                table: "Issues",
                column: "DbVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_MilestoneId",
                schema: "issue_tracker",
                table: "Issues",
                column: "MilestoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ReleaseId",
                schema: "issue_tracker",
                table: "Issues",
                column: "ReleaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_VehicleId",
                schema: "issue_tracker",
                table: "Issues",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PhotoId",
                schema: "issue_tracker",
                table: "Photos",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_TranslationId",
                schema: "issue_tracker",
                table: "Translations",
                column: "TranslationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appearances",
                schema: "issue_tracker");

            migrationBuilder.DropTable(
                name: "Issues",
                schema: "issue_tracker");

            migrationBuilder.DropTable(
                name: "Photos",
                schema: "issue_tracker");

            migrationBuilder.DropTable(
                name: "Translations",
                schema: "issue_tracker");

            migrationBuilder.DropTable(
                name: "Milestones",
                schema: "issue_tracker");

            migrationBuilder.DropTable(
                name: "Releases",
                schema: "issue_tracker");

            migrationBuilder.DropTable(
                name: "Vehicles",
                schema: "issue_tracker");
        }
    }
}
