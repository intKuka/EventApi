using EventsApi.MongoDb;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.GetEventById
{
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class GetEventByIdHandler : IRequestHandler<GetEventByIdQuery, ScResult<Event>>
    {
        private readonly IEventRepo _eventData;

        public GetEventByIdHandler(IEventRepo eventData)
        {
            _eventData = eventData;
        }

        public async Task<ScResult<Event>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            return new ScResult<Event>(await _eventData.GetEventById(request.Id));
        }
    }

}
