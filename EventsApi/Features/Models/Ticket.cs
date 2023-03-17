namespace EventsApi.Features.Models
{
    public class Ticket
    {
        public Guid Id { get; init; }
        public Guid Owner { get; set; } = Guid.Empty;
        public int Seat { get; set; }
    }
}
