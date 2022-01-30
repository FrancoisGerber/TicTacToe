using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Models
{
    public class Player
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        public string PlayerName { get; set; }
        
        public bool Active { get; set; }
        
    }
}