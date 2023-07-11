using ErrorOr;
using Shipyard.Domain.Entities;

namespace Shipyard.Application.Services.AdministrationServices;

public interface IRoleService
{
    /// <summary>
    /// Adds role to database.
    /// </summary>
    /// <param name="name">Name of a new role.</param>
    /// <returns>Returns new Role, or error if failed.</returns>
    ErrorOr<Role> AddRole(string name);
    
    /// <summary>
    /// Adds role to existing user.
    /// </summary>
    /// <param name="roleName">Name of a role.</param>
    /// <param name="userEmail">User email.</param>
    /// <returns></returns>
    ErrorOr<Role> AddRoleToUser(string roleName, string userEmail);

}