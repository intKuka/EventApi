using EventsApi.Features.Events.Data;
using EventsApi.Features.Events.Queries;
using MediatR;

namespace EventsApi.Features.Events.Handlers.ForEvent
{
    public class GetEventsHandler : IRequestHandler<GetEventsQuery, IEnumerable<Event>?>
    {
        readonly IEventData _eventData;
        public GetEventsHandler(IEventData eventData)
        {
            _eventData = eventData; 
        }

        public async Task<IEnumerable<Event>?> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            return await _eventData.GetAll();            
        }
    }
}
