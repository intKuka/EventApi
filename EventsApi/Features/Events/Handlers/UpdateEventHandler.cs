using EventsApi.Features.Events.Commands;
using EventsApi.Features.Events.Data;
using EventsApi.Features.Models;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.Handlers
{
    [UsedImplicitly]
    public class UpdateEventHandler : IRequestHandler<UpdateEventCommand, ScResult<Event>>
    {
        private readonly IEventData _eventData;

        public UpdateEventHandler(IEventData eventData)
        {
            _eventData = eventData;
        }

        public async Task<ScResult<Event>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            await _eventData.UpdateEvent(request.Event);
            return new ScResult<Event>(request.Event);
        }

    }
}