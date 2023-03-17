using EventsApi.Features.Events.Queries;
using EventsApi.Features.Models;
using EventsApi.Features.Tickets.Commands;
using EventsApi.Features.Tickets.Queries;
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

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // PATCH api/tickets/giveTicket
        /// <summary>
        /// вручить билет пользователю на определенное мероприятие
        /// </summary>
        /// <param name="eventId">guid мероприятия</param>
        /// <param name="userId">guid пользователя</param>
        /// <returns>билет, полученный указанным пользователем</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Плохие данные клиента</response>
        [HttpPatch("giveTicket")]
        [ProducesResponseType(typeof(ScResult<Ticket>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ScResult<Ticket>> IssueTicket(Guid eventId, Guid userId)
        {
            var existingEvent = await _mediator.Send(new GetEventByIdQuery(eventId));
            return await _mediator.Send(new IssueTicketCommand(existingEvent.Result!, userId));
        }

        // GET api/tickets/checkTicket
        /// <summary>
        /// список билетов пользователя на конкретное мероприятие
        /// </summary>
        /// <param name="eventId">guid мероприятия</param>
        /// <param name="userId">guid пользователя</param>
        /// <returns>список билетов, которыми владеет пользователь в рамках указанноко события</returns> 
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Плохие данные клиента</response>
        [HttpGet("checkTicket")]
        [ProducesResponseType(typeof(ScResult<Ticket>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ScResult<IEnumerable<Ticket>>> CheckUserTicket(Guid eventId, Guid userId)
        {
            var existingEvent = await _mediator.Send(new GetEventByIdQuery(eventId));
            return await _mediator.Send(new CheckUserTicketQuery(existingEvent.Result!, userId));
        }
    }
}
