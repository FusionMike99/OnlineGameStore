using System;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Entities
{
    public class Game : IBaseEntity<int>
    {
        public int Id { get; set; }
        
        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

        public int? PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<GameGenre> GameGenres { get; set; }

        public ICollection<GamePlatformType> GamePlatformTypes { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}