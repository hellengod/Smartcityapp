namespace SmartCityApp.Models
{
    public class RegisterUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
       public RegisterUserRequest(string username, string password, string email, string fullName)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
        }
    } 
}
