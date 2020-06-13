using Openweather.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Openweather.Services
{
    public interface IUserService
    {
        Token Authenticate(string username, string password);
        Task<CityInfo> GetWeatherByCityName(string cityName);
    }
}
