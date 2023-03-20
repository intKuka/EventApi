using EventsApi.Features.Events.Commands;
using EventsApi.MongoDb;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.Handlers
{
    [UsedImplicitly]
    public class DeleteEventHandler : IRequestHandler<DeleteEventCommand, ScResult<string>>
    {
        private readonly IEventRepo _eventData;

        public DeleteEventHandler(IEventRepo eventData)
        {
            _eventData = eventData;
        }

        public async Task<ScResult<string>> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            await _eventData.DeleteEvent(request.Id);
            return new ScResult<string>($"Мероприятие {request.Id} удалено");

        }
    }
}
