using AdminCrud.Models.Domain;
using AdminCrud.Models.DTO;
using AdminCrud.Repositories;
using Microsoft.AspNetCore.Http;

namespace AdminCrud.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IAuthRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task LoginAsync(LoginDTO dto)
        {
            // Check if user does not exists
            var user = await _repository.GetUserByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                throw new Exception("Invalid credentials");
            }

            // Login
            _httpContextAccessor.HttpContext.Session.SetInt32("UserId", user.Id);
            _httpContextAccessor.HttpContext.Session.SetString("UserName", user.Name);
            _httpContextAccessor.HttpContext.Session.SetString("UserEmail", user.Email);
            _httpContextAccessor.HttpContext.Session.SetInt32("RoleId", user.RoleId);
        }

        public async Task RegisterAsync(RegisterDTO dto)
        {
            // Check if the user already exists by email
            var user = await _repository.GetUserByEmailAsync(dto.Email);
            if (user != null)
            {
                throw new Exception("User already exists");
            }

            var role = await _repository.GetUserRole("User");

            // Create a new user
            var newUser = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = role.Id
            };
            await _repository.CreateUserAsync(newUser);
        }
        public Task Logout()
        {
            // Clear the session when the user logs out
            _httpContextAccessor.HttpContext.Session.Clear();
            return Task.CompletedTask;
        }
    }
}
