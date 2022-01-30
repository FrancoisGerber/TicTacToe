using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Models
{
    public class Game
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string GameMode { get; set; }

        public string? PlayerXId { get; set; }
        
        public List<PlayerHistory> PlayerXHistory { get; set; }

        public string? PlayerOId { get; set; }

        public List<PlayerHistory> PlayerOHistory { get; set; }

        public List<int> PlayedPositions { get; set; }

        public char Winner { get; set; }

        public bool Completed { get; set; }      

    }
}