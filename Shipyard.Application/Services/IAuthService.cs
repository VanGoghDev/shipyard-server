using ErrorOr;

namespace Shipyard.Application.Services;

public interface IAuthService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);

    ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
}