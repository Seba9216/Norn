using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Norn.Models.Models;
using Norn.Repository;

namespace Norn.Api.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class OrganisationController : Controller
{
    private IOrganisationRepository _organisationRepository;

    public OrganisationController(IOrganisationRepository organisationRepository)
    {
        _organisationRepository = organisationRepository;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateOrganisation(Models.Models.Requests.CreateOrganisationRequest createOrganisationRequest)
    {
        var result = await _organisationRepository.CreateOrganisation(createOrganisationRequest);
        if (result != null) return Ok(result);
        return BadRequest();
    }
    [HttpGet]
    public async Task<List<Organisation>> GetAllOrganisations()
    {
        return(await _organisationRepository.GetAllOrganisations());
    }
    [HttpGet("Related/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<List<int>> GetRelatedRooms([FromRoute]int id)
    {
        return await _organisationRepository.GetRelatedRooms(id);
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<bool> DeleteOrganistation([FromRoute]int id)
    {
        return await _organisationRepository.DeleteOrganisation(id);
    }
}
