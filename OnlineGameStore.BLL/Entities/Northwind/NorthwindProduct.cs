using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OnlineGameStore.BLL.Utils;

namespace OnlineGameStore.BLL.Entities.Northwind
{
    public class NorthwindProduct : MongoBaseEntity
    {
        [BsonElement("ProductID")]
        public int ProductId { get; set; }
        
        [Updatable]
        public string Key { get; set; }

        [BsonElement("ProductName")]
        public string Name { get; set; }

        [BsonElement("UnitPrice")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        [Updatable]
        public short UnitsInStock { get; set; }

        public bool Discontinued { get; set; }
        
        [Updatable]
        public ulong ViewsNumber { get; set; }

        public string QuantityPerUnit { get; set; }
        
        public int UnitsOnOrder { get; set; }
        
        public int ReorderLevel { get; set; }
        
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? DateAdded { get; set; }
        
        [BsonElement("SupplierID")]
        public int SupplierId { get; set; }
        
        public NorthwindSupplier Supplier { get; set; }
        
        [BsonElement("CategoryID")]
        public int CategoryId { get; set; }
        
        public NorthwindCategory Category { get; set; }
    }
}