using EventsApi.Features.Events.Data;
using EventsApi.Features.Events.Queries;
using EventsApi.Features.Models;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.Handlers
{
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    // копирует название операции для удобства
    public class GetEventsHandler : IRequestHandler<GetEventsQuery, ScResult<IEnumerable<Event>>>
    {
        private readonly IEventData _eventData;
        public GetEventsHandler(IEventData eventData)
        {
            _eventData = eventData;
        }

        public async Task<ScResult<IEnumerable<Event>>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            return new ScResult<IEnumerable<Event>>(await _eventData.GetAllEvents());
        }
    }
}
