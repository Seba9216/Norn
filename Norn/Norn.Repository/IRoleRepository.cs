using Norn.Models.Entities;

namespace Norn.Repository;

public interface IRoleRepository
{
    public Task<Role?> GetRoleByName(string roleName);

}