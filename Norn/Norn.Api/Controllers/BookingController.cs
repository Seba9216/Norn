using Infrastructure.Connections;
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
    IRabbitConnector _rabbitConnectior;
    public BookingController(IBookingRepository repository,IRabbitConnector rabbitConnector)
    {
        _bookingRepository = repository;
        _rabbitConnectior = rabbitConnector;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllBookings()
    {
        return Ok(await _bookingRepository.GetAllBookings());
    }

    [HttpGet("ByMail/{email}")]
    public async Task<IActionResult> GetAllBookingsForUser([FromRoute] string email)
    {
        var result = await _bookingRepository.GetAllBookingsForUser(email);
        if(result is not null)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest();
        }
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
        await _rabbitConnectior.PublishMessageToExchangeForServices(result);
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
