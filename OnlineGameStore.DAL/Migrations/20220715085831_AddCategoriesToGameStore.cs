using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineGameStore.DAL.Migrations
{
    public partial class AddCategoriesToGameStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Genres");

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "DeletedAt", "Description", "Name", "ParentId" },
                values: new object[,]
                {
                    { 17, null, "Soft drinks, coffees, teas, beers, and ales", "Beverages", null },
                    { 18, null, "Sweet and savory sauces, relishes, spreads, and seasonings", "Condiments", null },
                    { 19, null, "Desserts, candies, and sweet breads", "Confections", null },
                    { 20, null, "Cheeses", "Dairy Products", null },
                    { 21, null, "Breads, crackers, pasta, and cereal", "Grains/Cereals", null },
                    { 22, null, "Prepared meats", "Meat/Poultry", null },
                    { 23, null, "Dried fruit and bean curd", "Produce", null },
                    { 24, null, "Seaweed and fish", "Seafood", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Genres",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
