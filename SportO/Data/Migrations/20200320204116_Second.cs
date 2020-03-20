using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportO.Data.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26b6e53b-7790-44aa-882d-32e593ffbf16");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37d5a592-ad88-4f79-8f6c-5b38ceb3b2f9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77cf4bda-0012-4da1-b62e-32c07977115d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c89dba24-3f91-4d28-8ac7-86281641ea8b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d16f826c-ea32-4e21-b358-7c47eb97a616");

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    phoneNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeagueOwners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(nullable: false),
                    lastName = table.Column<string>(nullable: false),
                    accountActive = table.Column<bool>(nullable: false),
                    PhoneId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeagueOwners_Phones_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "Phones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(nullable: false),
                    lastName = table.Column<string>(nullable: false),
                    accountActive = table.Column<bool>(nullable: false),
                    PhoneId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Phones_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "Phones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Referees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(nullable: false),
                    lastName = table.Column<string>(nullable: false),
                    accountActive = table.Column<bool>(nullable: false),
                    PhoneId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Referees_Phones_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "Phones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamOwners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(nullable: false),
                    lastName = table.Column<string>(nullable: false),
                    accountActive = table.Column<bool>(nullable: false),
                    PhoneId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamOwners_Phones_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "Phones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    leagueName = table.Column<string>(nullable: false),
                    teamCapacity = table.Column<int>(nullable: false),
                    LeagueOwnerId = table.Column<int>(nullable: false),
                    SportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leagues_LeagueOwners_LeagueOwnerId",
                        column: x => x.LeagueOwnerId,
                        principalTable: "LeagueOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leagues_Sport_SportId",
                        column: x => x.SportId,
                        principalTable: "Sport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: false),
                    capacity = table.Column<int>(nullable: false),
                    LeagueId = table.Column<int>(nullable: false),
                    TeamOwnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_TeamOwners_TeamOwnerId",
                        column: x => x.TeamOwnerId,
                        principalTable: "TeamOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(nullable: false),
                    location = table.Column<string>(nullable: false),
                    homeTeamScore = table.Column<int>(nullable: false),
                    awayTeamScore = table.Column<int>(nullable: false),
                    HomeTeamId = table.Column<int>(nullable: true),
                    AwayTeamId = table.Column<int>(nullable: true),
                    RefereeId = table.Column<int>(nullable: false),
                    LeagueId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Referees_RefereeId",
                        column: x => x.RefereeId,
                        principalTable: "Referees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rosters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    playingPosition = table.Column<string>(nullable: false),
                    kitNumber = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: true),
                    PlayerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rosters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rosters_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rosters_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a81704d2-8437-45c9-a2fa-3d43cafb92cc", "14f07059-8d92-4de3-93e6-3c3e32f4be1b", "Admin", "ADMIN" },
                    { "eef76c23-00fa-4c77-ba98-50336d019133", "f2deea35-c303-457b-b7e9-0724249b7538", "League Owner", "LEAGUE OWNER" },
                    { "4db88156-ffee-4d66-907d-ecfc53d1a730", "b08c9d06-e69a-45da-9947-fe0c40774167", "Team Owner", "TEAM OWNER" },
                    { "744ca06f-a7c3-4be3-9274-581c11d52656", "b9f9d2bc-b150-4e42-a12c-63e310040f67", "Referee", "REFEREE" },
                    { "dc8242a0-70cd-4a29-8b83-42776db528bb", "d8319695-6f28-41fd-ac7e-4ec533704ac3", "Player", "PLAYER" }
                });

            migrationBuilder.InsertData(
                table: "Sport",
                columns: new[] { "Id", "type" },
                values: new object[,]
                {
                    { 1, "Soccer" },
                    { 2, "Football" },
                    { 3, "Basketball" },
                    { 4, "Hockey" },
                    { 5, "Rugby" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeagueOwners_PhoneId",
                table: "LeagueOwners",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_LeagueOwnerId",
                table: "Leagues",
                column: "LeagueOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_SportId",
                table: "Leagues",
                column: "SportId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AwayTeamId",
                table: "Matches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_LeagueId",
                table: "Matches",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_RefereeId",
                table: "Matches",
                column: "RefereeId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_PhoneId",
                table: "Players",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Referees_PhoneId",
                table: "Referees",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Rosters_PlayerId",
                table: "Rosters",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rosters_TeamId",
                table: "Rosters",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamOwners_PhoneId",
                table: "TeamOwners",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeagueId",
                table: "Teams",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamOwnerId",
                table: "Teams",
                column: "TeamOwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Rosters");

            migrationBuilder.DropTable(
                name: "Referees");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.DropTable(
                name: "TeamOwners");

            migrationBuilder.DropTable(
                name: "LeagueOwners");

            migrationBuilder.DropTable(
                name: "Sport");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4db88156-ffee-4d66-907d-ecfc53d1a730");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "744ca06f-a7c3-4be3-9274-581c11d52656");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a81704d2-8437-45c9-a2fa-3d43cafb92cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc8242a0-70cd-4a29-8b83-42776db528bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eef76c23-00fa-4c77-ba98-50336d019133");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "77cf4bda-0012-4da1-b62e-32c07977115d", "1afc7989-d12f-408e-8cf3-e159a2a2b91d", "Admin", "ADMIN" },
                    { "37d5a592-ad88-4f79-8f6c-5b38ceb3b2f9", "d158e215-238a-4619-8e94-1cd71c420093", "League Owner", "LEAGUE OWNER" },
                    { "26b6e53b-7790-44aa-882d-32e593ffbf16", "ee838aa2-f41d-43e8-9265-92185e977c81", "Team Owner", "TEAM OWNER" },
                    { "d16f826c-ea32-4e21-b358-7c47eb97a616", "e1f1720f-ece2-4b1d-9f8c-403385427fa5", "Referee", "REFEREE" },
                    { "c89dba24-3f91-4d28-8ac7-86281641ea8b", "25876079-c83a-4eec-a0c0-2edfd82c599f", "Player", "PLAYER" }
                });
        }
    }
}
