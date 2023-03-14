using System.ComponentModel.DataAnnotations;

namespace EventsApi.Features.Events
{
    public class Event
    {
        public Guid Id { get; init; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ImageId { get; set; }
        public Guid SpaceId { get; set; }
    }
}
