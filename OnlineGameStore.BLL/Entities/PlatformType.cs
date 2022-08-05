﻿using System;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Entities
{
    public class PlatformType : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        
        public string Type { get; set; }

        public ICollection<GamePlatformType> GamePlatformTypes { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}