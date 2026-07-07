using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Interface;


namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register (RegisterRequestDto request)
    {
        var response = await _authService.RegisterAsync(request);

        return Ok(response); 
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login (LoginRequestDto request)
    {
        var response = await _authService.LoginAsync(request);

        return Ok(new{message = "Login Successfully"});
    }
}