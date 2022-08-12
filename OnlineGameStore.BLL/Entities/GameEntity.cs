using System;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Entities
{
    public class GameEntity : BaseEntity
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discontinued { get; set; }
        
        public DateTime? DateAdded { get; set; }

        public DateTime? DatePublished { get; set; }
        
        public ulong ViewsNumber { get; set; }

        public string QuantityPerUnit { get; set; }
        
        public string PublisherName { get; set; }

        public PublisherEntity Publisher { get; set; }

        public ICollection<CommentEntity> Comments { get; set; }

        public ICollection<GameGenreEntity> GameGenres { get; set; }

        public ICollection<GamePlatformTypeEntity> GamePlatformTypes { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}