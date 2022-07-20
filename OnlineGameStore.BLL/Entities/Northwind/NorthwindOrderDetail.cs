using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineGameStore.BLL.Entities.Northwind
{
    public class NorthwindOrderDetail: INorthwindBaseEntity<ObjectId>
    {
        public ObjectId Id { get; set; }
        
        [BsonElement("OrderID")]
        public int OrderId { get; set; }
        
        [BsonElement("ProductID")]
        public int ProductId { get; set; }

        [BsonElement("UnitPrice")]
        public decimal Price { get; set; }

        public short Quantity { get; set; }

        [BsonRepresentation(BsonType.Double)]
        public double Discount { get; set; }
    }
}