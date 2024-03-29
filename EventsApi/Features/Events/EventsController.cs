﻿using EventsApi.Features.Events.CreateEvent;
using EventsApi.Features.Events.DeleteEvent;
using EventsApi.Features.Events.GetEventById;
using EventsApi.Features.Events.GetEvents;
using EventsApi.Features.Events.UpdateEvent;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;


namespace EventsApi.Features.Events;

[Produces("application/json")]
[Route("api/events")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventsController(IMediator mediator)
    {
        _mediator = mediator;
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
    // ReSharper disable once RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ScResult<Event>), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ScResult<Event>> GetEventById([FromRoute] Guid id)
    {
        return await _mediator.Send(new GetEventByIdQuery(id));
    }

    // POST api/events
    /// <summary>
    /// создать новое мероприятие
    /// </summary>
    /// <param name="update">объект мероприятие из тела json</param>
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
    public async Task<ScResult<Event>> CreateEvent([FromBody] Event update)
    {
        return await _mediator.Send(new CreateEventCommand(update));
    }

    // PUT api/events/{id}
    /// <summary>
    /// изменить мероприятие по его guid
    /// </summary>
    /// <param name="update">объект мероприятие из тела json</param>
    /// <returns>изменяет мероприятие</returns>
    /// <remarks>
    /// При обновлении количества билетов или добавления/удаление нумерации мест, все билеты анулируются
    /// </remarks>
    /// <remarks>
    /// формат установки даты: yyyy-mm-ddThh:MM:ss
    /// пример: 2023-03-15T16:40:20
    /// </remarks>
    /// <response code="400">Плохие данные клиента</response>
    /// <response code="200">Успешное выполнение</response>
    [HttpPut]
    [ProducesResponseType(typeof(ScResult<Event>), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ScResult<Event>> UpdateEvent([FromBody] Event update)
    {
        return await _mediator.Send(new UpdateEventCommand(update));
    }

    // DELETE api/events/{id}
    /// <summary>
    /// убирать мероприятие по его guid
    /// </summary>
    /// <param name="id">guid мероприятия, которое нужно убрать</param>
    /// <returns>строка об успехе операции</returns>
    /// <response code="200">Успешное выполнение</response>
    /// <response code="400">Плохие данные клиента</response>
    // ReSharper disable once RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ScResult<string>), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ScResult<string>> DeleteEvent([FromRoute] Guid id) => await _mediator.Send(new DeleteEventCommand(id));



}