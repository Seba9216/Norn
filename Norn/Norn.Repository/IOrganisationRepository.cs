using Norn.Models.Models.Requests;

namespace Norn.Repository;

public interface IOrganisationRepository
{
    public Task<Models.Models.Organisation> CreateOrganisation(CreateOrganisationRequest createOrganisationRequest);

    public Task<Models.Models.Organisation> UpdateOrganisation(CreateOrganisationRequest createOrganisationRequest);

    public Task<Models.Models.Organisation> DeleteOrganisation(int id);

    public Task<List<Models.Models.Organisation>> GetAllOrganisations(); 
}