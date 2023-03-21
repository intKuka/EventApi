using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.DeleteEvent;

public record DeleteEventCommand(Guid Id) : IRequest<ScResult<string>>;
