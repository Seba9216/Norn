using Microsoft.EntityFrameworkCore;
using Norn.Models.Entities;

namespace Norn.Repository;

public class RoleRepository : ListingRepo<Role>, IRoleRepository
{
    NornContext _nornContext;
    public RoleRepository(NornContext context) : base(context)
    {
        _nornContext = context;
    }

    public async Task<Role?> GetRoleByName(string roleName)
    {
        return await _nornContext.Roles.SingleOrDefaultAsync(x => x.RoleName == roleName);
    }
}
