using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChangFei.Grains.Entity
{
    public class BaseEntity
    {
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; }

        public BaseEntity(string id)
        {
            Id = id;
        }
    }
}
