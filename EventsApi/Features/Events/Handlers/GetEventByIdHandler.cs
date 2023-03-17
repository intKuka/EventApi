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
    public class GetEventByIdHandler : IRequestHandler<GetEventByIdQuery, ScResult<Event>>
    {
        private readonly IEventData _eventData;

        public GetEventByIdHandler(IEventData eventData)
        {
            _eventData = eventData;
        }

        public async Task<ScResult<Event>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            return new ScResult<Event>(await _eventData.GetEventById(request.Id));
        }
    }

}
