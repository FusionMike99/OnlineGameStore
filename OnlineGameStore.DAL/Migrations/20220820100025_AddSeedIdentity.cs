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
                    { new Guid("274fcde0-2619-41c9-9772-6e964fa0ef7d"), "5d000d91-f28c-4641-8631-82e122962dbb", "User", "USER" },
                    { new Guid("3ae74d1f-43a3-43ef-bce2-0316994a2d0c"), "44c5f952-0a4b-4185-b853-b15e822a71ec", "Publisher", "PUBLISHER" },
                    { new Guid("26bf4c5d-7779-4e3a-ad66-a59054e69d60"), "abc49135-8728-4ec6-a096-9efc5145a083", "Moderator", "MODERATOR" },
                    { new Guid("3b175e2e-ba5f-4c9a-9a80-816c85065ecb"), "2847bb06-ce64-4579-a88b-b48d157f856b", "Manager", "MANAGER" },
                    { new Guid("35de8211-62d9-43c2-a3d0-d3bb50b44bf6"), "c2476c7b-d88a-4c0d-8826-d224525158ea", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PublisherId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("16997a3c-43a1-409e-95e2-06d4e9aebfe2"), 0, "d04621be-6e82-474a-98b6-473efe8bd2ed", "user@gmail.com", false, false, null, "USER@GMAIL.COM", "USER", "AQAAAAEAACcQAAAAEJYENB35bdztqN7tM2XpGuNiMWFVWS0MQ75g62NI1NYBKtQ6dLu2PtWR+Aw5ZQj2Rw==", null, false, null, "93BF6096-EA7C-4BB6-B450-45524E43312E", false, "User" },
                    { new Guid("e2523839-7358-41e6-85ed-a0c48ef27e47"), 0, "e9735ae6-43d1-4c92-8359-5888532f1b4f", "moderator@gmail.com", false, false, null, "MODERATOR@GMAIL.COM", "MODERATOR", "AQAAAAEAACcQAAAAEPh8NxI2TxtD0IOQkOtiUnV/FLIKmWF1LHcgZHtzx9mGZKd5S+hicKRibIs9mJMITw==", null, false, null, "37037D92-39E2-41E0-B80A-38E2026B2280", false, "Moderator" },
                    { new Guid("cc0bb8a8-48f0-49fd-a1d5-08578f0a4cdb"), 0, "bd91f492-9d93-42c0-955d-e10700cf5c17", "manager@gmail.com", false, false, null, "MANAGER@GMAIL.COM", "MANAGER", "AQAAAAEAACcQAAAAEJR5w4lKnIayWqsNFM7h7mIIDitx/A3ssbZjQ7gdssiYVR0fNh0GhjuTfYJ1dXZYXQ==", null, false, null, "3E5BC2B0-39EA-4F66-8BDF-C12EAA89F5DD", false, "Manager" },
                    { new Guid("5ba77e7d-788a-403e-982c-29403d4d6dd2"), 0, "0352bfc4-20dd-4b1d-8472-a8c4b6adc868", "admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEFPU6PVz666a59OuDJzYimxz2jN9pcxQt6qsFbSKMYcwxqIybYJDWMyKERadjsbnvA==", null, false, null, "67B7C86D-0D3F-492E-B499-078485DDEFC5", false, "Admin" }
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
