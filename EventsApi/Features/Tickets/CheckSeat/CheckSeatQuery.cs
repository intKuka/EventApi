using EventsApi.Features.Events;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Tickets.CheckSeat;

[UsedImplicitly]
public record CheckSeatQuery(Event Event, int Seat) : IRequest<ScResult<bool>>;