using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DomainModels.Utils;

namespace OnlineGameStore.DAL.Data
{
    internal static class ModelBuilderExtensions
    {
        private static readonly IEnumerable<GenreEntity> Genres;
        private static readonly IEnumerable<PlatformTypeEntity> PlatformTypes;
        private static readonly IEnumerable<PublisherEntity> Publishers;
        private static readonly IEnumerable<GameEntity> Games;
        private static readonly IEnumerable<GameGenreEntity> GameGenres;
        private static readonly IEnumerable<GamePlatformTypeEntity> GamePlatformTypes;

        static ModelBuilderExtensions()
        {
            Genres = new[]
            {
                new GenreEntity { Id = Guid.Parse("8978F06E-A703-4746-BBE7-CC16A7E0249E"), Name = "Strategy" },
                new GenreEntity
                {
                    Id = Guid.Parse("46CC32CE-DB19-4C56-86DA-19713DDF6976"), Name = "RTS",
                    ParentId = Guid.Parse("8978F06E-A703-4746-BBE7-CC16A7E0249E")
                },
                new GenreEntity
                {
                    Id = Guid.Parse("773EC3E3-67FA-4A45-8D3A-1AD616B8913F"), Name = "TBS",
                    ParentId = Guid.Parse("8978F06E-A703-4746-BBE7-CC16A7E0249E")
                },
                new GenreEntity { Id = Guid.Parse("E49F4755-02D6-444A-B25C-9E65C5298CC5"), Name = "RPG" },
                new GenreEntity { Id = Guid.Parse("CE5B782D-D609-4BD5-A1C2-230861D63E05"), Name = "Sports" },
                new GenreEntity { Id = Guid.Parse("CB800C29-33E6-439E-B05E-657730C43213"), Name = "Races" },
                new GenreEntity
                {
                    Id = Guid.Parse("45E4E6B3-DBF6-4BD7-9349-174AA48AB2A4"), Name = "Rally",
                    ParentId = Guid.Parse("CB800C29-33E6-439E-B05E-657730C43213")
                },
                new GenreEntity
                {
                    Id = Guid.Parse("94A5F76B-CC14-447D-B9FD-BABD65D6F4D4"), Name = "Arcade",
                    ParentId = Guid.Parse("CB800C29-33E6-439E-B05E-657730C43213")
                },
                new GenreEntity
                {
                    Id = Guid.Parse("3B9515A5-CB4E-4667-8FF9-36AC005DB45F"), Name = "Formula",
                    ParentId = Guid.Parse("CB800C29-33E6-439E-B05E-657730C43213")
                },
                new GenreEntity
                {
                    Id = Guid.Parse("D4937448-4B9D-4277-BC77-A6ED012F0403"), Name = "Off-road",
                    ParentId = Guid.Parse("CB800C29-33E6-439E-B05E-657730C43213")
                },
                new GenreEntity { Id = Guid.Parse("AE4C8069-231A-4030-8202-907A2A548792"), Name = "Action" },
                new GenreEntity
                {
                    Id = Guid.Parse("59800DD6-A511-47B3-9420-60376C6A813D"), Name = "FPS",
                    ParentId = Guid.Parse("AE4C8069-231A-4030-8202-907A2A548792")
                },
                new GenreEntity
                {
                    Id = Guid.Parse("CBAB23A6-5122-4A72-995C-D3CD8D5AD6C3"), Name = "TPS",
                    ParentId = Guid.Parse("AE4C8069-231A-4030-8202-907A2A548792")
                },
                new GenreEntity { Id = Guid.Parse("2D96D846-DD30-4982-95EA-1BF4AADF38F9"), Name = "Adventure" },
                new GenreEntity { Id = Guid.Parse("8F9804AE-FCA1-4117-B89E-DB24F038F30E"), Name = "Puzzle & Skill" },
                new GenreEntity { Id = Guid.Parse("90649709-A022-415F-9E4B-45F4F9AFFC78"), Name = "Misc." }
            };

            PlatformTypes = new[]
            {
                new PlatformTypeEntity { Id = Guid.Parse("0D879FBC-75D9-4DF6-8EA2-2C0484207E20"), Type = "Mobile" },
                new PlatformTypeEntity { Id = Guid.Parse("46C32720-7400-4E51-82BB-96D35D4EFF18"), Type = "Browser" },
                new PlatformTypeEntity { Id = Guid.Parse("9F07B51A-F2CB-4C1B-ADA4-B4EBB652CE0B"), Type = "Desktop" },
                new PlatformTypeEntity { Id = Guid.Parse("8DAC1629-29CE-4054-89F0-6D5BBA95280F"), Type = "Console" }
            };

            Publishers = new[]
            {
                new PublisherEntity
                {
                    Id = Guid.Parse("8B750162-60AB-4DE9-A58D-F7EA9FBD0321"),
                    CompanyName = "CD Projekt RED",
                    Description = "Develop Witcher",
                    HomePage = "https://en.cdprojektred.com/"
                },
                new PublisherEntity
                {
                    Id = Guid.Parse("F02A54AE-B7B2-4904-9B67-C14F5C9622A7"),
                    CompanyName = "Bethesda Softworks",
                    Description = "Develop The Elder Scrolls",
                    HomePage = "https://bethesda.net/dashboard"
                },
                new PublisherEntity
                {
                    Id = Guid.Parse("EAAD9F95-7976-4893-A1ED-17C1679F2728"),
                    CompanyName = "THQ Nordic",
                    Description = "Develop Star Wars",
                    HomePage = "https://www.thqnordic.com/"
                }
            };

            Games = new[]
            {
                new GameEntity
                {
                    Id = Guid.Parse("94C979FA-20E5-412E-895B-A694B94F5AD4"),
                    Name = "The Witcher 3",
                    Key = "the-witcher-3",
                    Description =
                        "As war rages on throughout the Northern Realms, you take on the greatest contract of your life — tracking down the Child of Prophecy, a living weapon that can alter the shape of the world.",
                    Price = 49.99M,
                    UnitsInStock = 50,
                    Discontinued = false,
                    QuantityPerUnit = "units",
                    PublisherName = "CD Projekt RED",
                    DateAdded = new DateTime(2022, 5, 6),
                    DatePublished = new DateTime(2015, 5, 18)
                }
            };

            GameGenres = new[]
            {
                new GameGenreEntity
                {
                    Id = Guid.Parse("297C1914-86E5-4F50-A6D4-D0A83CCE8C7B"),
                    GameId = Guid.Parse("94C979FA-20E5-412E-895B-A694B94F5AD4"),
                    GenreId = Guid.Parse("E49F4755-02D6-444A-B25C-9E65C5298CC5")
                },
                new GameGenreEntity 
                {
                    Id = Guid.Parse("54716B49-D5E9-44D0-9C11-559D8B17D3DC"),
                    GameId = Guid.Parse("94C979FA-20E5-412E-895B-A694B94F5AD4"),
                    GenreId = Guid.Parse("2D96D846-DD30-4982-95EA-1BF4AADF38F9") 
                }
            };

            GamePlatformTypes = new[]
            {
                new GamePlatformTypeEntity
                {
                    Id = Guid.Parse("639847CA-2477-44FD-82C3-31204BE47F80"),
                    GameId = Guid.Parse("94C979FA-20E5-412E-895B-A694B94F5AD4"),
                    PlatformId = Guid.Parse("9F07B51A-F2CB-4C1B-ADA4-B4EBB652CE0B")
                },
                new GamePlatformTypeEntity
                {
                    Id = Guid.Parse("07BCAD23-3D65-43AD-B930-BE5042BD77BE"),
                    GameId = Guid.Parse("94C979FA-20E5-412E-895B-A694B94F5AD4"),
                    PlatformId = Guid.Parse("8DAC1629-29CE-4054-89F0-6D5BBA95280F")
                }
            };
        }

        internal static void StoreSeed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenreEntity>().HasData(Genres);
            modelBuilder.Entity<PlatformTypeEntity>().HasData(PlatformTypes);
            modelBuilder.Entity<PublisherEntity>().HasData(Publishers);
            modelBuilder.Entity<GameEntity>().HasData(Games);
            modelBuilder.Entity<GameGenreEntity>().HasData(GameGenres);
            modelBuilder.Entity<GamePlatformTypeEntity>().HasData(GamePlatformTypes);
            //GenerateFakeData(modelBuilder);
        }

        private static void GenerateFakeData(ModelBuilder modelBuilder)
        {
            var fakeGame = new Faker<GameEntity>()
                .UseSeed(333)
                .RuleFor(g => g.Id, f => f.Random.Guid())
                .RuleFor(g => g.Name, f => f.Commerce.ProductName())
                .RuleFor(g => g.Key, (f, g) => g.Name.ToKebabCase())
                .RuleFor(g => g.Description, f => f.Commerce.ProductDescription())
                .RuleFor(g => g.Price, f => f.Random.Decimal(25M, 250M))
                .RuleFor(g => g.UnitsInStock, f => f.Random.Short(1, 100))
                .RuleFor(g => g.Discontinued, f => f.Random.Bool())
                .RuleFor(g => g.DateAdded, f => f.Date.Recent(5, new DateTime(2022, 07, 14)))
                .RuleFor(g => g.DatePublished, f => f.Date.Past(3, new DateTime(2022, 07, 14)))
                .RuleFor(g => g.ViewsNumber, f => f.Random.ULong(0, 100000L))
                .RuleFor(g => g.QuantityPerUnit, f => f.Random.Words())
                .RuleFor(g => g.PublisherName, f => f.PickRandom(Publishers).CompanyName);
            
            var games = fakeGame.Generate(1000)
                .GroupBy(g => new { g.Key })
                .Select(c => c.FirstOrDefault())
                .ToList();

            modelBuilder.Entity<GameEntity>()
                .HasData(games);

            var fakeGameGenre = new Faker<GameGenreEntity>()
                .UseSeed(666)
                .RuleFor(gg => gg.Id, f => f.Random.Guid())
                .RuleFor(gg => gg.GameId, f => f.PickRandom(games).Id)
                .RuleFor(gg => gg.GenreId, f => f.PickRandom(Genres).Id);

            var gameGenres = fakeGameGenre.Generate(5000)
                .GroupBy(c => new { c.GameId, c.GenreId })
                .Select(c => c.FirstOrDefault())
                .ToList();
            
            modelBuilder.Entity<GameGenreEntity>()
                .HasData(gameGenres);
            
            var fakeGamePlatformType = new Faker<GamePlatformTypeEntity>()
                .UseSeed(777)
                .RuleFor(gg => gg.Id, f => f.Random.Guid())
                .RuleFor(gg => gg.GameId, f => f.PickRandom(games).Id)
                .RuleFor(gg => gg.PlatformId, f => f.PickRandom(PlatformTypes).Id);
            
            var gamePlatformTypes = fakeGamePlatformType.Generate(5000)
                .GroupBy(c => new { c.GameId, c.PlatformId })
                .Select(c => c.FirstOrDefault())
                .ToList();
            
            modelBuilder.Entity<GamePlatformTypeEntity>()
                .HasData(gamePlatformTypes);
        }
    }
}