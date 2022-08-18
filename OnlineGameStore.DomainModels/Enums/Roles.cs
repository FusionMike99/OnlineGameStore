using System;

namespace OnlineGameStore.DomainModels.Enums
{
    [Flags]
    public enum Roles : byte
    {
        User = 1,
        Publisher = 2,
        Moderator = 4,
        Manager = 8,
        Admin = 16
    }
}