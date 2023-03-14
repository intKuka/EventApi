using MediatR;

namespace EventsApi.Features.Events.Commands;

public record UpdateEventCommand(Event Event) : IRequest<Event?>;
