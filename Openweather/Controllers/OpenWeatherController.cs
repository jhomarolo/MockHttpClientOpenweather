using Openweather.Models;
using Openweather.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Openweather.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OpenWeatherController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        private readonly IHttpClientHelper _httpClientHelper;
        public OpenWeatherController(IUserService userService, ILogger<UsersController> logger, IHttpClientHelper httpClientHelper)
        {
            this._userService = userService;
            this._logger = logger;
            this._httpClientHelper = httpClientHelper;
        }

        /// <summary>
        /// Returning weather information for particular city
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/getweatherbycityname")]
        public async Task<IActionResult> GetWeatherByCityName(string cityName)
        {
            var weatherInfo = await _userService.GetWeatherByCityName(cityName);
            return Ok(weatherInfo);
        }
    }
}