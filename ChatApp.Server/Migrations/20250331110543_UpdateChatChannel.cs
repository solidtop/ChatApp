using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChatChannel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AllowedRoles",
                table: "ChatChannels",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ChatChannels",
                keyColumn: "Id",
                keyValue: 1,
                column: "AllowedRoles",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "ChatChannels",
                keyColumn: "Id",
                keyValue: 2,
                column: "AllowedRoles",
                value: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedRoles",
                table: "ChatChannels");
        }
    }
}
