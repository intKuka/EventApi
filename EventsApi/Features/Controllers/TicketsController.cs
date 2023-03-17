using System.Net;
using EventsApi.Features.Events.Queries;
using EventsApi.Features.Models;
using EventsApi.Features.Tickets.Commands;
using EventsApi.Features.Tickets.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;


namespace EventsApi.Features.Controllers
{
    [Produces("application/json")]
    [Route("api/tickets")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator, IValidator<Event> validator)
        {
            _mediator = mediator;
        }


        [HttpPatch("giveTicket")]
        public async Task<ScResult<Ticket>> IssueTicket(Guid eventId, Guid userId)
        {
            var existingEvent = await _mediator.Send(new GetEventByIdQuery(eventId));
            var result = await _mediator.Send(new IssueTicketCommand(existingEvent.Result!, userId));
            return result;
        }

        [HttpGet("checkTicket")]
        public async Task<ScResult<IEnumerable<Ticket>>> CheckUserTicket(Guid eventId, Guid userId)
        {
            var existingEvent = await _mediator.Send(new GetEventByIdQuery(eventId));
            var result = await _mediator.Send(new CheckUserTicketQuery(existingEvent.Result!, userId));
            return result;
        }
    }
}
