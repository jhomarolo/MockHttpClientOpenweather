using Openweather.Helper;
using Openweather.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Openweather.Services
{


    public class UserService : IUserService
    {
        private Login _login = null;

        private readonly AppSettings _appSettings;
        private readonly IHttpClientHelper _httpClientHelper;
        public UserService(IOptions<AppSettings> appSettings, IHttpClientHelper httpClientHelper)
        {
            _appSettings = appSettings.Value;
            _httpClientHelper = httpClientHelper ?? throw new ArgumentNullException(nameof(HttpClientHelper));

            _login = new Login
            {
                UserId = 1,
                Username = _appSettings.UserName,
                Password = _appSettings.Password,
            };
        }

        /// <summary>
        /// Returns all users
        /// </summary>
        /// <returns></returns>
        public async Task<CityInfo> GetWeatherByCityName(string cityName)
        {
            var url = _appSettings.WeatherApiBaseURI + _appSettings.WeatherApiGetByCityName + cityName + "&appid=" + _appSettings.Secret;
            return await _httpClientHelper.GetAsync<CityInfo>(url);
        }

        /// <summary>
        /// Token Generation
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Token Authenticate(string username, string password)
        {
            if (_login.Username != username || _login.Password != password)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, _login.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            return new Token { AccessToken = accessToken };
        }
    }
}