using MongoDB.Bson.Serialization.Attributes;

namespace OnlineGameStore.DAL.Entities.Northwind
{
    public class ShipperEntity : MongoBaseEntity
    {
        public string CompanyName { get; set; }
        
        public string Phone { get; set; }
        
        [BsonElement("ShipperID")]
        public int ShipperId { get; set; }
    }
}