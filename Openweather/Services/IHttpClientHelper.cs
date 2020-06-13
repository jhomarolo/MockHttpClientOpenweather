using System.Net.Http;
using System.Threading.Tasks;

namespace Openweather.Services
{

    public interface IHttpClientHelper
    {
        Task<TResult> GetAsync<TResult>(string requestUri);
       
        HttpClient HttpClient { get; set; }

    }

}
