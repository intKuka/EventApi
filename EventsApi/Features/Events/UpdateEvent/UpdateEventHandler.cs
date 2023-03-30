﻿using EventsApi.Features.Tickets;
using EventsApi.MongoDb;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.UpdateEvent
{
    [UsedImplicitly]
    public class UpdateEventHandler : IRequestHandler<UpdateEventCommand, ScResult<Event>>
    {
        private readonly IEventRepo _eventData;
        private readonly HttpClient _httpClient;

        public UpdateEventHandler(IEventRepo eventData)
        {
            _eventData = eventData;
            _httpClient = new HttpClient();
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