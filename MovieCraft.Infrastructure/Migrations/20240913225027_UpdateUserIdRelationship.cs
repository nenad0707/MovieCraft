using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieCraft.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserIdRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteMovies_Users_UserId",
                table: "FavoriteMovies");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FavoriteMovies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_UserId",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteMovies_Users_UserId",
                table: "FavoriteMovies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteMovies_Users_UserId",
                table: "FavoriteMovies");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_UserId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "FavoriteMovies",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteMovies_Users_UserId",
                table: "FavoriteMovies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
