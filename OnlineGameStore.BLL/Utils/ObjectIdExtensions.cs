using System;
using System.Linq;
using MongoDB.Bson;

namespace OnlineGameStore.BLL.Utils
{
    public static class ObjectIdExtensions
    {
        public static Guid AsGuid(this ObjectId objectId)
        {
            var bytes = objectId.ToByteArray().Concat(new byte[] { 1, 2, 3, 4 }).ToArray();
            var guid = new Guid(bytes);
            return guid;
        }
    }
}