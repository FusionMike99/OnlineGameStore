using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineGameStore.DAL.Migrations
{
    public partial class AddPublisherSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "CompanyName", "DeletedAt", "Description", "HomePage", "IsDeleted" },
                values: new object[] { 1, "CD Projekt RED", null, "Develop Wicther", "https://en.cdprojektred.com/", false });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "CompanyName", "DeletedAt", "Description", "HomePage", "IsDeleted" },
                values: new object[] { 2, "Bethesda Softworks", null, "Develop The Elder Scrolls", "https://bethesda.net/dashboard", false });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "CompanyName", "DeletedAt", "Description", "HomePage", "IsDeleted" },
                values: new object[] { 3, "THQ Nordic", null, "Develop Star Wars", "https://www.thqnordic.com/", false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
