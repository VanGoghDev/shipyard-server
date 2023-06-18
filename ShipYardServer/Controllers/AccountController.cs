using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShipYardServer.Models;
using ShipYardServer.ViewModels;

namespace ShipYardServer.Controllers;

[Route("api/account/")]
[ApiController]
public class AccountController : ControllerBase
{
    // ReSharper disable once NotAccessedField.Local
    private AppDbContext _appDbContext;
    private readonly JWTBearerTokenSettings _jwtBearerTokenSettings;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(AppDbContext appDbContext, UserManager<IdentityUser> userManager, IOptions<JWTBearerTokenSettings> jwtTokenOptions)
    {
        _appDbContext = appDbContext;
        _jwtBearerTokenSettings = jwtTokenOptions.Value;
        _userManager = userManager;
    }
    
    [HttpPost]
    [Route("sign-up")]
    public async Task<ActionResult> SignUp(SignUpData signUpData)
    {
        try
        {
            //Todo add business validation here
            var user = new IdentityUser()
            {
                Email = signUpData.Email,
                UserName = signUpData.Username,
            };
            var identityResult = await _userManager.CreateAsync(user, signUpData.Password);
            if (identityResult.Succeeded)
            {
                var jwtToken = GenerateJwtToken(user);
                return Ok(new
                {
                    Token = jwtToken
                });
            }
            return BadRequest(new
            {
                Message = identityResult.Errors
            });
        }
        catch (Exception error)
        {
            return BadRequest(new
            {
                message = error.Message
            });
        }
    }
        
    [HttpPost]
    [Route("sign-in")]
    public async Task<ActionResult> SignIn(SignInData signInData)
    {

        try
        {
            //Todo add business validation here
            //! You may want to edit catch exceptions block to handle failed scenarios
            var user = await ValidateUserCredentials(signInData);
            var jwtToken = GenerateJwtToken(user);
            return Ok(new
            {
                Token = jwtToken
            });
        }
        catch (Exception error)
        {
            return BadRequest(new
            {
                message = error.Message
            });
        }
    }
    
    //Rest of the SignIn() and ValidateUserCredentials() methods
    [HttpGet]
    //We add this annotation to tell the framework this is service/route requires Authorization Claims i.e JWT
    [Authorize]
    [Route("my-profile")]
    public async Task<ActionResult<UserProfile>> GetMyProfile()
    {
        try
        {
            //Todo add your business validation here
            //! You may want to edit catch exceptions block to handle failed scenarios
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(userEmail);

            return Ok(new UserProfile()
            {
                Email = user.Email,
                Username = user.UserName,
                PhoneNumber = user.PhoneNumber
            });
        }
        catch (Exception error)
        {
            return BadRequest(new
            {
                message = error.Message
            });
        }
    }

    private async Task<IdentityUser> ValidateUserCredentials(SignInData signInData)
    {
        var user = await _userManager.FindByEmailAsync(signInData.Email);
        if (user == null) return null!;
        var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, signInData.Password);
        return (result == PasswordVerificationResult.Failed ? null : user)!;
    }

    private string GenerateJwtToken(IdentityUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtBearerTokenSettings.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            }),

            Expires = DateTime.UtcNow.AddMinutes(_jwtBearerTokenSettings.ExpiryTimeInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = _jwtBearerTokenSettings.Audience,
            Issuer = _jwtBearerTokenSettings.Issuer
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}