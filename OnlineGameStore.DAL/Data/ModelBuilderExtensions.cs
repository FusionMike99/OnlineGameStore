using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.ExtensionsUtility.Extensions;

namespace OnlineGameStore.DAL.Data
{
    internal static class ModelBuilderExtensions
    {
        private static readonly List<GenreEntity> Genres;
        private static readonly List<PlatformTypeEntity> PlatformTypes;
        private static readonly List<PublisherEntity> Publishers;
        private static readonly List<GameEntity> Games;
        private static readonly List<GameGenreEntity> GameGenres;
        private static readonly List<GamePlatformTypeEntity> GamePlatformTypes;
        private static readonly List<IdentityRole<Guid>> Roles;
        private static readonly List<UserEntity> Users;
        private static readonly List<IdentityUserRole<Guid>> UserRoles;

        static ModelBuilderExtensions()
        {
            Genres = new List<GenreEntity>
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

            PlatformTypes = new List<PlatformTypeEntity>
            {
                new PlatformTypeEntity { Id = Guid.Parse("0D879FBC-75D9-4DF6-8EA2-2C0484207E20"), Type = "Mobile" },
                new PlatformTypeEntity { Id = Guid.Parse("46C32720-7400-4E51-82BB-96D35D4EFF18"), Type = "Browser" },
                new PlatformTypeEntity { Id = Guid.Parse("9F07B51A-F2CB-4C1B-ADA4-B4EBB652CE0B"), Type = "Desktop" },
                new PlatformTypeEntity { Id = Guid.Parse("8DAC1629-29CE-4054-89F0-6D5BBA95280F"), Type = "Console" }
            };

            Publishers = new List<PublisherEntity>
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

            Games = new List<GameEntity>
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

            GameGenres = new List<GameGenreEntity>
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

            GamePlatformTypes = new List<GamePlatformTypeEntity>
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

            Roles = new List<IdentityRole<Guid>>
            {
                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("274FCDE0-2619-41C9-9772-6E964FA0EF7D"),
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("3AE74D1F-43A3-43EF-BCE2-0316994A2D0C"),
                    Name = "Publisher",
                    NormalizedName = "PUBLISHER"
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("26BF4C5D-7779-4E3A-AD66-A59054E69D60"),
                    Name = "Moderator",
                    NormalizedName = "MODERATOR"
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("3B175E2E-BA5F-4C9A-9A80-816C85065ECB"),
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("35DE8211-62D9-43C2-A3D0-D3BB50B44BF6"),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            };

            Users = new List<UserEntity>
            {
                new UserEntity
                {
                    Id = Guid.Parse("16997A3C-43A1-409E-95E2-06D4E9AEBFE2"),
                    UserName = "User",
                    NormalizedUserName = "USER",
                    Email = "user@gmail.com",
                    NormalizedEmail = "USER@GMAIL.COM",
                    SecurityStamp = "93BF6096-EA7C-4BB6-B450-45524E43312E"
                },
                new UserEntity
                {
                    Id = Guid.Parse("E2523839-7358-41E6-85ED-A0C48EF27E47"),
                    UserName = "Moderator",
                    NormalizedUserName = "MODERATOR",
                    Email = "moderator@gmail.com",
                    NormalizedEmail = "MODERATOR@GMAIL.COM",
                    SecurityStamp = "37037D92-39E2-41E0-B80A-38E2026B2280"
                },
                new UserEntity
                {
                    Id = Guid.Parse("CC0BB8A8-48F0-49FD-A1D5-08578F0A4CDB"),
                    UserName = "Manager",
                    NormalizedUserName = "MANAGER",
                    Email = "manager@gmail.com",
                    NormalizedEmail = "MANAGER@GMAIL.COM",
                    SecurityStamp = "3E5BC2B0-39EA-4F66-8BDF-C12EAA89F5DD"
                },
                new UserEntity
                {
                    Id = Guid.Parse("5BA77E7D-788A-403E-982C-29403D4D6DD2"),
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    SecurityStamp = "67B7C86D-0D3F-492E-B499-078485DDEFC5"
                }
            };

            UserRoles = new List<IdentityUserRole<Guid>>
            {
                new IdentityUserRole<Guid>
                {
                    UserId = Guid.Parse("16997A3C-43A1-409E-95E2-06D4E9AEBFE2"),
                    RoleId = Guid.Parse("274FCDE0-2619-41C9-9772-6E964FA0EF7D")
                },
                new IdentityUserRole<Guid>
                {
                    UserId = Guid.Parse("E2523839-7358-41E6-85ED-A0C48EF27E47"),
                    RoleId = Guid.Parse("26BF4C5D-7779-4E3A-AD66-A59054E69D60")
                },
                new IdentityUserRole<Guid>
                {
                    UserId = Guid.Parse("CC0BB8A8-48F0-49FD-A1D5-08578F0A4CDB"),
                    RoleId = Guid.Parse("3B175E2E-BA5F-4C9A-9A80-816C85065ECB")
                },
                new IdentityUserRole<Guid>
                {
                    UserId = Guid.Parse("5BA77E7D-788A-403E-982C-29403D4D6DD2"),
                    RoleId = Guid.Parse("35DE8211-62D9-43C2-A3D0-D3BB50B44BF6")
                }
            };
        }

        internal static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenreEntity>().HasData(Genres);
            modelBuilder.Entity<PlatformTypeEntity>().HasData(PlatformTypes);
            modelBuilder.Entity<PublisherEntity>().HasData(Publishers);
            modelBuilder.Entity<GameEntity>().HasData(Games);
            modelBuilder.Entity<GameGenreEntity>().HasData(GameGenres);
            modelBuilder.Entity<GamePlatformTypeEntity>().HasData(GamePlatformTypes);
            
            modelBuilder.SeedRoles();
            modelBuilder.SeedUsers();
            modelBuilder.SeedUserRoles();
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

        private static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(Roles);
        }
        
        private static void SeedUsers(this ModelBuilder modelBuilder)
        {
            const string password = "pa$$w0rd";
            var passwordHasher = new PasswordHasher<UserEntity>();
            Users.ForEach(u => u.PasswordHash = passwordHasher.HashPassword(u, password));
            modelBuilder.Entity<UserEntity>().HasData(Users);
        }
        
        private static void SeedUserRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(UserRoles);
        }
    }
}