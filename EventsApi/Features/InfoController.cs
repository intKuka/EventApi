using EventsApi.MongoDb;
using EventsApi.RabbitMq;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;


namespace EventsApi.Features
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

        ///// <summary>
        ///// поставить изображение по guid на удаление в очередь
        ///// </summary>
        ///// <param name="imageGuid">guid идентификатор изображения</param>
        //[HttpDelete("delete_image/{imageGuid:guid}")]
        //public Task DeleteImage(Guid imageGuid)
        //{
        //    _imageDeletionSender.SendEvent(imageGuid);

        //    var events = _eventRepo.GetAllEvents().Result.ToList();
        //    foreach (var e in events.Where(e => e.ImageId == imageGuid))
        //    {
        //        e.ImageId = null;
        //    }

        //    return Task.CompletedTask;
        //}

        ///// <summary>
        ///// поставить пространство по guid на удаление в очередь
        ///// </summary>
        ///// <param name="spaceGuid">guid идентификатор пространства</param>
        //[HttpDelete("delete_space/{spaceGuid:guid}")]
        //public async Task DeleteSpace(Guid spaceGuid)
        //{
            
        //}

    }
}
