
using Microsoft.EntityFrameworkCore;
using Norn.Models.Entities;
using Norn.Models.Models.Mappers;
using Norn.Models.Models.Requests;
using System.Collections.Specialized;
using System.ComponentModel;

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

        //TODO HUSK at lav den her når rooms repo kommer på
        return OrganisationMapper.MapToModel(createdRoom.Id, createdRoom.Name, null);
    }


    public async Task<bool> DeleteOrganisation(int id)
    {
        try
        {
            var entity = await _nornContext.Organisations.SingleOrDefaultAsync(x => x.Id == id);
            _nornContext.Remove(entity);
            await _nornContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<List<Models.Models.Organisation>> GetAllOrganisations()
    {
        var result = await GetAllEntitiesFromTable();

        var resultAsModels = result.Select(x =>
        {
            if (x.OrganisationRooms != null)
            {
                return OrganisationMapper.MapToModel(x.Id, x.Name, x.OrganisationRooms.Select(x =>
                {
                    if (x.Room != null)
                    {
                        return OrganisationMapper.MapToModel(x.Room);
                    }
                    return null;
                }).ToList());
            }
            return OrganisationMapper.MapToModel(x.Id, x.Name, null);
        }).ToList();
        return resultAsModels;
    }

    public Task<Models.Models.Organisation> UpdateOrganisation(CreateOrganisationRequest createOrganisationRequest)
    {
        throw new NotImplementedException();
    }
}
