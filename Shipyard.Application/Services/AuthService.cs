using Shipyard.Application.Common.Interfaces.Authentication;

namespace Shipyard.Application.Services;

public class AuthService : IAuthService
{
    private IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        var id = Guid.NewGuid();

        var token = _jwtTokenGenerator.GenerateToken(id, firstName, lastName);
        return new AuthenticationResult(
            Guid.NewGuid(),
            firstName,
            lastName,
            email,
            token);
    }
    
    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(),
            "firstName",
            "lastName",
            email,
            "token"
        );
    }


}