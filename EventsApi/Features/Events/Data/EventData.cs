using EventsApi.Features.Images;
using EventsApi.Features.Models;
using EventsApi.Features.Spaces;
using SC.Internship.Common.Exceptions;

namespace EventsApi.Features.Events.Data
{
    public class EventData : IEventData
    {
        private static readonly List<Event> Events = new()
        {
            new Event
            {
                Id=Guid.NewGuid(),
                Starts=DateTime.UtcNow,
                Ends=DateTime.UtcNow.AddHours(5),
                Name="Some Event",
                Description="Very fun event",
                ImageId=TempImageData.Images[0].Id,
                SpaceId=TempSpaceData.Spaces[1].Id,
                TicketsQuantity = 5,
                TicketList =
                {
                    new Ticket(){Id=Guid.NewGuid(), Seat = 0},
                    new Ticket(){Id=Guid.NewGuid(), Seat = 1},
                    new Ticket(){Id=Guid.NewGuid(), Seat = 2},
                    new Ticket(){Id=Guid.NewGuid(), Seat = 3},
                    new Ticket(){Id=Guid.NewGuid(), Seat = 4},
                }
            },
        };


        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            return await Task.FromResult(Events);
        }

        public async Task<Event> GetEventById(Guid id)
        {
            var existEvent = await Task.FromResult(Events.FirstOrDefault(e => e.Id == id));
            return existEvent ?? throw new ScException("Мероприятие не найдено");
        }

        public async Task PostEvent(Event newEvent)
        {
            if(newEvent.TicketsQuantity > 0) AddTickets(newEvent, newEvent.TicketsQuantity);
            Events.Add(newEvent);
            await Task.CompletedTask;
        }

        public async Task UpdateEvent(Event update)
        {
            var existEvent = await Task.FromResult(Events.FirstOrDefault(e => e.Id == update.Id));
            if (existEvent != null)
            {
                if (update.TicketsQuantity > 0) AddTickets(update, update.TicketsQuantity);
                Events.Remove(existEvent);
                Events.Add(update);
                await Task.CompletedTask;
            }
            else
            {
                throw new ScException("Мероприятие не найдено");
            }
        }

        public async Task<string> DeleteEvent(Guid id)
        {
            var existEvent = await Task.FromResult(Events.First(e => e.Id == id));
            switch (existEvent)
            {
                case null:
                    throw new ScException("Мероприятие не найдено");
                default:
                    Events.Remove(existEvent);
                    return $"Мероприятие {id} успешно удалено";
            }
        }

        private static void AddTickets(Event newEvent, int quantity)
        {
            for (var i = 0; i < quantity; i++)
            {
               newEvent.TicketList.Add(new Ticket(){Id=Guid.NewGuid(), Seat = i});
            }
        }
        

    }
}
