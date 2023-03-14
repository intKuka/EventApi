using EventsApi.Features.Events.Commands;
using EventsApi.Features.Events.Data;
using MediatR;

namespace EventsApi.Features.Events.Handlers.ForEvent
{
    public class DeleteEventHandler : IRequestHandler<DeleteEventCommand, string?>
    {
        readonly IEventData _eventData;
        public DeleteEventHandler(IEventData eventData)
        {
            _eventData = eventData;
        }

        public async Task<string?> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            return await _eventData.Delete(request.Id);
        }
    }
}
