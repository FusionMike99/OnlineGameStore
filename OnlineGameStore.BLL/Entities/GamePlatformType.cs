﻿using System;

namespace OnlineGameStore.BLL.Entities
{
    public class GamePlatformType
    {
        public Guid GameId { get; set; }

        public Game Game { get; set; }

        public Guid PlatformId { get; set; }

        public PlatformType PlatformType { get; set; }
    }
}