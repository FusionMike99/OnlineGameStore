using Microsoft.EntityFrameworkCore;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.DAL.Data
{
    internal static class ModelBuilderExtensions
    {
        internal static void StoreSeed(this ModelBuilder modelBuilder)
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
                new Genre { Id = 14, Name = "Misc.", ParentId = 11 },
                new Genre { Id = 15, Name = "Adventure" },
                new Genre { Id = 16, Name = "Puzzle & Skill" });

            modelBuilder.Entity<PlatformType>().HasData(
                new PlatformType { Id = 1, Type = "Mobile" },
                new PlatformType { Id = 2, Type = "Browser" },
                new PlatformType { Id = 3, Type = "Desktop" },
                new PlatformType { Id = 4, Type = "Console" });
        }
    }
}
