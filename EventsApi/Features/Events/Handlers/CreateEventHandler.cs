using EventsApi.Features.Events.Commands;
using EventsApi.Features.Events.Data;
using EventsApi.Features.Models;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.Handlers
{
    [UsedImplicitly]
    public class CreateEventHandler : IRequestHandler<CreateEventCommand, ScResult<Event>>
    {
        private readonly IEventData _eventData;
        public CreateEventHandler(IEventData eventData)
        {
            _eventData = eventData;
        }

        public async Task<ScResult<Event>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            await _eventData.PostEvent(request.NewEvent);
            return new ScResult<Event>(request.NewEvent);
        }
    }
}
