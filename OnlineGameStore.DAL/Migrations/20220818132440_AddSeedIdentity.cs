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
                    { new Guid("274fcde0-2619-41c9-9772-6e964fa0ef7d"), "943a7823-9675-4672-9459-72f2ea5760b0", "User", "USER" },
                    { new Guid("3ae74d1f-43a3-43ef-bce2-0316994a2d0c"), "5db2276b-065f-42e4-8c18-58a5245477a4", "Publisher", "PUBLISHER" },
                    { new Guid("26bf4c5d-7779-4e3a-ad66-a59054e69d60"), "085098cf-43b4-4548-b180-2bbdffb396f0", "Moderator", "MODERATOR" },
                    { new Guid("3b175e2e-ba5f-4c9a-9a80-816c85065ecb"), "3abe68a7-cbf4-4e1c-bc7d-3d722f2b6e7c", "Manager", "MANAGER" },
                    { new Guid("35de8211-62d9-43c2-a3d0-d3bb50b44bf6"), "95e3bd43-c84e-4258-b635-45d96f43c416", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublisherId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("16997a3c-43a1-409e-95e2-06d4e9aebfe2"), 0, "049d49b1-5e4d-4c8e-8f48-0bd0958fd65c", "user@gmail.com", false, false, null, "USER@GMAIL.COM", "USER", null, null, false, null, null, false, "User" },
                    { new Guid("e2523839-7358-41e6-85ed-a0c48ef27e47"), 0, "1b8bb880-1c7b-411d-b1ad-07fd04e892f4", "moderator@gmail.com", false, false, null, "MODERATOR@GMAIL.COM", "MODERATOR", null, null, false, null, null, false, "Moderator" },
                    { new Guid("cc0bb8a8-48f0-49fd-a1d5-08578f0a4cdb"), 0, "ae308e6b-b814-40f6-bb01-ab3616ef0fca", "user@gmail.com", false, false, null, "USER@GMAIL.COM", "USER", null, null, false, null, null, false, "Manager" },
                    { new Guid("5ba77e7d-788a-403e-982c-29403d4d6dd2"), 0, "0c081c98-fabb-4d0e-a2c3-bc67109e7aac", "admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN", null, null, false, null, null, false, "Admin" }
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
