using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BemolChallenge.PaymentServiceB.Models
{
    public class Payment
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("uuid"), BsonRepresentation(BsonType.String)]
        public string Uuid { get; set; }

        [BsonElement("description"), BsonRepresentation(BsonType.String)]
        public string? Description { get; set; }

        [BsonElement("amount"), BsonRepresentation(BsonType.Double)]
        public decimal Amount { get; set; }

        [BsonElement("currency"), BsonRepresentation(BsonType.String)]
        public string Currency { get; set; }

        [BsonElement("created_at"), BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("status"), BsonRepresentation(BsonType.String)]
        public string Status { get; set; }

        [BsonElement("processed_at"), BsonRepresentation(BsonType.DateTime)]
        public DateTime ProcessedAt { get; set; }
    }
}
