using EventsApi.Features.Models;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.Commands;

public record UpdateEventCommand(Event Event) : IRequest<ScResult<Event>>;
