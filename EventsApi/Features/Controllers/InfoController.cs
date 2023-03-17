using EventsApi.Features.Images;
using EventsApi.Features.Models;
using EventsApi.Features.Spaces;
using EventsApi.Features.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EventsApi.Features.Controllers
{
    [Produces("application/json")]
    [Route("api/info")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        // GET: api/info/images
        /// <summary>
        /// get all images
        /// </summary>
        /// <returns>list of all images</returns>
        [HttpGet("images")]
        [ProducesResponseType(typeof(List<EventImage>), 200)]
        [ProducesDefaultResponseType]
        public ActionResult GetImages()
        {
            var images = TempImageData.GetAll();
            return Ok(images);
        }

        // GET api/info/images/{id}
        /// <summary>
        /// search and returns an image by its id
        /// </summary>
        /// <param name="id">existing image id</param>
        /// <returns>existing image</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Плохие данные клиента</response>
        [HttpGet("images/{id}")]
        [ProducesResponseType(typeof(Event), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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


        // GET: api/info/spaces
        /// <summary>
        /// get all spaces
        /// </summary>
        /// <returns>list of all spaces</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("spaces")]
        [ProducesResponseType(typeof(List<EventImage>), 200)]
        [ProducesDefaultResponseType]
        public ActionResult GetSpaces()
        {
            var spaces = TempSpaceData.GetAll();
            return Ok(spaces);
        }

        // GET api/info/spaces/{id}
        /// <summary>
        /// search and returns a space by its id
        /// </summary>
        /// <param name="id">existing space id</param>
        /// <returns>existing space</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Плохие данные клиента</response>
        [HttpGet("spaces/{id}")]
        [ProducesResponseType(typeof(Event), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        // GET: api/info/users
        /// <summary>
        /// get all users
        /// </summary>
        /// <returns>list of all users</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("users")]
        [ProducesResponseType(typeof(List<EventImage>), 200)]
        [ProducesDefaultResponseType]
        public ActionResult GetUsers()
        {
            var users = TempUserData.GetAll();
            return Ok(users);
        }

        // GET api/info/users/{id}
        /// <summary>
        /// search and returns an user by its id
        /// </summary>
        /// <param name="id">existing user id</param>
        /// <returns>existing user</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Плохие данные клиента</response>
        [HttpGet("users/{id}")]
        [ProducesResponseType(typeof(Event), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult GetUserById(Guid id)
        {
            var user = TempUserData.GetById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

    }
}
