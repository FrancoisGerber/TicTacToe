using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL.Models
{
    public class ErrorLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ExceptionMessage { get; set; }

        public string? ExceptionStackTrace { get; set; }

    }
}