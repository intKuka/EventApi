using EventsApi.Features.Events;

namespace EventsApi.MongoDb;

public interface IEventRepo
{
    Task CreateEvent(Event newEvent);
    Task DeleteEvent(Guid id);
    Task<IEnumerable<Event>> GetAllEvents();
    Task<Event> GetEventById(Guid id);
    Task UpdateEvent(Event update);
}