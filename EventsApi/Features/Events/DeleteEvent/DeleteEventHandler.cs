using EventsApi.MongoDb;
using EventsApi.RabbitMq;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.DeleteEvent
{
    [UsedImplicitly]
    public class DeleteEventHandler : IRequestHandler<DeleteEventCommand, ScResult<string>>
    {
        private readonly IEventRepo _eventData;
        private readonly EventDeletionSender _deletionSender;

        public DeleteEventHandler(IEventRepo eventData, EventDeletionSender deletionSender)
        {
            _eventData = eventData;
            _deletionSender = deletionSender;
        }

        public async Task<ScResult<string>> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            await _eventData.DeleteEvent(request.Id);
            _deletionSender.SendEvent(request.Id);
            return new ScResult<string>($"Мероприятие {request.Id} удалено");

        }
    }
}
