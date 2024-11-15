using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MoodifyAPI.Models
{
    public class Song
    {
        [BsonId] 
        public ObjectId Id { get; set; }

        [BsonElement]
        public string Name { get; set; } = default!;

        [BsonElement]
        public string Author { get; set; } = default!;
    }
}
