using EventsApi.MongoDb;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.UpdateEvent
{
    [UsedImplicitly]
    public class UpdateEventHandler : IRequestHandler<UpdateEventCommand, ScResult<Event>>
    {
        private readonly IEventRepo _eventData;

        public UpdateEventHandler(IEventRepo eventData)
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