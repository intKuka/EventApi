using EventsApi.MongoDb;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.CreateEvent
{
    [UsedImplicitly]
    public class CreateEventHandler : IRequestHandler<CreateEventCommand, ScResult<Event>>
    {
        private readonly IEventRepo _eventData;
        public CreateEventHandler(IEventRepo eventData)
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
