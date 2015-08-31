using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models.Common
{
    [Serializable]
    public class Entity
    {
        public Entity()
        {
            this.Added = DateTime.Now;
            this._id = ObjectId.GenerateNewId().ToString();
        }

        [BsonId]
        public string _id { get; set; }

        [BsonDateTimeOptions(Kind=DateTimeKind.Local)]
        public DateTime Added { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Modified { get; set; }
    }
}
