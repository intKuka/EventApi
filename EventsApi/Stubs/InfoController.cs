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
        private readonly HttpClient _httpClient;

        public InfoController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        // GET: api/info/images
        /// <summary>
        /// получить все изображения
        /// </summary>
        /// <returns>список всех изображенией</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <returns>список изображений</returns>
        [HttpGet("images")]
        [ProducesResponseType(typeof(ScResult<string>), 200)]
        [ProducesDefaultResponseType]
        public async Task<ScResult<string>> GetImages()
        {
            using var response = await _httpClient.GetAsync("http://localhost:5051/images");
            return new ScResult<string>(await response.Content.ReadAsStringAsync());
        }

        // GET: api/info/spaces
        /// <summary>
        /// получить все пространства
        /// </summary>
        /// <returns>список всех пространств</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("spaces")]
        [ProducesResponseType(typeof(ScResult<string>), 200)]
        [ProducesDefaultResponseType]
        public async Task<ScResult<string>> GetSpaces()
        {
            using var response = await _httpClient.GetAsync("http://localhost:5093/spaces");
            return new ScResult<string>(await response.Content.ReadAsStringAsync());
        }

        // GET: api/info/users
        /// <summary>
        /// получить всех пользователей
        /// </summary>
        /// <returns>список всех пользователей</returns>
        /// <response code="200">Успешное выполнение</response>
        [HttpGet("users")]
        [ProducesResponseType(typeof(ScResult<string>), 200)]
        [ProducesDefaultResponseType]
        public async Task<ScResult<string>> GetUsers()
        {
            using var response = await _httpClient.GetAsync("http://localhost:5018/users");
            return new ScResult<string>(await response.Content.ReadAsStringAsync());
        }

    }
}
