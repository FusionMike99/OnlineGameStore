using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineGameStore.DAL.Migrations
{
    public partial class AddTriggers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE TRIGGER
                Publishers_Update on Publishers AFTER UPDATE AS
                DECLARE @NewPublisherName nvarchar(max)
                DECLARE @OldPublisherName nvarchar(max)
                SELECT @NewPublisherName=CompanyName from inserted
                SELECT @OldPublisherName=CompanyName from deleted
                if @OldPublisherName!=@NewPublisherName
                    Update Games
                    Set PublisherName=@NewPublisherName
                    Where PublisherName=@OldPublisherName;");
            
            migrationBuilder.Sql(@"CREATE TRIGGER
                Games_Update on Games AFTER UPDATE AS
                DECLARE @NewGameKey nvarchar(max)
                DECLARE @OldGameKey nvarchar(max)
                SELECT @NewGameKey=[Key] from inserted
                SELECT @OldGameKey=[Key] from deleted
                if @OldGameKey!=@NewGameKey
                    Update OrderDetail
                    Set GameKey=@NewGameKey
                    Where GameKey=@OldGameKey;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER Games_Update;");
            
            migrationBuilder.Sql(@"DROP TRIGGER Publishers_Update;");
        }
    }
}
