using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineGameStore.DAL.Entities.Northwind
{
    public abstract class MongoBaseEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}