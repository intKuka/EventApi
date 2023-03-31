using EventsApi.Features.Tickets;
using MongoDB.Bson.Serialization.Attributes;

namespace EventsApi.Features.Events
{
    public class Event
    {
        [BsonId]
        public Guid Id { get; set; }
        public DateTime Starts { get; set; } = DateTime.UtcNow;
        public DateTime Ends { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid? ImageId { get; set; }
        public Guid SpaceId { get; set; }
        public int TicketsQuantity { get; set; }
        public List<Ticket> TicketList { get; set; } = new();
        public bool HasNumeration { get; set; }
        public decimal Price { get; set; }

        
    }
}
