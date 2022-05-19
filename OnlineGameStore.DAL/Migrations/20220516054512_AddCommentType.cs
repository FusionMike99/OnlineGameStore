using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineGameStore.DAL.Migrations
{
    public partial class AddCommentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentTypeId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CommentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CommentTypes",
                columns: new[] { "Id", "DeletedAt", "IsDeleted", "Type" },
                values: new object[] { 1, null, false, "Answer" });

            migrationBuilder.InsertData(
                table: "CommentTypes",
                columns: new[] { "Id", "DeletedAt", "IsDeleted", "Type" },
                values: new object[] { 2, null, false, "Quote" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentTypeId",
                table: "Comments",
                column: "CommentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentTypes_IsDeleted",
                table: "CommentTypes",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_CommentTypes_CommentTypeId",
                table: "Comments",
                column: "CommentTypeId",
                principalTable: "CommentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_CommentTypes_CommentTypeId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "CommentTypes");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentTypeId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommentTypeId",
                table: "Comments");
        }
    }
}
