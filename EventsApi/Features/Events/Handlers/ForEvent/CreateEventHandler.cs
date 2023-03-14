using EventsApi.Features.Events.Commands;
using EventsApi.Features.Events.Data;
using MediatR;

namespace EventsApi.Features.Events.Handlers.ForEvent
{
    public class CreateEventHandler : IRequestHandler<CreateEventCommand, Event>
    {
        readonly IEventData _eventData;
        public CreateEventHandler(IEventData eventData)
        {
            _eventData = eventData;
        }

        public async Task<Event> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            await _eventData.Create(request.NewEvent);
            return request.NewEvent;
        }
    }
}
