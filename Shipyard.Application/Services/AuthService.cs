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
        // 1. Check if user with given email exists.
        
        // 2. Check if given password equals user password.
        
        // 3. Save user in db.
        
        // 4. Return jwt token.
        
        var id = Guid.NewGuid();

        var token = _jwtTokenGenerator.GenerateToken(id, firstName, lastName);
        
        // Make AuthenticationResult return user object and token in order to have flexible control on modifying claims.
        return new AuthenticationResult(
            Guid.NewGuid(),
            firstName,
            lastName,
            email,
            token);
    }
    
    public AuthenticationResult Login(string email, string password)
    {
        // 1. Check if user with given email exists in db.
        
        // 2. Check if given password equals user password.
        
        // 3. Return jwt token.
        
        return new AuthenticationResult(
            Guid.NewGuid(),
            "firstName",
            "lastName",
            email,
            "token"
        );
    }


}