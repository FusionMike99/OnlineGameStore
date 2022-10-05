using OnlineGameStore.DomainModels.Enums;

namespace OnlineGameStore.DomainModels.Constants
{
    public static class Permissions
    {
        public const Roles AdminPermission = Roles.Admin;
        public const Roles ManagerPermission = Roles.Admin | Roles.Manager;
        public const Roles ModeratorPermission = Roles.Admin | Roles.Manager | Roles.Moderator;
        public const Roles PublisherPermission = Roles.Admin | Roles.Manager | Roles.Publisher;
        public const Roles UserPermission = Roles.Admin | Roles.Manager | Roles.Moderator | Roles.Publisher | Roles.User;
    }
}