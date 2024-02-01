using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect.Migrations
{
    /// <inheritdoc />
    public partial class deletecascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moves_AspNetUsers_UserId",
                table: "Moves");

            migrationBuilder.DropForeignKey(
                name: "FK_Moves_Games_GameId",
                table: "Moves");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipsCoord_AspNetUsers_UserId",
                table: "ShipsCoord");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipsCoord_Games_GameId",
                table: "ShipsCoord");

            migrationBuilder.AddForeignKey(
                name: "FK_Moves_AspNetUsers_UserId",
                table: "Moves",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Moves_Games_GameId",
                table: "Moves",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipsCoord_AspNetUsers_UserId",
                table: "ShipsCoord",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipsCoord_Games_GameId",
                table: "ShipsCoord",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moves_AspNetUsers_UserId",
                table: "Moves");

            migrationBuilder.DropForeignKey(
                name: "FK_Moves_Games_GameId",
                table: "Moves");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipsCoord_AspNetUsers_UserId",
                table: "ShipsCoord");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipsCoord_Games_GameId",
                table: "ShipsCoord");

            migrationBuilder.AddForeignKey(
                name: "FK_Moves_AspNetUsers_UserId",
                table: "Moves",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Moves_Games_GameId",
                table: "Moves",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipsCoord_AspNetUsers_UserId",
                table: "ShipsCoord",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipsCoord_Games_GameId",
                table: "ShipsCoord",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
