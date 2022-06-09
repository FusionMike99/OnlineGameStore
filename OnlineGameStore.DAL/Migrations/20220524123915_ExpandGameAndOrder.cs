using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineGameStore.DAL.Migrations
{
    public partial class ExpandGameAndOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "Games",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePublished",
                table: "Games",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ViewsNumber",
                table: "Games",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelledDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "DatePublished",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ViewsNumber",
                table: "Games");
        }
    }
}
