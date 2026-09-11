
using Microsoft.EntityFrameworkCore;
using Norn.Models.Entities;
namespace Norn.Repository;

public abstract class ListingRepo<TEntity>
    where TEntity : class, IEntity
{
    protected readonly NornContext _context;

    protected ListingRepo(NornContext context)
    {
        _context = context;
    }
    public async Task<List<TEntity>> GetAllEntities(
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
         
    public async Task<TEntity?> GetByPrimaryKey(int primaryKey, Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryBuilder = null)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (queryBuilder != null)
        {
            query = queryBuilder(query);
        }
        return await query.SingleOrDefaultAsync(x => x.Id == primaryKey);
    }
}
