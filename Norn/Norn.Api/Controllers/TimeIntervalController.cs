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

    [HttpGet("{roomid}/{from}/{to}")]
    public async Task<IActionResult> GetTimeInterValIdFromTimeAndRoomId([FromRoute] int roomId, [FromRoute] DateTime from, [FromRoute] DateTime to)
    {
        var result = await _timeIntervalRepository.GetIdFromTimeAndRoomId(from, to, roomId);
        if(result is null)
        {
            return BadRequest();
        }
        return Ok(result);
    }
}
