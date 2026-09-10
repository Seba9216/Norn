using Norn.Models.Entities;
using Norn.Models.Models.Requests;

namespace Norn.Repository;

public interface IUserRepository
{
    public Task<Models.Models.User?> GetUserByEmail(string email);
    public Task<bool> CreateUser(Models.Models.User user);
    public Task<List<Models.Models.User>> GetAllUsers();
    public Task<Models.Models.User> UpdateRoleForUser(PromoteUserRequest userRequest);
    public Task<bool> DeleteUserByEmail(string email);

}