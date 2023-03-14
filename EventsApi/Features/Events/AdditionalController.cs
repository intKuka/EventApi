using EventsApi.Features.Events.Commands;
using EventsApi.Features.Events.Data;
using EventsApi.Features.Events.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace EventsApi.Features.Events
{
    [Produces("application/json")]
    [Route("api/additional")]
    [ApiController]
    public class AdditionalController : ControllerBase
    {
        // GET: api/additional/images
        /// <summary>
        /// get all images
        /// </summary>
        /// <returns>list of all images</returns>
        [HttpGet("api/additional/images")]
        [ProducesResponseType(typeof(List<EventImage>), 200)]
        [ProducesDefaultResponseType]
        public ActionResult GetImages()
        {
            var images = TempImageData.GetAll();  
            if(images == null)
            {
                return NoContent();
            }
            return Ok(images);
        }

        // GET api/additional/images/{id}
        /// <summary>
        /// searchs and returns an image by its id
        /// </summary>
        /// <param name="id">existing image id</param>
        /// <returns>existing image</returns>
        [HttpGet("api/additional/images/{id}")]
        [ProducesResponseType(typeof(Event), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult GetImageById(Guid id)
        {
            var image = TempImageData.GetById(id);
            if (image != null)
            {
                return Ok(image);
            }
            return NotFound();
        }


        // GET: api/additional/spaces
        /// <summary>
        /// get all spaces
        /// </summary>
        /// <returns>list of all spaces</returns>
        [HttpGet("api/additional/spaces")]
        [ProducesResponseType(typeof(List<EventImage>), 200)]
        [ProducesDefaultResponseType]
        public ActionResult GetSpaces()
        {
            var spaces = TempSpaceData.GetAll();
            if (spaces == null)
            {
                return NoContent();
            }
            return Ok(spaces);
        }

        // GET api/additional/spaces/{id}
        /// <summary>
        /// searchs and returns a space by its id
        /// </summary>
        /// <param name="id">existing space id</param>
        /// <returns>existing space</returns>
        [HttpGet("api/additional/spaces/{id}")]
        [ProducesResponseType(typeof(Event), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult GetSpaceById(Guid id)
        {
            var space = TempSpaceData.GetById(id);
            if (space != null)
            {
                return Ok(space);
            }
            return NotFound();
        }

    }
}
