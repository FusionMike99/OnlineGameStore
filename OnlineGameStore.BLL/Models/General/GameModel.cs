using System;
using System.Collections.Generic;
using OnlineGameStore.BLL.Enums;

namespace OnlineGameStore.BLL.Models.General
{
    public class GameModel : BaseModel
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
        
        public int UnitsOnOrder { get; set; }
        
        public int ReorderLevel { get; set; }
        
        public string PublisherName { get; set; }

        public PublisherModel Publisher { get; set; }

        public ICollection<CommentModel> Comments { get; set; }

        public ICollection<GameGenreModel> GameGenres { get; set; }

        public ICollection<GamePlatformTypeModel> GamePlatformTypes { get; set; }

        public ICollection<OrderDetailModel> OrderDetails { get; set; }

        public DatabaseEntity DatabaseEntity { get; set; }
    }
}