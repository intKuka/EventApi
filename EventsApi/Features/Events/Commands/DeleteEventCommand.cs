using MediatR;

namespace EventsApi.Features.Events.Commands;

public record DeleteEventCommand(Guid Id) : IRequest<string?>;
