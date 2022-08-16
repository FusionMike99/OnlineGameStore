using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineGameStore.DAL.Migrations
{
    public partial class ChangeGameEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "ReorderLevel",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "UnitsOnOrder",
                table: "Games");

            migrationBuilder.AlterColumn<decimal>(
                name: "Freight",
                table: "Orders",
                type: "money",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<short>(
                name: "Quantity",
                table: "OrderDetail",
                type: "smallint",
                nullable: false,
                defaultValue: (short)1,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "OrderDetail",
                type: "money",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<float>(
                name: "Discount",
                table: "OrderDetail",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "OrderDetail",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "OrderDetail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OrderDetail",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "GamePlatformType",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "GamePlatformType",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "GamePlatformType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "GameGenre",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "GameGenre",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "GameGenre",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail",
                columns: new[] { "GameKey", "OrderId" });

            migrationBuilder.UpdateData(
                table: "GameGenre",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"), new Guid("2d96d846-dd30-4982-95ea-1bf4aadf38f9") },
                column: "Id",
                value: new Guid("54716b49-d5e9-44d0-9c11-559d8b17d3dc"));

            migrationBuilder.UpdateData(
                table: "GameGenre",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"), new Guid("e49f4755-02d6-444a-b25c-9e65c5298cc5") },
                column: "Id",
                value: new Guid("297c1914-86e5-4f50-a6d4-d0a83cce8c7b"));

            migrationBuilder.UpdateData(
                table: "GamePlatformType",
                keyColumns: new[] { "GameId", "PlatformId" },
                keyValues: new object[] { new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"), new Guid("8dac1629-29ce-4054-89f0-6d5bba95280f") },
                column: "Id",
                value: new Guid("07bcad23-3d65-43ad-b930-be5042bd77be"));

            migrationBuilder.UpdateData(
                table: "GamePlatformType",
                keyColumns: new[] { "GameId", "PlatformId" },
                keyValues: new object[] { new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"), new Guid("9f07b51a-f2cb-4c1b-ada4-b4ebb652ce0b") },
                column: "Id",
                value: new Guid("639847ca-2477-44fd-82c3-31204be47f80"));

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_IsDeleted",
                table: "OrderDetail",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatformType_IsDeleted",
                table: "GamePlatformType",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenre_IsDeleted",
                table: "GameGenre",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_IsDeleted",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_GamePlatformType_IsDeleted",
                table: "GamePlatformType");

            migrationBuilder.DropIndex(
                name: "IX_GameGenre_IsDeleted",
                table: "GameGenre");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "GamePlatformType");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GamePlatformType");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "GamePlatformType");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "GameGenre");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GameGenre");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "GameGenre");

            migrationBuilder.AlterColumn<decimal>(
                name: "Freight",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<short>(
                name: "Quantity",
                table: "OrderDetail",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldDefaultValue: (short)1);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "OrderDetail",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<float>(
                name: "Discount",
                table: "OrderDetail",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real",
                oldDefaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "ReorderLevel",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitsOnOrder",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail",
                columns: new[] { "OrderId", "GameKey" });
        }
    }
}
