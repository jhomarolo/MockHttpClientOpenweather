using Openweather.Controllers;
using Openweather.Models;
using Openweather.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Openweather.Test
{
    public class UserControllerTest
    {
        protected UsersController ControllerUnderTest { get; }

        protected Mock<ILogger<UsersController>> LoggerMock { get; }
        protected Mock<IUserService> UserServiceMock { get; }
        protected Mock<IHttpClientHelper> HttpClientHelperMock { get; }

        public UserControllerTest()
        {

            LoggerMock = new Mock<ILogger<UsersController>>();
            UserServiceMock = new Mock<IUserService>();
            HttpClientHelperMock = new Mock<IHttpClientHelper>();

            ControllerUnderTest = new UsersController(UserServiceMock.Object, LoggerMock.Object, HttpClientHelperMock.Object);
            ControllerUnderTest.ControllerContext.HttpContext = new DefaultHttpContext();
        }
        [Fact]
        public void When_Authenticate_Model_Is_Null()
        {
            // Arrange;
            Login loginUser = null;

            // Act 
            UserServiceMock.Setup(i => i.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(It.IsAny<Token>());

            var result = ControllerUnderTest.Authenticate(loginUser);
            // Assert
            Assert.Null(result);
        }


        
    }
}
