using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppManager.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Versions",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Versions",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Versions",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Tags",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Tags",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Tags",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Projects",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Projects",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Projects",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "ExternalToken",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "ExternalToken",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "ExternalToken",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Branches",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Branches",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Branches",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Annexes",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Annexes",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Annexes",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Versions",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Versions",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Versions",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Tags",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Tags",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Tags",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Projects",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Projects",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Projects",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "ExternalToken",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "ExternalToken",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ExternalToken",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Branches",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Branches",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Branches",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Annexes",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Annexes",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Annexes",
                newName: "Created");
        }
    }
}
