using Microsoft.AspNetCore.Mvc;
using Norn.Models.Models.Requests;
using Norn.Repository;

namespace Norn.Api.Controllers;

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
        if (!validPassword)  return Unauthorized();
        var token = _bearerTokenGenerator.GenerateToken(user,user.Role);
        return Ok(new
        {
            token
        });
    }
    [HttpPost]
    public async Task<IActionResult> CreateUser(LoginRequest request)
    {
       var result = await _userRepository.CreateUser(new Models.Models.User
        {
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = "User"
        });
        if (result) return Ok(result);

        return BadRequest();
    }
}
