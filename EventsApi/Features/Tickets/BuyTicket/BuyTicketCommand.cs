using EventsApi.Features.Events;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Tickets.BuyTicket;

public record BuyTicketCommand(Event Event, Guid UserGuid) : IRequest<ScResult<Ticket>>;