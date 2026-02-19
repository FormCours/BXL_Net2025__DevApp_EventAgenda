using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo_WebAPI_EventAgenda.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Fix_MemberRoleLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Members",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "Peon",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Peon");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Peon",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldDefaultValue: "Peon");
        }
    }
}
