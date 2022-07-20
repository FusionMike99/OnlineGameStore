using System;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Enums;

namespace OnlineGameStore.BLL.Models
{
    public class LogModel : INorthwindBaseEntity<ObjectId>
    {
        [BsonId] 
        public ObjectId Id { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; } = DateTime.UtcNow;

        [BsonElement("action")]
        public string ActionType { get; set; }

        [BsonElement("entity-type")]
        public string EntityType { get; set;  }
        
        [BsonElement("old-object")]
        [BsonIgnoreIfNull]
        public BsonDocument OldObject { get; set; }
        
        [BsonElement("new-object")]
        [BsonIgnoreIfNull]
        public BsonDocument NewObject { get; set; }

        public LogModel()
        { }

        public LogModel(ActionTypes actionType, MemberInfo entityType,
            object oldObject = null, object newObject = null)
        {
            ActionType = actionType.ToString();
            EntityType = entityType.Name;
            
            var serializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

            OldObject = oldObject != null 
                ? BsonDocument.Parse(JsonConvert.SerializeObject(oldObject, serializerSettings))
                : null;
            NewObject = newObject != null 
                ? BsonDocument.Parse(JsonConvert.SerializeObject(newObject, serializerSettings))
                : null;
        }
    }
}