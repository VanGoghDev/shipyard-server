using Shipyard.Domain.Entities;

namespace Shipyard.Application.Common.Interfaces.Persistence.Administation;

public interface IRoleRepository
{
    public void AddRole(Role role);

    public Role? GetRoleByName(string roleName);
    
}