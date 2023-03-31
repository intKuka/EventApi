using EventsApi.Features.Tickets;
using EventsApi.MongoDb;
using JetBrains.Annotations;
using SC.Internship.Common.ScResult;
using SC.Internship.Common.Exceptions;

namespace EventsApi.Features.Events.CreateEvent
{
    [UsedImplicitly]
    public class CreateEventHandler : ICommandHandler<CreateEventCommand, ScResult<Event>>
    {
        private readonly IEventRepo _eventData;
        private readonly IHttpClientFactory _factory;
        public CreateEventHandler(IEventRepo eventData, IHttpClientFactory factory)
        {
            _eventData = eventData;
            _factory = factory;
        }

        public async Task<ScResult<Event>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            request.NewEvent.Id = Guid.NewGuid();
            await IsValidImageAndSpace(request.NewEvent.ImageId, request.NewEvent.SpaceId);
            TicketsData.TryTicketsApplication(request.NewEvent);
            await _eventData.CreateEvent(request.NewEvent);
            return new ScResult<Event>(request.NewEvent);
        }

        private async Task IsValidImageAndSpace(Guid? imageGuid, Guid spaceGuid)
        {
            var client = _factory.CreateClient(Global.EventClient);
            if (imageGuid != null)
            {
                using var response = await client.GetAsync($"http://localhost:5051/images/{imageGuid}");
                if (response.Content.ReadAsStringAsync().Result == "false")
                    throw new ScException($"Изображение {imageGuid} не найдено");
            }

            using var response1 = await client.GetAsync($"http://localhost:5093/spaces/{spaceGuid}");
            if (response1.Content.ReadAsStringAsync().Result == "false")
                throw new ScException($"Пространство {spaceGuid} не найдено");
        }
    }
}
