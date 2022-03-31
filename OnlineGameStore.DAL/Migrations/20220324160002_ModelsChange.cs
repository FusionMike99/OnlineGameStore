using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineGameStore.DAL.Migrations
{
    public partial class ModelsChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenre_Games_GamesId",
                table: "GameGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenre_Genres_GenresId",
                table: "GameGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatformType_Games_GamesId",
                table: "GamePlatformType");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatformType_PlatformTypes_PlatformTypesId",
                table: "GamePlatformType");

            migrationBuilder.RenameColumn(
                name: "PlatformTypesId",
                table: "GamePlatformType",
                newName: "PlatformId");

            migrationBuilder.RenameColumn(
                name: "GamesId",
                table: "GamePlatformType",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_GamePlatformType_PlatformTypesId",
                table: "GamePlatformType",
                newName: "IX_GamePlatformType_PlatformId");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "GameGenre",
                newName: "GenreId");

            migrationBuilder.RenameColumn(
                name: "GamesId",
                table: "GameGenre",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_GameGenre_GenresId",
                table: "GameGenre",
                newName: "IX_GameGenre_GenreId");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Comments",
                newName: "ReplyToId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ParentId",
                table: "Comments",
                newName: "IX_Comments_ReplyToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ReplyToId",
                table: "Comments",
                column: "ReplyToId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenre_Games_GameId",
                table: "GameGenre",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenre_Genres_GenreId",
                table: "GameGenre",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatformType_Games_GameId",
                table: "GamePlatformType",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatformType_PlatformTypes_PlatformId",
                table: "GamePlatformType",
                column: "PlatformId",
                principalTable: "PlatformTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ReplyToId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenre_Games_GameId",
                table: "GameGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenre_Genres_GenreId",
                table: "GameGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatformType_Games_GameId",
                table: "GamePlatformType");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatformType_PlatformTypes_PlatformId",
                table: "GamePlatformType");

            migrationBuilder.RenameColumn(
                name: "PlatformId",
                table: "GamePlatformType",
                newName: "PlatformTypesId");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "GamePlatformType",
                newName: "GamesId");

            migrationBuilder.RenameIndex(
                name: "IX_GamePlatformType_PlatformId",
                table: "GamePlatformType",
                newName: "IX_GamePlatformType_PlatformTypesId");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "GameGenre",
                newName: "GenresId");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "GameGenre",
                newName: "GamesId");

            migrationBuilder.RenameIndex(
                name: "IX_GameGenre_GenreId",
                table: "GameGenre",
                newName: "IX_GameGenre_GenresId");

            migrationBuilder.RenameColumn(
                name: "ReplyToId",
                table: "Comments",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ReplyToId",
                table: "Comments",
                newName: "IX_Comments_ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParentId",
                table: "Comments",
                column: "ParentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenre_Games_GamesId",
                table: "GameGenre",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenre_Genres_GenresId",
                table: "GameGenre",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatformType_Games_GamesId",
                table: "GamePlatformType",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatformType_PlatformTypes_PlatformTypesId",
                table: "GamePlatformType",
                column: "PlatformTypesId",
                principalTable: "PlatformTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
