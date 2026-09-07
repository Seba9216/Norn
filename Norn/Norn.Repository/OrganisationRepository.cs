
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

    public async Task<Models.Models.Organisation> CreateOrganisation(CreateOrganisationRequest createOrganisationRequest)
    {

        var createdRoom = new Organisation
        {
            Name = createOrganisationRequest.Name
        };
        _nornContext.Add(createdRoom);
        await _nornContext.SaveChangesAsync();


        //TODO HUSK at lav den her når rooms repo kommer på
        return OrganisationMapper.MapToModel(createdRoom.Id, createdRoom.Name, null); 
    }


    public Task<Models.Models.Organisation> DeleteOrganisation(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Models.Models.Organisation>> GetAllOrganisations()
    {
        var result = await GetAllEntitiesFromTable();
        var resultAsModels = result.Select(x =>
        {
            return OrganisationMapper.MapToModel(x.Id, x.Name,null);
        }).ToList();
        return resultAsModels;
    }

    public Task<Models.Models.Organisation> UpdateOrganisation(CreateOrganisationRequest createOrganisationRequest)
    {
        throw new NotImplementedException();
    }
}
