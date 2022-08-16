using MongoDB.Bson.Serialization.Attributes;

namespace OnlineGameStore.DAL.Entities.Northwind
{
    public class CategoryEntity : MongoBaseEntity
    {
        [BsonElement("CategoryID")]
        public int CategoryId { get; set; }
        
        [BsonElement("CategoryName")]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public byte[] Picture { get; set; }
    }
}