using MediatR;

namespace EventsApi.Features.Events.Queries;

public record GetEventsQuery : IRequest<IEnumerable<Event>?>;
