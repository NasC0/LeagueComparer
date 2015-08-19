using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Models.Common
{
    public class Entity
    {
        public Entity()
        {
            this.Added = DateTime.Now;
        }

        [BsonId]
        public string Id { get; set; }

        [BsonDateTimeOptions(Kind=DateTimeKind.Local)]
        public DateTime Added { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Modified { get; set; }
    }
}
