using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppManager.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedFileVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Versions",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Versions");
        }
    }
}
