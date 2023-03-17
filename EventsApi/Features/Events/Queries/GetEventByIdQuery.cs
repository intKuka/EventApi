using EventsApi.Features.Models;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Events.Queries;

// ReSharper disable once InconsistentNaming
// копирует название операции для удобства
public record GetEventByIdQuery(Guid Id) : IRequest<ScResult<Event>>;
