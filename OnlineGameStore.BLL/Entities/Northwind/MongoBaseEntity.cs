using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineGameStore.BLL.Entities.Northwind
{
    public abstract class MongoBaseEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}