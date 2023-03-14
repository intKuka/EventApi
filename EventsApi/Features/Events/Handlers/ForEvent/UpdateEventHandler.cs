using EventsApi.Features.Events.Commands;
using EventsApi.Features.Events.Data;
using MediatR;

namespace EventsApi.Features.Events.Handlers.ForEvent
{
    public class UpdateEventHandler : IRequestHandler<UpdateEventCommand, Event?>
    {
        IEventData _eventData;

        public UpdateEventHandler(IEventData eventData)
        {
            _eventData = eventData;            
        }

        public async Task<Event?> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var event_ = await _eventData.Update(request.Event);
            return event_;
        }
    }
}
