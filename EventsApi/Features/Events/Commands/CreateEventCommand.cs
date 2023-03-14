using MediatR;

namespace EventsApi.Features.Events.Commands;

public record CreateEventCommand(Event NewEvent) : IRequest<Event>;

