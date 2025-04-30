namespace SmartCityApp.ViewModels
{
    public class RouteRequest
    {
        public List<string> Waypoints { get; set; }

        // Construtor para garantir que a lista seja inicializada
        public RouteRequest()
        {
            Waypoints = new List<string>();
        }
    }
}
