using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.UpdateEvent;

public record UpdateEventCommand(Event Event) : IRequest<ScResult<Event>>;
