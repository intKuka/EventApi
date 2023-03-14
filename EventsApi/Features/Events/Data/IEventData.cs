namespace EventsApi.Features.Events.Data
{
    public interface IEventData
    {
        Task Create(Event newEvent);
        Task<string?> Delete(Guid id);
        Task<IEnumerable<Event>?> GetAll();
        Task<Event?> GetById(Guid id);
        Task<Event?> Update(Event update);
    }
}