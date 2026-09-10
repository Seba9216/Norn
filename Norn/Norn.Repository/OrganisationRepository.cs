
using Microsoft.EntityFrameworkCore;
using Norn.Models.Entities;
using Norn.Models.Models.Mappers;
using Norn.Models.Models.Requests;

namespace Norn.Repository;

public class OrganisationRepository : ListingRepo<Organisation>, IOrganisationRepository
{
    NornContext _nornContext;
    public OrganisationRepository(NornContext context) : base(context)
    {
        _nornContext = context;
    }

    public async Task<List<int>> GetRelatedRooms(int id)
    {
        return await _nornContext.OrganisationRoom.AsNoTracking().Where(x => x.OrganisationId == id).Select(x => x.RoomId).ToListAsync();
    }
    public async Task<Models.Models.Organisation> CreateOrganisation(CreateOrganisationRequest createOrganisationRequest)
    {

        var createdRoom = new Organisation
        {
            Name = createOrganisationRequest.Name
        };

        _nornContext.Add(createdRoom);
        await _nornContext.SaveChangesAsync();

        if (createOrganisationRequest.RoomIds != null)
        {
            foreach (var roomToAdd in createOrganisationRequest.RoomIds)
            {
                var foundroom = await _nornContext.Rooms.SingleOrDefaultAsync(x => x.Id == roomToAdd);
                if (foundroom is not null)
                {
                    _nornContext.OrganisationRoom.Add(new OrganisationRoom
                    {
                        OrganisationId = createdRoom.Id,
                        RoomId = foundroom.Id
                    });
                }
            }
            await _nornContext.SaveChangesAsync();
        }

        return OrganisationMapper.MapToModel(createdRoom.Id, createdRoom.Name, null);
    }


    public async Task<bool> DeleteOrganisation(int id)
    {
        return await RemoveByPrimaryKey(id);
    }
    public async Task<Models.Models.Organisation> UpdateOrganisation(UpdateOrganisationRequest request)
    {
        var singularEntity = await GetByPrimaryKey(request.Id);
        if (singularEntity != null)
        {
            singularEntity.Name = request.Name;
            if (request.RoomIds != null)
            {

                var roomsToRemove = (await GetRelatedRooms(request.Id)).Except(request.RoomIds).ToList();
                foreach (var roomToRemove in roomsToRemove)
                {
                    var entityToRemove = await _nornContext.OrganisationRoom.SingleOrDefaultAsync(x => x.OrganisationId == request.Id && x.RoomId == roomToRemove);
                    if (entityToRemove is not null)
                    {
                        _nornContext.OrganisationRoom.Remove(entityToRemove);
                    }
                }
                var roomsToAdd = request.RoomIds.Except(await GetRelatedRooms(request.Id)).ToList();
                foreach (var roomToAdd in roomsToAdd)
                {
                    _nornContext.OrganisationRoom.Add(new OrganisationRoom { OrganisationId = singularEntity.Id, RoomId = roomToAdd });
                }
                await _nornContext.SaveChangesAsync();
            }

        }
        else
        {
            throw new InvalidOperationException("Orginisation not found");
        }
        return OrganisationMapper.MapToModel(singularEntity);
    }

    public async Task<List<Models.Models.Organisation>> GetAllOrganisations()
    {
        var result = await GetAllEntitiesFromTable(q =>
            q.Include(o => o.OrganisationRooms)
             .ThenInclude(or => or.Room));

        var resultAsModels = result.Select(x =>
        {
            return OrganisationMapper.MapToModel(x);
        }).ToList();
        return resultAsModels;
    }
}
