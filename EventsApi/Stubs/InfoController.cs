using EventsApi.Stubs.Images;
using EventsApi.Stubs.Spaces;
using EventsApi.Stubs.Users;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;


namespace EventsApi.Stubs
{
    [Produces("application/json")]
    [Route("api/info")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        // GET: api/info/images
        /// <summary>
        /// получить все изображения
        /// </summary>
        /// <response code="200">Успешное выполнение</response>
        /// <returns>список изображений</returns>
        [HttpGet("images")]
        [ProducesResponseType(typeof(ScResult<IEnumerable<EventImage>>), 200)]
        [ProducesDefaultResponseType]
        public ScResult<IEnumerable<EventImage>> GetImages()
        {
            var images = TempImageData.GetAll();
            return new ScResult<IEnumerable<EventImage>>(images);
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
        [ProducesResponseType(typeof(EventImage), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ScResult<EventImage> GetImageById(Guid id)
        {
            var image = TempImageData.GetById(id);
            return new ScResult<EventImage>(image);
        }


        // GET: api/info/spaces
        /// <summary>
        /// get all spaces
        /// </summary>
        /// <returns>list of all spaces</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("spaces")]
        [ProducesResponseType(typeof(ScResult<IEnumerable<UserSpace>>), 200)]
        [ProducesDefaultResponseType]
        public ScResult<IEnumerable<UserSpace>> GetSpaces()
        {
            var spaces = TempSpaceData.GetAll();
            return new ScResult<IEnumerable<UserSpace>>(spaces);
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
        [ProducesResponseType(typeof(UserSpace), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ScResult<UserSpace> GetSpaceById(Guid id)
        {
            var space = TempSpaceData.GetById(id);
            return new ScResult<UserSpace>(space);
        }

        // GET: api/info/users
        /// <summary>
        /// get all users
        /// </summary>
        /// <returns>list of all users</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("users")]
        [ProducesResponseType(typeof(ScResult<IEnumerable<User>>), 200)]
        [ProducesDefaultResponseType]
        public ScResult<IEnumerable<User>> GetUsers()
        {
            var users = TempUserData.GetAll();
            return new ScResult<IEnumerable<User>>(users);
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
        [ProducesResponseType(typeof(ScResult<User>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ScResult<User> GetUserById(Guid id)
        {
            var user = TempUserData.GetById(id);
            return new ScResult<User>(user);
        }

    }
}
