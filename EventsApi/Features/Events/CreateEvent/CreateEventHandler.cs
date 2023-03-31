using EventsApi.Features.Tickets;
using EventsApi.MongoDb;
using EventsApi.Settings;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using SC.Internship.Common.ScResult;
using SC.Internship.Common.Exceptions;

namespace EventsApi.Features.Events.CreateEvent
{
    [UsedImplicitly]
    public class CreateEventHandler : ICommandHandler<CreateEventCommand, ScResult<Event>>
    {
        private readonly IEventRepo _eventData;
        private readonly IHttpClientFactory _factory;
        private readonly IOptions<ServicesUris> _options;

        public CreateEventHandler(IEventRepo eventData, IHttpClientFactory factory, IOptions<ServicesUris> options)
        {
            _eventData = eventData;
            _factory = factory;
            _options = options;
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
                using var response = await client.GetAsync($"{_options.Value.Images}/{imageGuid}");
                if (response.Content.ReadAsStringAsync().Result == "false")
                    throw new ScException($"Изображение {imageGuid} не найдено");
            }

            client = _factory.CreateClient(Global.EventClient);
            using var response1 = await client.GetAsync($"{_options.Value.Spaces}/{spaceGuid}");
            if (response1.Content.ReadAsStringAsync().Result == "false")
                throw new ScException($"Пространство {spaceGuid} не найдено");
        }
    }
}
