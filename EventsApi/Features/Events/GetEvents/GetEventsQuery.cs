using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.GetEvents;

// ReSharper disable once InconsistentNaming
// копирует название операции для удобства
public record GetEventsQuery : IRequest<ScResult<IEnumerable<Event>>>;
