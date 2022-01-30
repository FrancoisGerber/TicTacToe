using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Models
{
    public class AuditLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public string OldObject { get; set; }
        
        public string NewObject { get; set; }
        
        public string CollectionName { get; set; }
                
    }
}