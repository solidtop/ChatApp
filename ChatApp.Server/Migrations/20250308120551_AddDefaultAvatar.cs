using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Avatars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "https://www.gravatar.com/avatar/?d=mp", "Default" });

            migrationBuilder.UpdateData(
                table: "Avatars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "https://robohash.org/9fd81b488a86a7b3f61eebbca767b644?set=set4&bgset=&size=200x200", "Ninja cat" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Avatars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "https://robohash.org/9fd81b488a86a7b3f61eebbca767b644?set=set4&bgset=&size=200x200", "Ninja cat" });

            migrationBuilder.UpdateData(
                table: "Avatars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "https://robohash.org/e1662c85f590fa94ac5652b749a1f1cd?set=set4&bgset=&size=200x200", "Spotty cat" });
        }
    }
}
