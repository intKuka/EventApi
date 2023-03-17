using EventsApi.Features.Models;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.Queries;

public record GetEventByIdQuery(Guid Id) : IRequest<ScResult<Event>>;
