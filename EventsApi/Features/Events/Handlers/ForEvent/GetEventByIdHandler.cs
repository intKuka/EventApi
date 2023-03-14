using EventsApi.Features.Events.Data;
using EventsApi.Features.Events.Queries;
using MediatR;

namespace EventsApi.Features.Events.Handlers.ForEvent
{
    public class GetEventByIdHandler
    {
        public class GetEventsHandler : IRequestHandler<GetEventByIdQuery, Event?>
        {
            readonly IEventData _eventData;
            public GetEventsHandler(IEventData eventData)
            {
                _eventData = eventData;
            }

            public async Task<Event?> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
            {
                return await _eventData.GetById(request.Id);
            }
        }
    }
}
