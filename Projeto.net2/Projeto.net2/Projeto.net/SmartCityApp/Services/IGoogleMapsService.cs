using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;
using System.Threading.Tasks;

namespace SmartCityApp.Services
{
    public interface IGoogleMapsService
    {
        Task<DirectionsResponse> DirectionsQueryAsync(DirectionsRequest request);
    }
}
