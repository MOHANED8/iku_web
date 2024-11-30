using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buytopia.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLoginFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Companies");
        }
    }
}
