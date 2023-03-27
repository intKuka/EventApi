using EventsApi.Features.Events.GetEventById;
using EventsApi.Features.Tickets.BuyTicket;
using EventsApi.Features.Tickets.CheckSeat;
using EventsApi.Features.Tickets.CheckUserTicket;
using EventsApi.Features.Tickets.IssueTicket;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;
using System.Collections.Generic;
using System.Net.Http;


namespace EventsApi.Features.Tickets
{
    [Produces("application/json")]
    [Route("api/tickets")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly HttpClient _httpClient;

        public TicketsController(IMediator mediator, IHttpClientFactory factory)
        {
            _mediator = mediator;
            _httpClient = factory.CreateClient();
        }

        // PATCH api/tickets/get_ticket
        /// <summary>
        /// вручить билет пользователю на определенное мероприятие
        /// </summary>
        /// <param name="eventId">guid мероприятия</param>
        /// <param name="userId">guid пользователя</param>
        /// <returns>билет, полученный указанным пользователем</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Плохие данные клиента</response>
        [HttpPatch("get_ticket")]
        [ProducesResponseType(typeof(ScResult<Ticket>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ScResult<Ticket>> IssueTicket([FromQuery] Guid eventId, [FromQuery] Guid userId)
        {
            using (var response = await _httpClient.GetAsync($"http://localhost:5018/users/{userId}"))
            {
                if (response.Content.ReadAsStringAsync().Result == "false")
                {
                    
                    return new ScResult<Ticket>(new ScError(){ Message = $"Пользователь {userId} не найден"});
                }
            }
            var existingEvent = await _mediator.Send(new GetEventByIdQuery(eventId));
            if (existingEvent.Result!.Price > 0)
            {
                return await _mediator.Send(new BuyTicketCommand(existingEvent.Result, userId));
            }
            return await _mediator.Send(new IssueTicketCommand(existingEvent.Result!, userId));
        }

        // GET api/tickets/check_ticket
        /// <summary>
        /// список билетов пользователя на конкретное мероприятие
        /// </summary>
        /// <param name="eventId">guid мероприятия</param>
        /// <param name="userId">guid пользователя</param>
        /// <returns>список билетов, которыми владеет пользователь в рамках указанноко события</returns> 
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Плохие данные клиента</response>
        [HttpGet("check_ticket")]
        [ProducesResponseType(typeof(ScResult<Ticket>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ScResult<IEnumerable<Ticket>>> CheckUserTicket([FromQuery] Guid eventId, [FromQuery] Guid userId)
        {
            using (var response = await _httpClient.GetAsync($"http://localhost:5018/users/{userId}"))
            {
                if (response.Content.ReadAsStringAsync().Result == "false")
                {
                    return new ScResult<IEnumerable<Ticket>> (new ScError() { Message = $"Пользователь {userId} не найден" });
                }
            }
            var existingEvent = await _mediator.Send(new GetEventByIdQuery(eventId));
            return await _mediator.Send(new CheckUserTicketQuery(existingEvent.Result!, userId));
        }

        // GET api/tickets/check_seat
        /// <summary>
        /// проверяет свободно ли указанное места на мероприятии
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="seat"></param>
        /// <returns></returns>
        [HttpGet("check_seat")]
        [ProducesResponseType(typeof(ScResult<bool>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ScResult<bool>> CheckSeat([FromQuery] Guid eventId, [FromQuery] int seat)
        {
            var existingEvent = await _mediator.Send(new GetEventByIdQuery(eventId));
            return await _mediator.Send(new CheckSeatQuery(existingEvent.Result!, seat));
        }
        


    }
}
