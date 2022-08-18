using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineGameStore.DAL.Migrations
{
    public partial class AddSeedIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("274fcde0-2619-41c9-9772-6e964fa0ef7d"), "376e6bbc-a368-426c-8bd5-722ce48aea80", "User", "USER" },
                    { new Guid("3ae74d1f-43a3-43ef-bce2-0316994a2d0c"), "337fbd08-8074-49d4-b5ac-0cf0fa90e120", "Publisher", "PUBLISHER" },
                    { new Guid("26bf4c5d-7779-4e3a-ad66-a59054e69d60"), "c6bb6787-764a-46e8-be38-bc91db02373e", "Moderator", "MODERATOR" },
                    { new Guid("3b175e2e-ba5f-4c9a-9a80-816c85065ecb"), "d2312c86-97dc-4f43-90bd-32fd1c043222", "Manager", "MANAGER" },
                    { new Guid("35de8211-62d9-43c2-a3d0-d3bb50b44bf6"), "13dc8abe-1335-426c-bf79-e90eca6f5032", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublisherId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("16997a3c-43a1-409e-95e2-06d4e9aebfe2"), 0, "a0b3b2e4-1193-4d92-8f21-089e8833fba6", "user@gmail.com", false, false, null, "USER@GMAIL.COM", "USER", "AQAAAAEAACcQAAAAEGry2F9UPk4qLPShDGdOIqm0aYlAprYDCeIUG990qM30LmEjyHG2wOkGG2kazwQDnw==", null, false, null, null, false, "User" },
                    { new Guid("e2523839-7358-41e6-85ed-a0c48ef27e47"), 0, "b52c9c8b-cfbe-4613-afac-1f8014b8cb46", "moderator@gmail.com", false, false, null, "MODERATOR@GMAIL.COM", "MODERATOR", "AQAAAAEAACcQAAAAEFAIF/SPD3fiqYItnod/oJFwaYc1xaJ84vc0Uj4AlKdWBU1QXgTnFVlRcTiaHzTy9Q==", null, false, null, null, false, "Moderator" },
                    { new Guid("cc0bb8a8-48f0-49fd-a1d5-08578f0a4cdb"), 0, "ffcdf3cd-8b6d-4154-9caa-2c69abc7efb2", "user@gmail.com", false, false, null, "USER@GMAIL.COM", "USER", "AQAAAAEAACcQAAAAEFiDmJWv5aXJ99pRf5IVQ+eEYkwYzZQJqM00hvEOZEqtc3WOM2QQ1YN6xY/lOwS0iw==", null, false, null, null, false, "Manager" },
                    { new Guid("5ba77e7d-788a-403e-982c-29403d4d6dd2"), 0, "d09268cd-547e-4d4e-96e6-095cb9cec60d", "admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEEefXY9nHSFLvWbrvYoN5MV7Y7qBbBvqWOA+StiRKdsIwyrmwfBjtRAjhGFpdR28bw==", null, false, null, null, false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("274fcde0-2619-41c9-9772-6e964fa0ef7d"), new Guid("16997a3c-43a1-409e-95e2-06d4e9aebfe2") },
                    { new Guid("26bf4c5d-7779-4e3a-ad66-a59054e69d60"), new Guid("e2523839-7358-41e6-85ed-a0c48ef27e47") },
                    { new Guid("3b175e2e-ba5f-4c9a-9a80-816c85065ecb"), new Guid("cc0bb8a8-48f0-49fd-a1d5-08578f0a4cdb") },
                    { new Guid("35de8211-62d9-43c2-a3d0-d3bb50b44bf6"), new Guid("5ba77e7d-788a-403e-982c-29403d4d6dd2") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3ae74d1f-43a3-43ef-bce2-0316994a2d0c"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("274fcde0-2619-41c9-9772-6e964fa0ef7d"), new Guid("16997a3c-43a1-409e-95e2-06d4e9aebfe2") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("35de8211-62d9-43c2-a3d0-d3bb50b44bf6"), new Guid("5ba77e7d-788a-403e-982c-29403d4d6dd2") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("3b175e2e-ba5f-4c9a-9a80-816c85065ecb"), new Guid("cc0bb8a8-48f0-49fd-a1d5-08578f0a4cdb") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("26bf4c5d-7779-4e3a-ad66-a59054e69d60"), new Guid("e2523839-7358-41e6-85ed-a0c48ef27e47") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("26bf4c5d-7779-4e3a-ad66-a59054e69d60"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("274fcde0-2619-41c9-9772-6e964fa0ef7d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("35de8211-62d9-43c2-a3d0-d3bb50b44bf6"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3b175e2e-ba5f-4c9a-9a80-816c85065ecb"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("16997a3c-43a1-409e-95e2-06d4e9aebfe2"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5ba77e7d-788a-403e-982c-29403d4d6dd2"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cc0bb8a8-48f0-49fd-a1d5-08578f0a4cdb"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e2523839-7358-41e6-85ed-a0c48ef27e47"));
        }
    }
}
