
using Microsoft.EntityFrameworkCore;

namespace Norn.Repository;

public abstract class ListingRepo<TEntity> 
    where TEntity : class
{
    protected readonly NornContext Context;

    protected ListingRepo(NornContext context)
    {
        Context = context;
    }

    public async Task<List<TEntity>> GetAllEntitiesFromTable()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }
}
