using EventsApi.Features.Models;

namespace EventsApi.MongoDb;

public interface IEventRepo
{
    Task PostEvent(Event newEvent);
    Task DeleteEvent(Guid id);
    Task<IEnumerable<Event>> GetAllEvents();
    Task<Event> GetEventById(Guid id);
    Task UpdateEvent(Event update);
}