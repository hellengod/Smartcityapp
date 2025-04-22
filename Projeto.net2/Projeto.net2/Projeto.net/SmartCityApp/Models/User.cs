namespace SmartCityApp.Models
{
    public class User
    {
        public int Id { get; set; }

        // Agora as propriedades são não anuláveis, mas precisam ser inicializadas no construtor
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        // Construtor para garantir que as propriedades sejam inicializadas corretamente
        public User(string username, string password, string email)
        {
            // Garantir que as propriedades não sejam nulas ou vazias
            Username = string.IsNullOrWhiteSpace(username) ? throw new ArgumentNullException(nameof(username), "Username is required.") : username;
            Password = string.IsNullOrWhiteSpace(password) ? throw new ArgumentNullException(nameof(password), "Password is required.") : password;
            Email = string.IsNullOrWhiteSpace(email) ? throw new ArgumentNullException(nameof(email), "Email is required.") : email;
        }
    }
}
