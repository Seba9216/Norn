
using Microsoft.EntityFrameworkCore;
using Norn.Models.Entities;
using Norn.Models.Models.Mappers;
using Norn.Models.Models.Requests;

namespace Norn.Repository;

internal class OrganisationRepository : ListingRepo<Organisation>, IOrganisationRepository
{
    NornContext _nornContext;
    public OrganisationRepository(NornContext context) : base(context)
    {
        _nornContext = context;
    }

    public async Task<Models.Models.Organisation> CreateOrganisation(CreateOrganisationRequest createOrganisationRequest)
    {

        var createdRoom = new Organisation
        {
            Name = createOrganisationRequest.Name
        };
        _nornContext.Add(createdRoom);
        await _nornContext.SaveChangesAsync();

        foreach(var room in createOrganisationRequest.RoomIds)
        {
            var roomToFind = await _nornContext.Rooms.SingleOrDefaultAsync(x => x.Id == room);
            if (roomToFind is not null)
            {
                 roomToFind.OrginisationIds.Add(createdRoom.Id);
            } 
        }
        await _nornContext.SaveChangesAsync();


        //TODO HUSK at lav den her når rooms repo kommer på
        return OrganisationMapper.MapToModel(createdRoom.Id, createdRoom.Name, null); 
    }
}
