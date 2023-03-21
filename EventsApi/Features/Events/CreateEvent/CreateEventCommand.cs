using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.CreateEvent;

public record CreateEventCommand(Event NewEvent) : IRequest<ScResult<Event>>;

