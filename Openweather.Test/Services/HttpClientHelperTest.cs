using AutoFixture;
using Openweather.Models;
using Openweather.Services;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Openweather.Test
{
    public class HttpClientHelperTest
    {
        protected HttpClientHelper HttpClientHelperUnderTest { get; }
        public HttpClientHelperTest()
        {
            HttpClientHelperUnderTest = new HttpClientHelper();
        }

        /// <summary>
        /// Weather info by city name test cases are resides.
        /// </summary>
        public class GetAsyncHttpHelper : HttpClientHelperTest
        {
            [Fact]
            public async Task When_GetAsync_Returns_Success_Result()
            {
                //Arrange;
                var result = new List<CityInfo>()  {
                     new CityInfo() { baseinfo="stations",timezone=-10800, name="Alfenas" }
                };
                var httpMessageHandler = new Mock<HttpMessageHandler>();
                var fixture = new Fixture();

                // Setup Protected method on HttpMessageHandler mock.
                httpMessageHandler.Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                    )
                    .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                    {
                        HttpResponseMessage response = new HttpResponseMessage();
                        response.StatusCode = System.Net.HttpStatusCode.OK;//Setting statuscode
                        response.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(result)); // configure your response here
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json"); //Setting media type for the response
                        return response;
                    });

                var httpClient = new HttpClient(httpMessageHandler.Object);
                httpClient.BaseAddress = fixture.Create<Uri>();

                HttpClientHelperUnderTest.HttpClient = httpClient; //Mocking setting Httphandler object to interface property.

                //Act
                var weatherResult = await HttpClientHelperUnderTest.GetAsync<List<CityInfo>>(string.Empty);

                // Assert
                Assert.NotNull(weatherResult);
            }
        }
    }
}
