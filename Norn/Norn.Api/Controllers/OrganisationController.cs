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
}
