using EventsApi.MongoDb;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.GetEvents;

[UsedImplicitly]
// ReSharper disable once InconsistentNaming
// копирует название операции для удобства
public class GetEventsHandler : IRequestHandler<GetEventsQuery, ScResult<IEnumerable<Event>>>
{
    private readonly IEventRepo _eventData;
    public GetEventsHandler(IEventRepo eventData)
    {
        _eventData = eventData;
    }

    public async Task<ScResult<IEnumerable<Event>>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        return new ScResult<IEnumerable<Event>>(await _eventData.GetAllEvents());
    }
}