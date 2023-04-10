using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Token : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRefreshToken_AppUsers_AppUserId",
                table: "AppRefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppRefreshToken",
                table: "AppRefreshToken");

            migrationBuilder.RenameTable(
                name: "AppRefreshToken",
                newName: "AppRefreshTokens");

            migrationBuilder.RenameIndex(
                name: "IX_AppRefreshToken_AppUserId",
                table: "AppRefreshTokens",
                newName: "IX_AppRefreshTokens_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppRefreshTokens",
                table: "AppRefreshTokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRefreshTokens_AppUsers_AppUserId",
                table: "AppRefreshTokens",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRefreshTokens_AppUsers_AppUserId",
                table: "AppRefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppRefreshTokens",
                table: "AppRefreshTokens");

            migrationBuilder.RenameTable(
                name: "AppRefreshTokens",
                newName: "AppRefreshToken");

            migrationBuilder.RenameIndex(
                name: "IX_AppRefreshTokens_AppUserId",
                table: "AppRefreshToken",
                newName: "IX_AppRefreshToken_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppRefreshToken",
                table: "AppRefreshToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRefreshToken_AppUsers_AppUserId",
                table: "AppRefreshToken",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
