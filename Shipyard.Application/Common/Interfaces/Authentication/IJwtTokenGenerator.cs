using Shipyard.Domain.Entities;

namespace Shipyard.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}