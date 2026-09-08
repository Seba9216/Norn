using Microsoft.EntityFrameworkCore;
using Norn.Models.Entities;
using Norn.Models.Models.Mappers;
using Norn.Models.Models.Requests;
using System.Security;

namespace Norn.Repository;

public class RoomRepository : ListingRepo<Room>, IRoomRepository
{
    NornContext _nornContext;
    public RoomRepository(NornContext context) : base(context)
    {
        _nornContext = context;
    }
    public async Task<Models.Models.Room> CreateRoom(CreateRoomRequest request)
    {
        var entity = new Room
        {
            Name = request.Name,
            Monday = request.Monday,
            Tuesday = request.Tuesday,
            Wednesday = request.Wensday,
            Thursday = request.Thursday,
            Friday = request.Friday,
            FromHour = request.FromHour,
            ToHour = request.ToHour,
            Saturday = request.Saturday,
            Sunday = request.Sunday,
            TimeLease = request.TimeLease,
            Increment = request.Increment,
        };

        _nornContext.Rooms.Add(entity);


        await _nornContext.SaveChangesAsync();
        if (request.OrganisationIds != null)
        {
            foreach (var org in request.OrganisationIds)
            {
                var foundorg = await _nornContext.Organisations.SingleOrDefaultAsync(x => x.Id == org);
                if (foundorg is not null)
                {
                    _nornContext.OrganisationRoom.Add(new OrganisationRoom
                    {
                        OrganisationId = foundorg.Id,
                        RoomId = entity.Id
                    });
                }
                await _nornContext.SaveChangesAsync();
            }
        }
        return RoomMapper.MapToModel(entity);
    }
    public async Task<Models.Models.Room> UpdateByRoom(UpdateRoomRequest request)
    {
        var singularEntity = await GetByPrimaryKey(request.Id);
        if(singularEntity != null)
        {
            singularEntity.Saturday = request.Saturday;
            singularEntity.Sunday = request.Sunday;
            singularEntity.Thursday = request.Thursday;
            singularEntity.Wednesday = request.Wensday;
            singularEntity.Tuesday = request.Tuesday;
            singularEntity.Monday = request.Monday;
            singularEntity.FromHour = request.FromHour;
            singularEntity.ToHour = request.ToHour;
            singularEntity.TimeLease = request.TimeLease;
            singularEntity.Increment = request.Increment;
            if(request.OrganisationIds != null)
            {
                var currentOrgs = await GetAllRelatedOrgs(request.Id);
                
                var orgsToRemove = currentOrgs.Except(request.OrganisationIds).ToList();
                foreach(var orgToRemove in orgsToRemove)
                {
                    var entityToRemove = await _nornContext.OrganisationRoom.SingleOrDefaultAsync(x => x.OrganisationId == request.Id && x.RoomId == orgToRemove);
                    if(entityToRemove is not null)
                    {
                        _nornContext.OrganisationRoom.Remove(entityToRemove);
                    }
                }
                var orgsToAdd = request.OrganisationIds.Except(currentOrgs);
                foreach (var orgToAdd in orgsToAdd)
                {
                    _nornContext.OrganisationRoom.Add(new OrganisationRoom { RoomId = singularEntity.Id, OrganisationId= orgToAdd });

                }
                await _nornContext.SaveChangesAsync();
            }
            return RoomMapper.MapToModel(singularEntity);
        }
        else
        {
            throw new InvalidOperationException("could not find room");
        }

    } 
    public async Task<List<Models.Models.Room>> GetAllRooms()
    {
        var result = await GetAllEntitiesFromTable();
        return result.Select(x =>
        {
            return RoomMapper.MapToModel(x);
        }).ToList();
    }
    public async Task<bool> DeleteRoom(int id)
    {
        var foundEntity = await GetByPrimaryKey(id);
        if (foundEntity is not null)
        {
            _nornContext.Remove(foundEntity);
            await _nornContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
    public async Task<List<int>> GetAllRelatedOrgs(int id)
    {
        return await _nornContext.OrganisationRoom.Where(x => x.RoomId == id).Select(x => x.OrganisationId).ToListAsync();
    }
}
