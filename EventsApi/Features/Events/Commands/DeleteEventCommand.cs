using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.Commands;

public record DeleteEventCommand(Guid Id) : IRequest<ScResult<string>>;
