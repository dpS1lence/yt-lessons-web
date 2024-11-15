using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MoodifyAPI.Models
{
    public class Playlist
    {
        public Playlist()
        {
            Songs = new HashSet<Song>();
        }

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement]
        public string Name { get; set; } = default!;

        [BsonElement]
        public IEnumerable<Song> Songs { get; set; }
    }
}
