using Shipyard.Domain.Entities;

namespace Shipyard.Application.Services;

public record AuthenticationResult
(
    User User,
    string Token
);