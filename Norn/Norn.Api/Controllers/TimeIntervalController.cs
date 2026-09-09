using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Norn.Repository;

namespace Norn.Api.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class TimeIntervalController : Controller
{
    ITimeIntervalRepository _timeIntervalRepository;
    public TimeIntervalController(ITimeIntervalRepository timeIntervalRepository)
    {
        _timeIntervalRepository = timeIntervalRepository;
    }

    [HttpGet("{roomId}")]
    public async Task<IActionResult> GetTimerInterValsByRoomId([FromRoute] int roomId)
    {
       return Ok(await _timeIntervalRepository.GetRoomRelatedTimeIntervals(roomId));
    }
}
