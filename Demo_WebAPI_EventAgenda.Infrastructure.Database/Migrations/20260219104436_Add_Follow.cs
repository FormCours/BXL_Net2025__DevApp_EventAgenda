using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo_WebAPI_EventAgenda.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Follow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agenda_Event_Followers",
                columns: table => new
                {
                    FollowEventsId = table.Column<long>(type: "bigint", nullable: false),
                    FollowersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agenda_Event_Followers", x => new { x.FollowEventsId, x.FollowersId });
                    table.ForeignKey(
                        name: "FK_Agenda_Event_Followers_Agenda_Events_FollowEventsId",
                        column: x => x.FollowEventsId,
                        principalTable: "Agenda_Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agenda_Event_Followers_Members_FollowersId",
                        column: x => x.FollowersId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_Event_Followers_FollowersId",
                table: "Agenda_Event_Followers",
                column: "FollowersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agenda_Event_Followers");
        }
    }
}
