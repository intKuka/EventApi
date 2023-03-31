using EventsApi.Features.Events;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Tickets.CheckUserTicket;

[UsedImplicitly]
public record CheckUserTicketQuery(Event Event, Guid UserGuid) : IRequest<ScResult<IEnumerable<Ticket>>>;