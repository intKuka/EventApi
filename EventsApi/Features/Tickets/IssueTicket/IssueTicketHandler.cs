using EventsApi.MongoDb;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Tickets.IssueTicket
{
    [UsedImplicitly]
    public class IssueTicketHandler : IRequestHandler<IssueTicketCommand, ScResult<Ticket>>
    {
        private readonly IEventRepo _eventData;
        public IssueTicketHandler(IEventRepo eventData)
        {
            _eventData = eventData;
        }

        public async Task<ScResult<Ticket>> Handle(IssueTicketCommand request, CancellationToken cancellationToken)
        {
            return new ScResult<Ticket>(await _eventData.IssueTicket(request.Event, request.UserGuid));
        }
    }
}
