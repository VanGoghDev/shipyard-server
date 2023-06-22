using Shipyard.Application.Common.Interfaces.Authentication;
using Shipyard.Application.Common.Interfaces.Persistence;
using Shipyard.Domain.Entities;

namespace Shipyard.Application.Services;

public class AuthService : IAuthService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private IUserRepository _userRepository;

    public AuthService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    
    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // 1. Check if user with given email exists.
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            throw new Exception("User with given login already exists.");
        }

        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        
        // 3. Save user in db.
        _userRepository.AddUser(user);
        
        // 4. Return jwt token.
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        // Make AuthenticationResult return user object and token in order to have flexible control on modifying claims.
        return new AuthenticationResult(
            user,
            token);
    }
    
    public AuthenticationResult Login(string email, string password)
    {
        // 1. Check if user with given email exists in db.
        var user = _userRepository.GetUserByEmail(email);

        if (user is null)
        {
            throw new Exception("User not found.");
        }
        
        // 2. Check if given password equals user password.
        if (!user.Password.Equals(password))
        {
            throw new Exception("Incorrect password.");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user,
            token
        );
    }


}