using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Norn.Models.Models.Requests;
using Norn.Repository;

namespace Norn.Api.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class BookingController : Controller
{
    IBookingRepository _bookingRepository;
    public BookingController(IBookingRepository repository)
    {
        _bookingRepository = repository;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllBookings()
    {
        return Ok(await _bookingRepository.GetAllBookings());
    }

    [HttpPost]
    public async Task<IActionResult> MakeBookingRequest(CreateBookingRequest request)
    {
        var result = await _bookingRepository.CreateBookingRequest(request);
        if (result is null)
        {
            return BadRequest();
        }
        return Ok(result);
    }
    [HttpPut("approve")]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> ApproveBookingRequest([FromBody] int id)
    {
        var result = await _bookingRepository.ApproveBookingById(id);
        if (result is null) return BadRequest();
        return Ok(result);
    }
    [HttpPut("cancel")]
    public async Task<IActionResult> CancelBookingRequest([FromBody] int id)
    {
        var result = await _bookingRepository.CancelBookingById(id);
        if (result is null) return BadRequest();
        return Ok(result);
    }
}
