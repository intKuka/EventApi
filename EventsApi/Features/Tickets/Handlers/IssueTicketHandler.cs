using EventsApi.Features.Models;
using EventsApi.Features.Tickets.Commands;
using EventsApi.Features.Tickets.Data;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Tickets.Handlers
{
    [UsedImplicitly]
    public class IssueTicketHandler : IRequestHandler<IssueTicketCommand, ScResult<Ticket>>
    {
        public async Task<ScResult<Ticket>> Handle(IssueTicketCommand request, CancellationToken cancellationToken)
        {
            return new ScResult<Ticket>(await TicketsData.IssueTicket(request.Event, request.UserGuid));
        }
    }
}
