using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Norn.Api.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class OrganisationController : Controller
{

}
