﻿using System;

namespace OnlineGameStore.DAL.Entities
{
    public class GamePlatformTypeEntity : BaseEntity
    {
        public Guid GameId { get; set; }

        public GameEntity Game { get; set; }

        public Guid PlatformId { get; set; }

        public PlatformTypeEntity PlatformType { get; set; }
    }
}