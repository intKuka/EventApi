using EventsApi.Features.Models;
using EventsApi.Features.Tickets.Data;
using EventsApi.Features.Tickets.Queries;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Tickets.Handlers
{
    [UsedImplicitly]
    public class CheckUserTicketHandler : IRequestHandler<CheckUserTicketQuery, ScResult<IEnumerable<Ticket>>>
    {
        public async Task<ScResult<IEnumerable<Ticket>>> Handle(CheckUserTicketQuery request, CancellationToken cancellationToken)
        {
            return new ScResult<IEnumerable<Ticket>>(await TicketsData.CheckUserTicket(request.Event, request.UserGuid));
        }
    }
}
