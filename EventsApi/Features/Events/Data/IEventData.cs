using EventsApi.Features.Models;

namespace EventsApi.Features.Events.Data
{
    public interface IEventData
    {
        Task PostEvent(Event newEvent);
        Task<string> DeleteEvent(Guid id);
        Task<IEnumerable<Event>> GetAllEvents();
        Task<Event> GetEventById(Guid id);
        Task UpdateEvent(Event update);
    }
}