
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Norn.Repository;

public abstract class ListingRepo<TEntity>
    where TEntity : class
{
    protected readonly NornContext _context;

    protected ListingRepo(NornContext context)
    {
        _context = context;
    }
    public async Task<List<TEntity>> GetAllEntitiesFromTable(
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryBuilder = null)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (queryBuilder != null)
        {
            query = queryBuilder(query);
        }

        return await query.ToListAsync();
    }
    public async Task<bool> RemoveByPrimaryKey(int primaryKey)
    {
        try
        {
            var found = await GetByPrimaryKey(primaryKey);
            _context.Remove(found);
            await _context.SaveChangesAsync();
            return true;
        }catch(Exception e)
        {
            return false;
        }
    }
         
    public async Task<TEntity?> GetByPrimaryKey(int primaryKey)
    {
        return await _context.Set<TEntity>().FindAsync(primaryKey);
    }
}
