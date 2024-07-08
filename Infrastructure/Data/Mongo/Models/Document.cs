using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Data.Mongo.Models
{
    public class Document
    {
        [BsonId]
        public string Id { get; set; } = null!;

        [BsonElement("tags")]
        public List<string> Tags { get; set; } = null!;

        [BsonElement("data")]
        public string Data { get; set; } = null!;
    }
}