using EventsApi.Features.Events.Commands;
using EventsApi.Features.Events.Queries;
using EventsApi.Features.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
            var result = await _mediator.Send(new GetEventsQuery());
            return result;
        }

        // GET api/events/{id}
        /// <summary>
        /// получить мероприятие по его guid
        /// </summary>
        /// <param name="id">guid существующего мероприятия</param>
        /// <returns>существующее мероприятие</returns>
        /// <response code="400">Плохие данные клиента</response>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ScResult<Event>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ScResult<Event>> GetEventById(Guid id)
        {
            return await _mediator.Send(new GetEventByIdQuery(id));
        }

        // POST api/events
        /// <summary>
        /// создать новое мероприятие
        /// </summary>
        /// <param name="starts">дата и время начала</param>
        /// <param name="ends">дата и время окончания</param>
        /// <param name="name">название</param>
        /// <param name="imageId">guid картинки для мероприятия</param>
        /// <param name="spaceId">guid места проведения</param>
        /// <param name="ticketQuantity">количество билетов</param>
        /// <param name="description">описание</param>
        /// <returns>изменяет мероприятие</returns>
        /// <remarks>
        /// формат установки даты: yyyy-mm-ddThh:MM:ss
        /// пример: 2023-03-15T16:40:20
        /// </remarks>
        /// <response code="400">Плохие данные клиента</response>
        /// <response code="201">Создано успешно</response>
        [HttpPost]
        [ProducesResponseType(typeof(ScResult<Event>), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ScResult<Event>> PostEvent(DateTime starts, DateTime ends, string name, Guid imageId, Guid spaceId, int ticketQuantity = 0, string description = "")
        {
            var result = new Event()
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
            await _validator.ValidateAndThrowAsync(result);

            return await _mediator.Send(new CreateEventCommand(result));
        }

        // PUT api/events/{id}
        /// <summary>
        /// изменить мероприятие по его guid
        /// </summary>
        /// <param name="id">guid мерориятия, которое нужно изменить</param>
        /// <param name="starts">дата и время начала</param>
        /// <param name="ends">дата и время окончания</param>
        /// <param name="name">название</param>
        /// <param name="imageId">guid картинки для мероприятия</param>
        /// <param name="spaceId">guid места проведения</param>
        /// <param name="ticketQuantity">количество билетов</param>
        /// <param name="description">описание</param>
        /// <returns>изменяет мероприятие</returns>
        /// <remarks>
        /// формат установки даты: yyyy-mm-ddThh:MM:ss
        /// пример: 2023-03-15T16:40:20
        /// </remarks>
        /// <response code="400">Плохие данные клиента</response>
        /// <response code="200">Успешное выполнение</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ScResult<Event>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ScResult<Event>> PutEvent(Guid id, DateTime starts, DateTime ends, string name, Guid imageId, Guid spaceId, int ticketQuantity = 0, string description = "")
        {
            var result = new Event()
            {   Id=id,
                Starts = starts,
                Ends = ends,
                Name = name,
                Description = description,
                ImageId = imageId,
                SpaceId = spaceId,
                TicketsQuantity = ticketQuantity
            };
            await _validator.ValidateAndThrowAsync(result);
            return await _mediator.Send(new UpdateEventCommand(result));
        }

        // DELETE api/events/{id}
        /// <summary>
        /// убирать мероприятие по его guid
        /// </summary>
        /// <param name="id">guid мероприятия, которое нужно убрать</param>
        /// <returns>строка об успехе операции</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Плохие данные клиента</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ScResult<string>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ScResult<string>> DeleteEvent(Guid id) => await _mediator.Send(new DeleteEventCommand(id));
    }
}
