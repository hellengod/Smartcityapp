using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartCityApp.Data;
using SmartCityApp.Models;
using SmartCityApp.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GoogleApi;
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Maps.Common;
using System.Linq;



namespace SmartCityApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            ApplicationDbContext context,
            IConfiguration configuration,
            ILogger<AuthController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        // Endpoint de Login
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin login)
        {
            // Validar os campos de login
            if (string.IsNullOrEmpty(login?.Username) || string.IsNullOrEmpty(login?.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var user = _context.Users.SingleOrDefault(u => u.Username == login.Username && u.Password == login.Password);

            if (user == null)
                return Unauthorized();

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }


        // Endpoint de Registro
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            try
            {
                // Validação abrangente
                if (request == null)
                    return BadRequest(new { message = "Dados de registro são obrigatórios" });

                if (string.IsNullOrWhiteSpace(request.Username))
                    return BadRequest(new { message = "Nome de usuário é obrigatório" });

                if (string.IsNullOrWhiteSpace(request.Password))
                    return BadRequest(new { message = "Senha é obrigatória" });

                if (request.Password.Length < 8)
                    return BadRequest(new { message = "A senha deve ter pelo menos 8 caracteres" });

                // Verificar usuário existente
                var existingUser = await _context.Users
                    .SingleOrDefaultAsync(u => u.Username == request.Username);

                if (existingUser != null)
                    return Conflict(new { message = "Nome de usuário já existe" });

                // Criar usuário
                var user = new User(request.Username, request.Password, request.Email);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Registrar informação de usuário registrado
                _logger.LogInformation($"Usuário registrado: {user.Username}");

                var response = new UserResponse
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email
                };

                return CreatedAtAction(nameof(Register), new { id = user.Id }, response);
            }
            catch (Exception ex)
            {
                // Registro de erro durante registro
                _logger.LogError(ex, "Erro durante o registro de usuário");
                return StatusCode(500, new { message = "Ocorreu um erro interno do servidor" });
            }
        }

        /*[HttpPost("optimized-route")]
        public async Task<IActionResult> GetOptimizedRoute([FromBody] RouteRequest request)
        {
            try
            {
                // Ajuste do objeto DirectionsRequest
                var directionsRequest = new DirectionsRequest
                {
                    Origin = request.Waypoints.First(), 
                    Destination = request.Waypoints.Last(),
                    WayPoints = request.Waypoints
                     .Skip(1)
                     .Take(request.Waypoints.Count - 2)
                     .Select(w => new GoogleApi.Entities.Maps.Directions.Request.WayPoint(w, false))
                     .ToList(),
                    OptimizeWaypoints = true,
                    Key = _configuration["GoogleMaps:ApiKey"]
                };


                // Faz a requisição à API do Google
                var response = await GoogleApi.GoogleMaps.Directions.QueryAsync(directionsRequest);

                if (response.Status != GoogleApi.Entities.Common.Enums.Status.Ok)
                {
                    return BadRequest($"Erro da API do Google Maps: {response.ErrorMessage}");
                }

                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }*/

        // Método para gerar o token JWT
        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
