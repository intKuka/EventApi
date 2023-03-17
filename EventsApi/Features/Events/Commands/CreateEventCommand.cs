using EventsApi.Features.Models;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.Commands;

public record CreateEventCommand(Event NewEvent) : IRequest<ScResult<Event>>;

