using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Norn.Models.Models;
using Norn.Models.Models.Requests;
using Norn.Repository;

namespace Norn.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : Controller
{
    private IUserRepository _userRepository;
    private IBearerTokenGenerator _bearerTokenGenerator;
    public UserController(IBearerTokenGenerator bearerTokenGenerator, IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _bearerTokenGenerator = bearerTokenGenerator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);
        if (user == null) return Unauthorized();
        var validPassword =
        BCrypt.Net.BCrypt.Verify(
        request.Password,
        user.Password);
        if (!validPassword) return Unauthorized();
        var token = _bearerTokenGenerator.GenerateToken(user, user.Role);
        return Ok(new
        {
            token
        });
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetUserIdByEmail([FromRoute]string email)
    {
        var result = await _userRepository.GetIdByEmail(email);
        if (result is null) return BadRequest();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(LoginRequest request)
    {
        var result = await _userRepository.CreateUser(new Models.Models.User
        {
            Email = request.Email,
            Password = request.Password,
            Role = "User"
        });
        if (result) return Ok(result);

        return BadRequest();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _userRepository.GetAllUsers());
    }
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PromoteUser(PromoteUserRequest request)
    {
        return Ok(await _userRepository.UpdateRoleForUser(request));
    }
    [HttpDelete("{email}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser([FromRoute]string email)
    {
        return Ok(await _userRepository.DeleteUserByEmail(email));
    }
}
