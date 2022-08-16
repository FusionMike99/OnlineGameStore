﻿using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineGameStore.DAL.Entities.Northwind
{
    public class OrderMongoDbEntity : MongoBaseEntity
    {
        [BsonElement("OrderID")]
        public int OrderId { get; set; }

        [BsonElement("CustomerID")]
        public string CustomerId { get; set; }
        
        [BsonElement("EmployeeID")]
        public int EmployeeId { get; set; }
        
        public DateTime OrderDate { get; set; }
        
        public DateTime? RequiredDate { get; set; }
        
        public DateTime? ShippedDate { get; set; }
        
        public decimal Freight { get; set; }
        
        public int ShipVia { get; set; }
        
        public string ShipName { get; set; }
        
        public string ShipAddress { get; set; }
        
        public string ShipCity { get; set; }
        
        public string ShipRegion { get; set; }
        
        public string ShipPostalCode { get; set; }
        
        public string ShipCountry { get; set; }
        
        [BsonIgnore]
        public IEnumerable<OrderDetailMongoDbEntity> OrderDetails { get; set; }
        
        [BsonIgnore]
        public ShipperEntity Shipper { get; set; }
    }
}