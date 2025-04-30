namespace SmartCityApp.ViewModels
{
    public class UserResponse
    {
        public int Id { get; set; }

        // Propriedade obrigatória
        public required string Username { get; set; }

        // Propriedade obrigatória
        public required string Email { get; set; }
    }
}
