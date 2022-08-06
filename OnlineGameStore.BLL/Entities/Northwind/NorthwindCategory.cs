using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineGameStore.BLL.Entities.Northwind
{
    public class NorthwindCategory : NorthwindBaseEntity
    {
        [BsonElement("CategoryID")]
        public int CategoryId { get; set; }
        
        [BsonElement("CategoryName")]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public byte[] Picture { get; set; }
    }
}