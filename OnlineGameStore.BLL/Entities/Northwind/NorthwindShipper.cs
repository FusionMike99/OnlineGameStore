using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineGameStore.BLL.Entities.Northwind
{
    public class NorthwindShipper : INorthwindBaseEntity<ObjectId>
    {
        public ObjectId Id { get; set; }
        
        public string CompanyName { get; set; }
        
        public string Phone { get; set; }
        
        [BsonElement("ShipperID")]
        public int ShipperId { get; set; }
    }
}