using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace SmartCityApp.Services
{
    public class GoogleMapsService : IGoogleMapsService
    {
        private readonly IConfiguration _configuration;

        public GoogleMapsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<DirectionsResponse> DirectionsQueryAsync(DirectionsRequest request)
        {
            request.Key = _configuration["GoogleMaps:ApiKey"];
            return await GoogleApi.GoogleMaps.Directions.QueryAsync(request);
        }
    }
}
