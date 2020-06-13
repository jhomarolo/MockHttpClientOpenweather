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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        private readonly IHttpClientHelper _httpClientHelper;
        public UsersController(IUserService userService, ILogger<UsersController> logger, IHttpClientHelper httpClientHelper)
        {
            this._userService = userService;
            this._logger = logger;
            this._httpClientHelper = httpClientHelper;
        }

        /// <summary>
        /// Generate token based on basic authentication
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Login model)
        {
            this._logger.LogInformation("Token generation method invoked.");

            if (model == null)
                return null;
            var token = _userService.Authenticate(model.Username, model.Password);

            if (token == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            this._logger.LogInformation("Token generation method end.");

            return Ok(token);
        }

    }
}