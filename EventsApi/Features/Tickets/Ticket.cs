using JetBrains.Annotations;
using MongoDB.Bson.Serialization.Attributes;

namespace EventsApi.Features.Tickets;

public class Ticket
{
    [BsonId]
    [UsedImplicitly]
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid Owner { get; set; } = Guid.Empty;
    public int? Seat { get; set; }
}