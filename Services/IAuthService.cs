using AdminCrud.Models.Domain;
using AdminCrud.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AdminCrud.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDTO dto);
        Task LoginAsync(LoginDTO dto);
        Task Logout();
    }
}
