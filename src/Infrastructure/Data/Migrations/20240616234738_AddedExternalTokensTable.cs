using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppManager.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedExternalTokensTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnexExternalToken_ExternalToken_ExternalTokensId",
                table: "AnnexExternalToken");

            migrationBuilder.DropForeignKey(
                name: "FK_BranchExternalToken_ExternalToken_ExternalTokensId",
                table: "BranchExternalToken");

            migrationBuilder.DropForeignKey(
                name: "FK_ExternalTokenProject_ExternalToken_ExternalTokensId",
                table: "ExternalTokenProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalToken",
                table: "ExternalToken");

            migrationBuilder.RenameTable(
                name: "ExternalToken",
                newName: "ExternalTokens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalTokens",
                table: "ExternalTokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnexExternalToken_ExternalTokens_ExternalTokensId",
                table: "AnnexExternalToken",
                column: "ExternalTokensId",
                principalTable: "ExternalTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BranchExternalToken_ExternalTokens_ExternalTokensId",
                table: "BranchExternalToken",
                column: "ExternalTokensId",
                principalTable: "ExternalTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalTokenProject_ExternalTokens_ExternalTokensId",
                table: "ExternalTokenProject",
                column: "ExternalTokensId",
                principalTable: "ExternalTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnnexExternalToken_ExternalTokens_ExternalTokensId",
                table: "AnnexExternalToken");

            migrationBuilder.DropForeignKey(
                name: "FK_BranchExternalToken_ExternalTokens_ExternalTokensId",
                table: "BranchExternalToken");

            migrationBuilder.DropForeignKey(
                name: "FK_ExternalTokenProject_ExternalTokens_ExternalTokensId",
                table: "ExternalTokenProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalTokens",
                table: "ExternalTokens");

            migrationBuilder.RenameTable(
                name: "ExternalTokens",
                newName: "ExternalToken");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalToken",
                table: "ExternalToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnnexExternalToken_ExternalToken_ExternalTokensId",
                table: "AnnexExternalToken",
                column: "ExternalTokensId",
                principalTable: "ExternalToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BranchExternalToken_ExternalToken_ExternalTokensId",
                table: "BranchExternalToken",
                column: "ExternalTokensId",
                principalTable: "ExternalToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalTokenProject_ExternalToken_ExternalTokensId",
                table: "ExternalTokenProject",
                column: "ExternalTokensId",
                principalTable: "ExternalToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
