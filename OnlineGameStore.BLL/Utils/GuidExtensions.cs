using System;
using System.Linq;
using MongoDB.Bson;

namespace OnlineGameStore.BLL.Utils
{
    public static class GuidExtensions
    {
        public static ObjectId AsObjectId(this Guid guid)
        {
            var bytes = guid.ToByteArray().Take(12).ToArray();
            var objectId = new ObjectId(bytes);
            return objectId;
        }
    }
}