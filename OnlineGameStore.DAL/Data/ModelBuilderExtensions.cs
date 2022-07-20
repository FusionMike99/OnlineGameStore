using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Utils;

namespace OnlineGameStore.DAL.Data
{
    internal static class ModelBuilderExtensions
    {
        internal static void StoreSeed(this ModelBuilder modelBuilder,
            INorthwindUnitOfWork northwindUnitOfWork)
        {
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Strategy" },
                new Genre { Id = 2, Name = "RTS", ParentId = 1 },
                new Genre { Id = 3, Name = "TBS", ParentId = 1 },
                new Genre { Id = 4, Name = "RPG" },
                new Genre { Id = 5, Name = "Sports" },
                new Genre { Id = 6, Name = "Races" },
                new Genre { Id = 7, Name = "Rally", ParentId = 6 },
                new Genre { Id = 8, Name = "Arcade", ParentId = 6 },
                new Genre { Id = 9, Name = "Formula", ParentId = 6 },
                new Genre { Id = 10, Name = "Off-road", ParentId = 6 },
                new Genre { Id = 11, Name = "Action" },
                new Genre { Id = 12, Name = "FPS", ParentId = 11 },
                new Genre { Id = 13, Name = "TPS", ParentId = 11 },
                new Genre { Id = 14, Name = "Adventure" },
                new Genre { Id = 15, Name = "Puzzle & Skill" },
                new Genre { Id = 16, Name = "Misc." });

            var genreId = 17;
            var genres = northwindUnitOfWork.Categories.GetMany()
                .Select(category => new Genre 
                    { 
                        Id = genreId++,
                        Name = category.Name,
                        Description = category.Description
                    });

            modelBuilder.Entity<Genre>().HasData(genres);

            modelBuilder.Entity<PlatformType>().HasData(
                new PlatformType { Id = 1, Type = "Mobile" },
                new PlatformType { Id = 2, Type = "Browser" },
                new PlatformType { Id = 3, Type = "Desktop" },
                new PlatformType { Id = 4, Type = "Console" });

            modelBuilder.Entity<Publisher>().HasData(
                new Publisher
                {
                    Id = 1,
                    CompanyName = "CD Projekt RED",
                    Description = "Develop Witcher",
                    HomePage = "https://en.cdprojektred.com/"
                },
                new Publisher
                {
                    Id = 2,
                    CompanyName = "Bethesda Softworks",
                    Description = "Develop The Elder Scrolls",
                    HomePage = "https://bethesda.net/dashboard"
                },
                new Publisher
                {
                    Id = 3,
                    CompanyName = "THQ Nordic",
                    Description = "Develop Star Wars",
                    HomePage = "https://www.thqnordic.com/"
                }
            );

            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    Id = 1,
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
                });

            modelBuilder.Entity<GameGenre>().HasData(
                new GameGenre { GameId = 1, GenreId = 11 },
                new GameGenre { GameId = 1, GenreId = 14 });

            modelBuilder.Entity<GamePlatformType>().HasData(
                new GamePlatformType { GameId = 1, PlatformId = 3 },
                new GamePlatformType { GameId = 1, PlatformId = 4 });

            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus { Id = 1, Status = "Open" },
                new OrderStatus { Id = 2, Status = "In progress" },
                new OrderStatus { Id = 3, Status = "Cancelled" },
                new OrderStatus { Id = 4, Status = "Closed" });

            GenerateFakeData(modelBuilder);
        }

        private static void GenerateFakeData(ModelBuilder modelBuilder)
        {
            var gameId = 2;

            var publishers = new string[]
            {
                "CD Projekt RED", "Bethesda Softworks", "THQ Nordic"
            };

            var fakeGame = new Faker<Game>()
                .UseSeed(333)
                .RuleFor(g => g.Id, f => gameId++)
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
                .RuleFor(g => g.PublisherName, f => f.PickRandom(publishers));
            
            var games = fakeGame.Generate(1000)
                .GroupBy(g => new { g.Key })
                .Select(c => c.FirstOrDefault())
                .ToList();

            modelBuilder.Entity<Game>()
                .HasData(games);

            var fakeGameGenre = new Faker<GameGenre>()
                .UseSeed(666)
                .RuleFor(gg => gg.GameId, f => f.PickRandom(games).Id)
                .RuleFor(gg => gg.GenreId, f => f.Random.Int(1, 16));

            var gameGenres = fakeGameGenre.Generate(5000)
                .GroupBy(c => new { c.GameId, c.GenreId })
                .Select(c => c.FirstOrDefault())
                .ToList();
            
            modelBuilder.Entity<GameGenre>()
                .HasData(gameGenres);
            
            var fakeGamePlatformType = new Faker<GamePlatformType>()
                .UseSeed(777)
                .RuleFor(gg => gg.GameId, f => f.PickRandom(games).Id)
                .RuleFor(gg => gg.PlatformId, f => f.Random.Int(1, 4));
            
            var gamePlatformTypes = fakeGamePlatformType.Generate(5000)
                .GroupBy(c => new { c.GameId, c.PlatformId })
                .Select(c => c.FirstOrDefault())
                .ToList();
            
            modelBuilder.Entity<GamePlatformType>()
                .HasData(gamePlatformTypes);
        }
    }
}