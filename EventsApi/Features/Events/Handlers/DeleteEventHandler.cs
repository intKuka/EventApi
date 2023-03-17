using EventsApi.Features.Events.Commands;
using EventsApi.Features.Events.Data;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.Handlers
{
    [UsedImplicitly]
    public class DeleteEventHandler : IRequestHandler<DeleteEventCommand, ScResult<string>>
    {
        private readonly IEventData _eventData;

        public DeleteEventHandler(IEventData eventData)
        {
            _eventData = eventData;
        }

        public async Task<ScResult<string>> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            return  new ScResult<string>(await _eventData.DeleteEvent(request.Id));

        }
    }
}
