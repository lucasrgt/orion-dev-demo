using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrionDev.Infrastructure.Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolarSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolarSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FullName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    NickName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Lore = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Appearance_ColorPalette_PrimaryColorHex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Appearance_ColorPalette_SecondaryColorHex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Appearance_ColorPalette_Gradient_StartHex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Appearance_ColorPalette_Gradient_MiddleHex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Appearance_ColorPalette_Gradient_EndHex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Appearance_IconPath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Appearance_BannerPath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SolarSystemId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planets_SolarSystems_SolarSystemId",
                        column: x => x.SolarSystemId,
                        principalTable: "SolarSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Moons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Lore = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Appearance_ColorPalette_PrimaryColorHex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Appearance_ColorPalette_SecondaryColorHex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Appearance_ColorPalette_Gradient_StartHex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Appearance_ColorPalette_Gradient_MiddleHex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Appearance_ColorPalette_Gradient_EndHex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Appearance_IconPath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Appearance_BannerPath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PlanetId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moons_Planets_PlanetId",
                        column: x => x.PlanetId,
                        principalTable: "Planets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Missions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Detail_ExpeditionsNumber = table.Column<int>(type: "integer", nullable: false),
                    Detail_ExplorationTimeInMinutes = table.Column<int>(type: "integer", nullable: false),
                    Detail_AreaCategory = table.Column<int>(type: "integer", nullable: false),
                    Detail_Difficulty = table.Column<int>(type: "integer", nullable: false),
                    Detail_Lore = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Slug = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PlanetId = table.Column<Guid>(type: "uuid", nullable: true),
                    MoonId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Missions_Moons_MoonId",
                        column: x => x.MoonId,
                        principalTable: "Moons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Missions_Planets_PlanetId",
                        column: x => x.PlanetId,
                        principalTable: "Planets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MissionModules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    MissionId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissionModules_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MissionPrerequisites",
                columns: table => new
                {
                    MissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrerequisiteMissionsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionPrerequisites", x => new { x.MissionId, x.PrerequisiteMissionsId });
                    table.ForeignKey(
                        name: "FK_MissionPrerequisites_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissionPrerequisites_Missions_PrerequisiteMissionsId",
                        column: x => x.PrerequisiteMissionsId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMission",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMission", x => new { x.UserId, x.MissionId });
                    table.ForeignKey(
                        name: "FK_UserMission_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMission_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expeditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VideoPath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    MissionModuleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expeditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expeditions_MissionModules_MissionModuleId",
                        column: x => x.MissionModuleId,
                        principalTable: "MissionModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Content = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpeditionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Expeditions_ExpeditionId",
                        column: x => x.ExpeditionId,
                        principalTable: "Expeditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ExpeditionId",
                table: "Comments",
                column: "ExpeditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Expeditions_MissionModuleId",
                table: "Expeditions",
                column: "MissionModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionModules_MissionId",
                table: "MissionModules",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionPrerequisites_PrerequisiteMissionsId",
                table: "MissionPrerequisites",
                column: "PrerequisiteMissionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_MoonId",
                table: "Missions",
                column: "MoonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Missions_PlanetId",
                table: "Missions",
                column: "PlanetId");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_Slug",
                table: "Missions",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Moons_PlanetId",
                table: "Moons",
                column: "PlanetId");

            migrationBuilder.CreateIndex(
                name: "IX_Planets_SolarSystemId",
                table: "Planets",
                column: "SolarSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMission_MissionId",
                table: "UserMission",
                column: "MissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "MissionPrerequisites");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "UserMission");

            migrationBuilder.DropTable(
                name: "Expeditions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MissionModules");

            migrationBuilder.DropTable(
                name: "Missions");

            migrationBuilder.DropTable(
                name: "Moons");

            migrationBuilder.DropTable(
                name: "Planets");

            migrationBuilder.DropTable(
                name: "SolarSystems");
        }
    }
}
