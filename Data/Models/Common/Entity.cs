using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models.Common
{
    public class Entity
    {
        public Entity()
        {
            this.Added = DateTime.Now;
            this.Id = ObjectId.GenerateNewId().ToString();
        }

        [BsonId]
        public string Id { get; set; }

        [BsonDateTimeOptions(Kind=DateTimeKind.Local)]
        public DateTime Added { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Modified { get; set; }
    }
}
