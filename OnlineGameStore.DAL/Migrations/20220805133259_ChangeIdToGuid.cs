using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineGameStore.DAL.Migrations
{
    public partial class ChangeIdToGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "GameGenre",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 1, 11 });

            migrationBuilder.DeleteData(
                table: "GameGenre",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { 1, 14 });

            migrationBuilder.DeleteData(
                table: "GamePlatformType",
                keyColumns: new[] { "GameId", "PlatformId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "GamePlatformType",
                keyColumns: new[] { "GameId", "PlatformId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 16);

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

            migrationBuilder.DeleteData(
                table: "PlatformTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PlatformTypes",
                keyColumn: "Id",
                keyValue: 2);

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

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "PlatformTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PlatformTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "OrderStatusId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                "PK_Publishers",
                "Publishers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Publishers");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Publishers",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                "PK_Publishers",
                "Publishers",
                "Id");

            migrationBuilder.DropForeignKey(
                "FK_GamePlatformType_PlatformTypes_PlatformId",
                "GamePlatformType");

            migrationBuilder.DropPrimaryKey(
                "PK_PlatformTypes",
                "PlatformTypes");
            
            migrationBuilder.DropColumn(
                name: "Id",
                table: "PlatformTypes");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PlatformTypes",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                "PK_PlatformTypes",
                "PlatformTypes",
                "Id");

            migrationBuilder.DropForeignKey(
                "FK_OrderDetail_Orders_OrderId",
                "OrderDetail");

            migrationBuilder.DropPrimaryKey(
                "PK_Orders",
                "Orders");
            
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                "PK_Orders",
                "Orders",
                "Id");

            migrationBuilder.AddColumn<int>(
                name: "OrderState",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.DropPrimaryKey(
                "PK_OrderDetail",
                "OrderDetail");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderDetail");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "OrderDetail",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                "PK_OrderDetail",
                "OrderDetail",
                new[] { "GameKey", "OrderId" });
            
            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Orders_OrderId",
                table: "OrderDetail",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.DropForeignKey(
                "FK_Genres_Genres_ParentId",
                "Genres");

            migrationBuilder.DropIndex(
                "IX_Genres_ParentId",
                "Genres");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Genres");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Genres",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_Genres_ParentId",
                "Genres",
                "ParentId");
            
            migrationBuilder.DropForeignKey(
                "FK_GameGenre_Genres_GenreId",
                "GameGenre");

            migrationBuilder.DropPrimaryKey(
                "PK_Genres",
                "Genres");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Genres");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Genres",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                "PK_Genres",
                "Genres",
                "Id");
            
            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Genres_ParentId",
                table: "Genres",
                column: "ParentId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            
            migrationBuilder.DropForeignKey(
                "FK_GamePlatformType_Games_GameId",
                "GamePlatformType");
            
            migrationBuilder.DropForeignKey(
                "FK_GameGenre_Games_GameId",
                "GameGenre");
            
            migrationBuilder.DropForeignKey(
                "FK_Comments_Games_GameId",
                "Comments");

            migrationBuilder.DropPrimaryKey(
                "PK_Games",
                "Games");
            
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Games");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Games",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                "PK_Games",
                "Games",
                "Id");

            migrationBuilder.DropPrimaryKey(
                "PK_GamePlatformType",
                "GamePlatformType");
            
            migrationBuilder.DropIndex(
                "IX_GamePlatformType_PlatformId",
                "GamePlatformType");
            
            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "GamePlatformType");

            migrationBuilder.AddColumn<Guid>(
                name: "PlatformId",
                table: "GamePlatformType",
                type: "uniqueidentifier",
                nullable: false);
            
            migrationBuilder.CreateIndex(
                "IX_GamePlatformType_PlatformId",
                "GamePlatformType",
                "PlatformId");
            
            migrationBuilder.DropColumn(
                name: "GameId",
                table: "GamePlatformType");

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "GamePlatformType",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                "PK_GamePlatformType",
                "GamePlatformType",
                new[] { "PlatformId", "GameId" });
            
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

            migrationBuilder.DropPrimaryKey(
                "PK_GameGenre",
                "GameGenre");
            
            migrationBuilder.DropIndex(
                "IX_GameGenre_GenreId",
                "GameGenre");
            
            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "GameGenre");

            migrationBuilder.AddColumn<Guid>(
                name: "GenreId",
                table: "GameGenre",
                type: "uniqueidentifier",
                nullable: false);
            
            migrationBuilder.CreateIndex(
                "IX_GameGenre_GenreId",
                "GameGenre",
                "GenreId");
            
            migrationBuilder.DropColumn(
                name: "GameId",
                table: "GameGenre");

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "GameGenre",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                "PK_GameGenre",
                "GameGenre",
                new[] { "GenreId", "GameId" });
            
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
            
            migrationBuilder.DropIndex(
                "IX_Comments_ReplyToId",
                "Comments");
            
            migrationBuilder.DropForeignKey(
                "FK_Comments_Comments_ReplyToId",
                "Comments");
            
            migrationBuilder.DropColumn(
                name: "ReplyToId",
                table: "Comments");

            migrationBuilder.AddColumn<Guid>(
                name: "ReplyToId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);
            
            migrationBuilder.CreateIndex(
                "IX_Comments_ReplyToId",
                "Comments",
                "ReplyToId");
            
            migrationBuilder.DropIndex(
                "IX_Comments_GameId",
                "Comments");
            
            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Comments");

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false);
            
            migrationBuilder.CreateIndex(
                "IX_Comments_GameId",
                "Comments",
                "GameId");
            
            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Games_GameId",
                table: "Comments",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropPrimaryKey(
                "PK_Comments",
                "Comments");
            
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Comments");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                "PK_Comments",
                "Comments",
                "Id");
            
            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ReplyToId",
                table: "Comments",
                column: "ReplyToId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "DateAdded", "DatePublished", "DeletedAt", "Description", "Discontinued", "Key", "Name", "Price", "PublisherName", "QuantityPerUnit", "UnitsInStock" },
                values: new object[] { new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"), new DateTime(2022, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2015, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "As war rages on throughout the Northern Realms, you take on the greatest contract of your life — tracking down the Child of Prophecy, a living weapon that can alter the shape of the world.", false, "the-witcher-3", "The Witcher 3", 49.99m, "CD Projekt RED", "units", (short)50 });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "DeletedAt", "Description", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("06ea7b74-a38d-42fe-8e7f-4edadb8cfa02"), null, "Seaweed and fish", "Seafood", null },
                    { new Guid("6b0663e6-d189-4536-a950-42e53d419175"), null, "Dried fruit and bean curd", "Produce", null },
                    { new Guid("455ef484-ab3d-4bbd-bd8b-41e01fb5f0d6"), null, "Prepared meats", "Meat/Poultry", null },
                    { new Guid("5c75e7f8-3ac2-4877-a5c5-6b0e3b02f70a"), null, "Breads, crackers, pasta, and cereal", "Grains/Cereals", null },
                    { new Guid("a6079e7a-6363-43d1-8004-8c99b5d9b1b9"), null, "Cheeses", "Dairy Products", null },
                    { new Guid("ad5d6708-6675-4504-8a90-e2cb60ff4a4c"), null, "Sweet and savory sauces, relishes, spreads, and seasonings", "Condiments", null },
                    { new Guid("d41414d6-ce60-435d-809a-4f4350990604"), null, "Soft drinks, coffees, teas, beers, and ales", "Beverages", null },
                    { new Guid("f5b883d4-1d0b-4dac-9c9e-a9cf69c30162"), null, "Desserts, candies, and sweet breads", "Confections", null }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "DeletedAt", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("8f9804ae-fca1-4117-b89e-db24f038f30e"), null, "Puzzle & Skill", null },
                    { new Guid("2d96d846-dd30-4982-95ea-1bf4aadf38f9"), null, "Adventure", null },
                    { new Guid("ae4c8069-231a-4030-8202-907a2a548792"), null, "Action", null },
                    { new Guid("cb800c29-33e6-439e-b05e-657730c43213"), null, "Races", null },
                    { new Guid("ce5b782d-d609-4bd5-a1c2-230861d63e05"), null, "Sports", null },
                    { new Guid("e49f4755-02d6-444a-b25c-9e65c5298cc5"), null, "RPG", null },
                    { new Guid("8978f06e-a703-4746-bbe7-cc16a7e0249e"), null, "Strategy", null },
                    { new Guid("90649709-a022-415f-9e4b-45f4f9affc78"), null, "Misc.", null }
                });

            migrationBuilder.InsertData(
                table: "PlatformTypes",
                columns: new[] { "Id", "DeletedAt", "Type" },
                values: new object[,]
                {
                    { new Guid("0d879fbc-75d9-4df6-8ea2-2c0484207e20"), null, "Mobile" },
                    { new Guid("46c32720-7400-4e51-82bb-96d35d4eff18"), null, "Browser" },
                    { new Guid("9f07b51a-f2cb-4c1b-ada4-b4ebb652ce0b"), null, "Desktop" },
                    { new Guid("8dac1629-29ce-4054-89f0-6d5bba95280f"), null, "Console" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "CompanyName", "DeletedAt", "Description", "HomePage", "IsDeleted" },
                values: new object[,]
                {
                    { new Guid("f02a54ae-b7b2-4904-9b67-c14f5c9622a7"), "Bethesda Softworks", null, "Develop The Elder Scrolls", "https://bethesda.net/dashboard", false },
                    { new Guid("8b750162-60ab-4de9-a58d-f7ea9fbd0321"), "CD Projekt RED", null, "Develop Witcher", "https://en.cdprojektred.com/", false },
                    { new Guid("eaad9f95-7976-4893-a1ed-17c1679f2728"), "THQ Nordic", null, "Develop Star Wars", "https://www.thqnordic.com/", false }
                });

            migrationBuilder.InsertData(
                table: "GameGenre",
                columns: new[] { "GameId", "GenreId" },
                values: new object[,]
                {
                    { new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"), new Guid("e49f4755-02d6-444a-b25c-9e65c5298cc5") },
                    { new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"), new Guid("2d96d846-dd30-4982-95ea-1bf4aadf38f9") }
                });

            migrationBuilder.InsertData(
                table: "GamePlatformType",
                columns: new[] { "GameId", "PlatformId" },
                values: new object[,]
                {
                    { new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"), new Guid("9f07b51a-f2cb-4c1b-ada4-b4ebb652ce0b") },
                    { new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"), new Guid("8dac1629-29ce-4054-89f0-6d5bba95280f") }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "DeletedAt", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("46cc32ce-db19-4c56-86da-19713ddf6976"), null, "RTS", new Guid("8978f06e-a703-4746-bbe7-cc16a7e0249e") },
                    { new Guid("773ec3e3-67fa-4a45-8d3a-1ad616b8913f"), null, "TBS", new Guid("8978f06e-a703-4746-bbe7-cc16a7e0249e") },
                    { new Guid("45e4e6b3-dbf6-4bd7-9349-174aa48ab2a4"), null, "Rally", new Guid("cb800c29-33e6-439e-b05e-657730c43213") },
                    { new Guid("94a5f76b-cc14-447d-b9fd-babd65d6f4d4"), null, "Arcade", new Guid("cb800c29-33e6-439e-b05e-657730c43213") },
                    { new Guid("3b9515a5-cb4e-4667-8ff9-36ac005db45f"), null, "Formula", new Guid("cb800c29-33e6-439e-b05e-657730c43213") },
                    { new Guid("d4937448-4b9d-4277-bc77-a6ed012f0403"), null, "Off-road", new Guid("cb800c29-33e6-439e-b05e-657730c43213") },
                    { new Guid("59800dd6-a511-47b3-9420-60376c6a813d"), null, "FPS", new Guid("ae4c8069-231a-4030-8202-907a2a548792") },
                    { new Guid("cbab23a6-5122-4a72-995c-d3cd8d5ad6c3"), null, "TPS", new Guid("ae4c8069-231a-4030-8202-907a2a548792") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameGenre",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"), new Guid("2d96d846-dd30-4982-95ea-1bf4aadf38f9") });

            migrationBuilder.DeleteData(
                table: "GameGenre",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"), new Guid("e49f4755-02d6-444a-b25c-9e65c5298cc5") });

            migrationBuilder.DeleteData(
                table: "GamePlatformType",
                keyColumns: new[] { "GameId", "PlatformId" },
                keyValues: new object[] { new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"), new Guid("8dac1629-29ce-4054-89f0-6d5bba95280f") });

            migrationBuilder.DeleteData(
                table: "GamePlatformType",
                keyColumns: new[] { "GameId", "PlatformId" },
                keyValues: new object[] { new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"), new Guid("9f07b51a-f2cb-4c1b-ada4-b4ebb652ce0b") });

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("06ea7b74-a38d-42fe-8e7f-4edadb8cfa02"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("3b9515a5-cb4e-4667-8ff9-36ac005db45f"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("455ef484-ab3d-4bbd-bd8b-41e01fb5f0d6"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("45e4e6b3-dbf6-4bd7-9349-174aa48ab2a4"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("46cc32ce-db19-4c56-86da-19713ddf6976"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("59800dd6-a511-47b3-9420-60376c6a813d"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("5c75e7f8-3ac2-4877-a5c5-6b0e3b02f70a"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("6b0663e6-d189-4536-a950-42e53d419175"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("773ec3e3-67fa-4a45-8d3a-1ad616b8913f"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("8f9804ae-fca1-4117-b89e-db24f038f30e"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("90649709-a022-415f-9e4b-45f4f9affc78"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("94a5f76b-cc14-447d-b9fd-babd65d6f4d4"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a6079e7a-6363-43d1-8004-8c99b5d9b1b9"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("ad5d6708-6675-4504-8a90-e2cb60ff4a4c"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("cbab23a6-5122-4a72-995c-d3cd8d5ad6c3"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("ce5b782d-d609-4bd5-a1c2-230861d63e05"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("d41414d6-ce60-435d-809a-4f4350990604"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("d4937448-4b9d-4277-bc77-a6ed012f0403"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("f5b883d4-1d0b-4dac-9c9e-a9cf69c30162"));

            migrationBuilder.DeleteData(
                table: "PlatformTypes",
                keyColumn: "Id",
                keyValue: new Guid("0d879fbc-75d9-4df6-8ea2-2c0484207e20"));

            migrationBuilder.DeleteData(
                table: "PlatformTypes",
                keyColumn: "Id",
                keyValue: new Guid("46c32720-7400-4e51-82bb-96d35d4eff18"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("8b750162-60ab-4de9-a58d-f7ea9fbd0321"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("eaad9f95-7976-4893-a1ed-17c1679f2728"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("f02a54ae-b7b2-4904-9b67-c14f5c9622a7"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("94c979fa-20e5-412e-895b-a694b94f5ad4"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("2d96d846-dd30-4982-95ea-1bf4aadf38f9"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("8978f06e-a703-4746-bbe7-cc16a7e0249e"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("ae4c8069-231a-4030-8202-907a2a548792"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("cb800c29-33e6-439e-b05e-657730c43213"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("e49f4755-02d6-444a-b25c-9e65c5298cc5"));

            migrationBuilder.DeleteData(
                table: "PlatformTypes",
                keyColumn: "Id",
                keyValue: new Guid("8dac1629-29ce-4054-89f0-6d5bba95280f"));

            migrationBuilder.DeleteData(
                table: "PlatformTypes",
                keyColumn: "Id",
                keyValue: new Guid("9f07b51a-f2cb-4c1b-ada4-b4ebb652ce0b"));

            migrationBuilder.DropColumn(
                name: "OrderState",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Publishers",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PlatformTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "OrderStatusId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderDetail",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "Genres",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Genres",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Games",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "PlatformId",
                table: "GamePlatformType",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "GamePlatformType",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "GameGenre",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "GameGenre",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "ReplyToId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "DateAdded", "DatePublished", "DeletedAt", "Description", "Discontinued", "Key", "Name", "Price", "PublisherName", "QuantityPerUnit", "UnitsInStock" },
                values: new object[] { 1, null, null, null, "As war rages on throughout the Northern Realms, you take on the greatest contract of your life — tracking down the Child of Prophecy, a living weapon that can alter the shape of the world.", false, "the-witcher-3", "The Witcher 3", 49.99m, "CD Projekt RED", "units", (short)50 });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "DeletedAt", "Description", "Name", "ParentId" },
                values: new object[,]
                {
                    { 24, null, "Seaweed and fish", "Seafood", null },
                    { 23, null, "Dried fruit and bean curd", "Produce", null },
                    { 22, null, "Prepared meats", "Meat/Poultry", null },
                    { 20, null, "Cheeses", "Dairy Products", null },
                    { 19, null, "Desserts, candies, and sweet breads", "Confections", null },
                    { 18, null, "Sweet and savory sauces, relishes, spreads, and seasonings", "Condiments", null },
                    { 17, null, "Soft drinks, coffees, teas, beers, and ales", "Beverages", null },
                    { 21, null, "Breads, crackers, pasta, and cereal", "Grains/Cereals", null }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "DeletedAt", "Name", "ParentId" },
                values: new object[,]
                {
                    { 15, null, "Puzzle & Skill", null },
                    { 14, null, "Adventure", null },
                    { 11, null, "Action", null },
                    { 6, null, "Races", null },
                    { 5, null, "Sports", null },
                    { 4, null, "RPG", null },
                    { 1, null, "Strategy", null },
                    { 16, null, "Misc.", null }
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "Id", "DeletedAt", "Status" },
                values: new object[,]
                {
                    { 4, null, "Closed" },
                    { 3, null, "Cancelled" },
                    { 2, null, "In progress" },
                    { 1, null, "Open" }
                });

            migrationBuilder.InsertData(
                table: "PlatformTypes",
                columns: new[] { "Id", "DeletedAt", "Type" },
                values: new object[,]
                {
                    { 1, null, "Mobile" },
                    { 2, null, "Browser" },
                    { 3, null, "Desktop" },
                    { 4, null, "Console" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "CompanyName", "DeletedAt", "Description", "HomePage", "IsDeleted" },
                values: new object[,]
                {
                    { 2, "Bethesda Softworks", null, "Develop The Elder Scrolls", "https://bethesda.net/dashboard", false },
                    { 1, "CD Projekt RED", null, "Develop Witcher", "https://en.cdprojektred.com/", false },
                    { 3, "THQ Nordic", null, "Develop Star Wars", "https://www.thqnordic.com/", false }
                });

            migrationBuilder.InsertData(
                table: "GameGenre",
                columns: new[] { "GameId", "GenreId" },
                values: new object[,]
                {
                    { 1, 11 },
                    { 1, 14 }
                });

            migrationBuilder.InsertData(
                table: "GamePlatformType",
                columns: new[] { "GameId", "PlatformId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 1, 4 }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "DeletedAt", "Name", "ParentId" },
                values: new object[,]
                {
                    { 2, null, "RTS", 1 },
                    { 3, null, "TBS", 1 },
                    { 7, null, "Rally", 6 },
                    { 8, null, "Arcade", 6 },
                    { 9, null, "Formula", 6 },
                    { 10, null, "Off-road", 6 },
                    { 12, null, "FPS", 11 },
                    { 13, null, "TPS", 11 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatuses_IsDeleted",
                table: "OrderStatuses",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId",
                principalTable: "OrderStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
