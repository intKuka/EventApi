using MongoDB.Driver;
using EventsApi.Features.Models;
using MongoDB.Bson;
using SC.Internship.Common.Exceptions;

namespace EventsApi.MongoDb
{
    public class MongoDbRepo : IEventRepo
    {
        private const string DatabaseName = "EventsApi";
        private const string CollectionName = "events";
        private readonly IMongoCollection<Event> _eventsCollection;
        private readonly FilterDefinitionBuilder<Event> _filterBuilder = Builders<Event>.Filter;


        public MongoDbRepo(IMongoClient client)
        {
            var db = client.GetDatabase(DatabaseName);
            _eventsCollection = db.GetCollection<Event>(CollectionName);
        }

        public async Task PostEvent(Event newEvent)
        {
            SetTicketsApplication(newEvent);
            await _eventsCollection.InsertOneAsync(newEvent);
        }

        public async Task DeleteEvent(Guid id)
        {
            var filter = _filterBuilder.Eq("_id", id);
            var eEvent = await _eventsCollection.FindOneAndDeleteAsync(filter);
            if (eEvent == null) throw new ScException("Мероприятие не найдено");
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            return await _eventsCollection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Event> GetEventById(Guid id)
        {
            var filter = _filterBuilder.Eq("_id", id);
            var existEvent = await _eventsCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
            return existEvent ?? throw new ScException("Мероприятие не найдено");
        }

        public async Task UpdateEvent(Event update)
        {
            SetTicketsApplication(update);
            var filter = _filterBuilder.Eq("_id", update.Id);
            var updated = await _eventsCollection.FindOneAndReplaceAsync(filter, update);
            if (updated == null)
            {
                throw new ScException("Мероприятие не найдено");
            }
        }  

        //проверяет нужно ли добовлять или оставлять билеты
        private static void SetTicketsApplication(Event eEvent)
        {
            eEvent.TicketList = new List<Ticket>();
            if (eEvent.TicketsQuantity == 0) return;
            for (var i = 1; i <= eEvent.TicketsQuantity; i++)
            {
                eEvent.TicketList.Add(new Ticket() { Id = Guid.NewGuid(), Seat = i });
            }
        }
    }
}
