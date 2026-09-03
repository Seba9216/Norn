using Norn.Models.Entities;

namespace Norn.Repository;

public  interface IUserRepository
{
    public Task<Models.Models.User?> GetUserByEmail(string email);
    public Task<bool> CreateUser(Models.Models.User user);
    public Task<List<Models.Models.User>> GetAllUsers();

}