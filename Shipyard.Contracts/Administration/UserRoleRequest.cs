namespace Shipyard.Contracts.Administration;

public record UserRoleRequest(
    string Email,
    string RoleName
    );