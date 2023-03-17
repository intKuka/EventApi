using System.Text.Json;
using EventsApi.Features.Events.Commands;
using EventsApi.Features.Events.Queries;
using EventsApi.Features.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;


namespace EventsApi.Features.Controllers
{
    [Produces("application/json")]
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<Event> _validator;
        public EventsController(IMediator mediator, IValidator<Event> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }


        // GET api/events
        /// <summary>
        /// получить все мероприятия
        /// </summary>
        /// <returns>список всех мероприятий</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet]
        [ProducesResponseType(typeof(ScResult<IEnumerable<Event>>), 200)]
        [ProducesDefaultResponseType]
        public async Task<ScResult<IEnumerable<Event>>> GetEvents()
        {
            var events = await _mediator.Send(new GetEventsQuery());
            return events;
        }

        // GET api/events/{id}
        /// <summary>
        /// ищет и возвращает мероприятие по его id
        /// </summary>
        /// <param name="id">id существующего мероприятия</param>
        /// <returns>существующее мероприятие</returns>
        /// <response code="400">Плохие данные клиента</response>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ScResult<Event>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ScResult<Event>> GetEventById(Guid id)
        {
            var event_ = await _mediator.Send(new GetEventByIdQuery(id));
            return event_;
        }

        // POST api/events
        /// <summary>
        /// creates new event
        /// </summary>
        /// <param name="starts">date and time when event starts</param>
        /// <param name="ends">date and time when event ends</param>
        /// <param name="name">name of event</param>
        /// <param name="imageId">image id for this event</param>
        /// <param name="spaceId">space id for this event</param>
        /// <param name="ticketQuantity">количество билетов для мероприятия</param>
        /// <param name="description">описание</param>
        /// <returns>created event</returns>
        /// <remarks>data installation format: yyyy-mm-ddThh:MM:ss</remarks>
        /// <response code="400">Плохие данные клиента</response>
        /// <response code="200">Успешное выполнение</response>
        [HttpPost]
        [ProducesResponseType(typeof(ScResult<Event>), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ScResult<Event>> PostEvent(DateTime starts, DateTime ends, string name, Guid imageId, Guid spaceId, int ticketQuantity = 0, string description = "")
        {
            var event_ = new Event()
            {
                Id = Guid.NewGuid(),
                Starts = starts,
                Ends = ends,
                Name = name,
                Description = description,
                ImageId = imageId,
                SpaceId = spaceId,
                TicketsQuantity = ticketQuantity
            };
            await _validator.ValidateAndThrowAsync(event_);

            return await _mediator.Send(new CreateEventCommand(event_));
        }

        // PUT api/events/{id}
        /// <summary>
        /// alters event into a new one
        /// </summary>
        /// <param name="id">id of event to change</param>
        /// <param name="starts">date and time when event starts</param>
        /// <param name="ends">date and time when event ends</param>
        /// <param name="name">name of event</param>
        /// <param name="imageId">image id for this event</param>
        /// <param name="spaceId">space id for this event</param>
        /// <param name="ticketQuantity"></param>
        /// <param name="description">description</param>
        /// <returns>altered event</returns>
        /// <remarks>data installation format: yyyy-mm-ddThh:MM:ss</remarks>
        /// <response code="400">Плохие данные клиента</response>
        /// <response code="200">Успешное выполнение</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ScResult<Event>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ScResult<Event>> PutEvent(Guid id, DateTime starts, DateTime ends, string name, Guid imageId, Guid spaceId, int ticketQuantity = 0, string description = "")
        {
            var event_ = new Event()
            {   Id=id,
                Starts = starts,
                Ends = ends,
                Name = name,
                Description = description,
                ImageId = imageId,
                SpaceId = spaceId,
                TicketsQuantity = ticketQuantity
            };
            await _validator.ValidateAndThrowAsync(event_);
            return await _mediator.Send(new UpdateEventCommand(event_));
        }

        // DELETE api/events/{id}
        /// <summary>
        /// removes event from the list
        /// </summary>
        /// <param name="id">id of event to remove</param>
        /// <returns>result string</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Плохие данные клиента</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ScResult<string>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ScResult<string>> DeleteEvent(Guid id)
        {
            return await _mediator.Send(new DeleteEventCommand(id)); ;
        }
    }
}
