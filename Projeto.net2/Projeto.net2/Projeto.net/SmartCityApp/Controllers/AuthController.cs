using Microsoft.AspNetCore.Mvc;
using SmartCityApp.ViewModels;  // Certifique-se de incluir o namespace correto
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;
using SmartCityApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCityApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IGoogleMapsService _googleMapsService;

        public AuthController(IGoogleMapsService googleMapsService)
        {
            _googleMapsService = googleMapsService;
        }

        [HttpPost]
        [Route("optimizedRoute")]
        public async Task<IActionResult> GetOptimizedRoute([FromBody] RouteRequest request)
        {
            if (request?.Waypoints == null || request.Waypoints.Count == 0)
            {
                return BadRequest("Waypoints are required.");
            }

            // Criando a lista de waypoints diretamente a partir das strings
            var directionsRequest = new DirectionsRequest
            {
                Waypoints = request.Waypoints.Select(w => new Waypoint { Location = w }).ToList() // Aqui usamos o "Waypoint" simples
            };

            var response = await _googleMapsService.DirectionsQueryAsync(directionsRequest);

            if (response.Status == GoogleApi.Entities.Common.Enums.Status.Ok)
            {
                return Ok(response);
            }

            return BadRequest("Failed to get optimized route.");
        }
    }
}
        