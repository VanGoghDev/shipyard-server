using Microsoft.AspNetCore.Mvc;
using Shipyard.Application.Services;
using Shipyard.Contracts.Authentication;

namespace Shipyard.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private IAuthService _authService;

    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authService.Register(request.FirstName, request.LastName, request.Email, request.Password);
        return Ok(authResult);
    }
    
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authService.Login(request.Email, request.Password);
        return Ok(authResult);
    }
}