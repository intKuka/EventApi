using EventsApi.Features.Tickets;
using EventsApi.MongoDb;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;
using SC.Internship.Common.Exceptions;

namespace EventsApi.Features.Events.CreateEvent
{
    [UsedImplicitly]
    public class CreateEventHandler : IRequestHandler<CreateEventCommand, ScResult<Event>>
    {
        private readonly IEventRepo _eventData;
        private readonly HttpClient _httpClient;
        public CreateEventHandler(IEventRepo eventData)
        {
            _eventData = eventData;
            _httpClient = new HttpClient();
        }

        public async Task<ScResult<Event>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            await IsValidImageAndSpace(request.NewEvent.ImageId, request.NewEvent.SpaceId);
            TicketsData.TryTicketsApplication(request.NewEvent);
            await _eventData.CreateEvent(request.NewEvent);
            return new ScResult<Event>(request.NewEvent);
        }

        private async Task IsValidImageAndSpace(Guid? imageGuid, Guid spaceGuid)
        {
            if (imageGuid != null)
            {
                using var response = await _httpClient.GetAsync($"http://localhost:5051/images/{imageGuid}");
                if (response.Content.ReadAsStringAsync().Result == "false")
                    throw new ScException($"Изображение {imageGuid} не найдено");
            }

            using var response1 = await _httpClient.GetAsync($"http://localhost:5093/spaces/{spaceGuid}");
            if (response1.Content.ReadAsStringAsync().Result == "false")
                throw new ScException($"Пространство {spaceGuid} не найдено");
        }
    }
}
