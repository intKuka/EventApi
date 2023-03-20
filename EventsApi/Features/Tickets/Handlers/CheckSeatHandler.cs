using EventsApi.Features.Tickets.Data;
using EventsApi.Features.Tickets.Queries;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventsApi.Features.Tickets.Handlers
{
    [UsedImplicitly]
    public class CheckSeatHandler : IRequestHandler<CheckSeatQuery, ScResult<bool>>
    {
        public Task<ScResult<bool>> Handle(CheckSeatQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ScResult<bool>(TicketsData.CheckSeat(request.Event, request.Seat)));
        }
    }
}
