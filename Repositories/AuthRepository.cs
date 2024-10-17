using AdminCrud.Data;
using AdminCrud.Models.Domain;
using Dapper;

namespace AdminCrud.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DapperContext _context;
        public AuthRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            using (var connection = _context.CreateConnection())
            {
                string query = @"INSERT INTO [users] (name, email, password, roleid) VALUES (@name, @email, @password, @role)";
                await connection.ExecuteAsync(query, new
                {
                    name = user.Name,
                    email = user.Email,
                    password = user.Password,
                    role = user.RoleId
                });
            }
        }

        public Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            using (var connection = _context.CreateConnection())
            {
                string query = @"SELECT * FROM [users] WHERE email = @email";
                return await connection.QuerySingleOrDefaultAsync<User>(query, new { Email = email });
            }
        }

        public async Task<Role> GetUserRole(string RoleName)
        {
            using (var connection = _context.CreateConnection())
            {
                string query = @"SELECT * FROM [Roles] WHERE name = @name";
                return await connection.QuerySingleOrDefaultAsync<Role>(query, new { name = RoleName });
            }
        }
    }
}
