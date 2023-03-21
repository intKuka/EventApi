using EventsApi.Features.Events;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Tickets.IssueTicket
{
    public record IssueTicketCommand(Event Event, Guid UserGuid) : IRequest<ScResult<Ticket>>;
}
