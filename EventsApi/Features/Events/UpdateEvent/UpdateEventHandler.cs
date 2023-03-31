using EventsApi.Features.Tickets;
using EventsApi.MongoDb;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.UpdateEvent
{
    [UsedImplicitly]
    public class UpdateEventHandler : ICommandHandler<UpdateEventCommand, ScResult<Event>>
    {
        private readonly IEventRepo _eventData;
        private readonly IHttpClientFactory _factory;

        public UpdateEventHandler(IEventRepo eventData, IHttpClientFactory factory)
        {
            _eventData = eventData;
            _factory = factory;
        }

        public async Task<ScResult<Event>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            await IsValidImageAndSpace(request.Event.ImageId, request.Event.SpaceId);
            TicketsData.TryTicketsApplication(request.Event);
            await _eventData.UpdateEvent(request.Event);
            return new ScResult<Event>(request.Event);
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