using MongoDB.Bson.Serialization.Attributes;

namespace EventsApi.Features.Models
{
    public class Ticket
    {
        [BsonId]
        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid Owner { get; set; } = Guid.Empty;
        public int? Seat { get; set; }
    }
}
