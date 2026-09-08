using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Norn.Models.Models;
using Norn.Repository;

namespace Norn.Api.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class RoomController : Controller
{
    private IRoomRepository _roomRepository;
    public RoomController(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom(Models.Models.Requests.CreateRoomRequest createRoomRequest)
    {
        var result = await _roomRepository.CreateRoom(createRoomRequest);
        if (result != null) return Ok(result);
        return BadRequest();
    }
    [HttpGet]
    public async Task<List<Room>> GetAllRooms()
    {
        return await _roomRepository.GetAllRooms();
    }
    [HttpGet("Related/{id}")]
    public async Task<List<int>> GetAllRelatedOrgs(int id)
    {
        return await _roomRepository.GetAllRelatedOrgs(id);
    }
}
