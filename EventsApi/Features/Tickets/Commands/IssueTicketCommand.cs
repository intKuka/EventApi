using EventsApi.Features.Models;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Tickets.Commands
{
    public record IssueTicketCommand(Event Event, Guid UserGuid) : IRequest<ScResult<Ticket>>;
}
