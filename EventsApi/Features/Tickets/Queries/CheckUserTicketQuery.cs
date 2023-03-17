using EventsApi.Features.Models;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Tickets.Queries
{
    [UsedImplicitly]
    public record CheckUserTicketQuery(Event Event, Guid UserGuid) : IRequest<ScResult<IEnumerable<Ticket>>>;

}
