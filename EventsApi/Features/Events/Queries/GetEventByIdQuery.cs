using MediatR;

namespace EventsApi.Features.Events.Queries;

public record GetEventByIdQuery(Guid Id) : IRequest<Event?>;
