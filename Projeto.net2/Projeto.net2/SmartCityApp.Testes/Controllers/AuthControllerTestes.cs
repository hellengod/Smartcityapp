using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartCityApp.Controllers;
using SmartCityApp.Services;
using SmartCityApp.ViewModels;  // Namespace correto para RouteRequest
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;

namespace SmartCityApp.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IGoogleMapsService> _mockGoogleMapsService;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockGoogleMapsService = new Mock<IGoogleMapsService>();
            _controller = new AuthController(_mockGoogleMapsService.Object);
        }

        [Fact]
        public async Task GetOptimizedRoute_ReturnsOk_WhenRouteIsFound()
        {
            // Arrange
            var request = new RouteRequest
            {
                Waypoints = new List<string> { "pointA", "pointB" }
            };

            var response = new DirectionsResponse
            {
                Status = GoogleApi.Entities.Common.Enums.Status.Ok
            };

            _mockGoogleMapsService.Setup(s => s.DirectionsQueryAsync(It.IsAny<DirectionsRequest>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetOptimizedRoute(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task GetOptimizedRoute_ReturnsBadRequest_WhenNoWaypoints()
        {
            // Arrange
            var request = new RouteRequest(); // Sem waypoints

            // Act
            var result = await _controller.GetOptimizedRoute(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Waypoints are required.", badRequestResult.Value);
        }
    }
}
