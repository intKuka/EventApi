using EventsApi.Features.Models;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.Queries;

public record GetEventsQuery : IRequest<ScResult<IEnumerable<Event>>>;
