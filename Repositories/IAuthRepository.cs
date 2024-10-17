 using AdminCrud.Models.Domain;
 using AdminCrud.Models.DTO;

namespace AdminCrud.Repositories
{
    public interface IAuthRepository
    {
        Task CreateUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);    
        Task<Role> GetUserRole(string roleId);
    }
}
