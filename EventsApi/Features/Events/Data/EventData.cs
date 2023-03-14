using EventsApi.Features.Events.Queries;

namespace EventsApi.Features.Events.Data
{
    public class EventData : IEventData
    {
        public static List<Event> events = new()
        {
            new Event
            {
                Id=Guid.NewGuid(),
                Starts=DateTime.UtcNow,
                Ends=DateTime.UtcNow.AddHours(5),
                Name="Some Event",
                Description="Very fun event",
                ImageId=TempImageData.images[0].Id,
                SpaceId=TempSpaceData.spaces[1].Id
            },
        };


        public async Task<IEnumerable<Event>?> GetAll()
        {
            return await Task.FromResult(events);
        }

        public async Task<Event?> GetById(Guid id)
        {  
            return await Task.FromResult(events.FirstOrDefault(e => e.Id == id));
        }

        public async Task Create(Event newEvent)
        {
            events.Add(newEvent);
            await Task.CompletedTask;
        }

        public async Task<Event?> Update(Event update)
        {
            var existEvent = await Task.FromResult(events.FirstOrDefault(e => e.Id == update.Id));
            if (existEvent != null)
            {
                events.Remove(existEvent);
                events.Add(update);
                return update;
            }
            return null;
        }

        public async Task<string?> Delete(Guid id)
        {
            var existEvent = await Task.FromResult(events.FirstOrDefault(e => e.Id == id));
            if (existEvent != null)
            {
                events.Remove(existEvent);
                return $"Event {id} has deleted";
            }
            return null;
        }

        
    }
}
