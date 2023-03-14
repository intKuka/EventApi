using EventsApi.Features.Events.Commands;
using EventsApi.Features.Events.Data;
using EventsApi.Features.Events.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace EventsApi.Features.Events
{
    [Produces("application/json")]
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly IValidator<Event> _validator;
        public EventsController(IMediator mediator, IValidator<Event> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }


        // GET api/events
        /// <summary>
        /// get all events
        /// </summary>
        /// <returns>list of all events</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Event>), 200)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetEvents()
        {
            var events = await _mediator.Send(new GetEventsQuery());  
            if(events == null)
            {
                return NoContent();
            }
            return Ok(events);
        }

        // GET api/events/{id}
        /// <summary>
        /// searchs and returns an event by its id
        /// </summary>
        /// <param name="id">existing event id</param>
        /// <returns>existing event</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Event), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetEventById(Guid id)
        {
            var event_ = await _mediator.Send(new GetEventByIdQuery(id));
            if (event_ != null)
            {
                return Ok(event_);
            }
            return NotFound();
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
        /// <param name="description">description</param>
        /// <returns>created event</returns>
        /// <remarks>data installation format: yyyy-mm-ddThh:MM:ss</remarks>
        [HttpPost]
        [ProducesResponseType(typeof(Event), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> PostEvent(DateTime starts, DateTime ends, string name, Guid imageId, Guid spaceId, string description = "")
        {
            List<string> errors = new();
            var event_ = new Event()
            {
                Id = Guid.NewGuid(),
                Starts = starts,
                Ends = ends,
                Name = name,
                Description = description,
                ImageId = imageId,
                SpaceId = spaceId
            };
            if (!TempImageData.CheckImage(event_.ImageId)) errors.Add($"Image {event_.ImageId} not exists");
            if (!TempSpaceData.CheckSpace(event_.SpaceId)) errors.Add($"Space {event_.SpaceId} not exists");
            var validationResults = _validator.Validate(event_);
            foreach (var fail in validationResults.Errors)
            {
                errors.Add($"{fail.PropertyName} failure. Error: {fail.ErrorMessage}");
            }

            if (errors.Count > 0)
            {
                return BadRequest(errors);
            }
            return Ok(await _mediator.Send(new CreateEventCommand(event_)));
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
        /// <param name="description">description</param>
        /// <returns>altered event</returns>
        /// <remarks>data installation format: yyyy-mm-ddThh:MM:ss</remarks>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Event), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> PutEvent(Guid id, DateTime starts, DateTime ends, string name, Guid imageId, Guid spaceId, string description = "")
        {
            List<string> errors = new();
            var event_ = new Event()
            {
                Id = id,
                Starts = starts,
                Ends = ends,
                Name = name,
                Description = description,
                ImageId = imageId,
                SpaceId = spaceId
            };
            if (!TempImageData.CheckImage(event_.ImageId)) errors.Add($"Image {event_.ImageId} not exists");
            if (!TempSpaceData.CheckSpace(event_.SpaceId)) errors.Add($"Space {event_.SpaceId} not exists");
            var validationResults = _validator.Validate(event_);
            foreach (var fail in validationResults.Errors)
            {
                errors.Add($"{fail.PropertyName} failure. Error: {fail.ErrorMessage}");
            }

            if (errors.Count > 0)
            {
                return BadRequest(errors);
            }
            event_ = await _mediator.Send(new UpdateEventCommand(event_));
            if (event_ != null) return Ok(event_);
            return NotFound();
        }

        // DELETE api/events/{id}
        /// <summary>
        /// removes event from the list
        /// </summary>
        /// <param name="id">id of evetn to remove</param>
        /// <returns>result string</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Event), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteEvent(Guid id)
        {
            var result = await _mediator.Send(new DeleteEventCommand(id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
