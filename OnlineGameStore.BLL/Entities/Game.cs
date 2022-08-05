﻿using System;
using System.Collections.Generic;
using OnlineGameStore.BLL.Enums;

namespace OnlineGameStore.BLL.Entities
{
    public class Game : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        
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

        public Publisher Publisher { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<GameGenre> GameGenres { get; set; }

        public ICollection<GamePlatformType> GamePlatformTypes { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public DatabaseEntity DatabaseEntity { get; set; }
    }
}