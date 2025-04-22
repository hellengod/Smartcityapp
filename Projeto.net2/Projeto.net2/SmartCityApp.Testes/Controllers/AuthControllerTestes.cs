using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SmartCityApp.Controllers;
using SmartCityApp.Data;
using SmartCityApp.Models;
using SmartCityApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;

namespace SmartCityApp.Tests
{
    public class AuthControllerTestes
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<ILogger<AuthController>> _mockLogger;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockContext = new Mock<ApplicationDbContext>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockLogger = new Mock<ILogger<AuthController>>();
            _controller = new AuthController(_mockContext.Object, _mockConfiguration.Object, _mockLogger.Object);
        }

        [Fact]
        public void Login_ReturnsOk_WithToken_WhenUserIsFound()
        {
            // Arrange
            var login = new UserLogin { Username = "test", Password = "password" };
            var user = new User("test", "password", "test@example.com");
            _mockContext.Setup(c => c.Users).Returns(new List<User> { user }.AsQueryable().BuildMockDbSet().Object);
            _mockConfiguration.Setup(c => c["Jwt:Key"]).Returns("supersecretkey");
            _mockConfiguration.Setup(c => c["Jwt:Issuer"]).Returns("testissuer");

            // Act
            var result = _controller.Login(login);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task Register_ReturnsCreatedAtAction_WhenUserIsRegistered()
        {
            // Arrange
            var request = new RegisterUserRequest("test", "password", "test@example.com", "Test User");
            _mockContext.Setup(c => c.Users).Returns(new List<User>().AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = await _controller.Register(request);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdAtActionResult.StatusCode);
            Assert.NotNull(createdAtActionResult.Value);
        }
        [Fact]
        public async Task GetOptimizedRoute_ReturnsOkResult()
        {
            // Arrange
            var waypoints = new List<string> { "PointA", "PointB", "PointC" };
            var request = new RouteRequest { Waypoints = waypoints };

            var directionsResponse = new DirectionsResponse
            {
                Status = GoogleApi.Entities.Common.Enums.Status.Ok
            };

            // Mock the Google Maps API response
            var mockGoogleMaps = new Mock<IGoogleMapsService>();
            mockGoogleMaps.Setup(x => x.Directions.QueryAsync(It.IsAny<DirectionsRequest>()))
                          .ReturnsAsync(directionsResponse);

            // Inject the mock service into the controller
            _controller.GoogleMapsService = mockGoogleMaps.Object;

            // Act
            var result = await _controller.GetOptimizedRoute(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}