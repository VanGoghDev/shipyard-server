using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shipyard.Application.Services.AdministrationServices;
using Shipyard.Contracts.Administration;

namespace Shipyard.Api.Controllers;

[Authorize(Roles = "admin")]
[Route("admin")]
public class AdministrationController: ApiController
{
    private readonly IRoleService _roleService;
    
    public AdministrationController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    
    [HttpPost("role")]
    public IActionResult AddRole(RoleRequest roleRequest)
    {
        var role = _roleService.AddRole(roleRequest.Name);
        return role.Match(
            validResult => Ok(validResult),
            errors => Problem(errors)
        );
    }

    [HttpPost("user/role")]
    public IActionResult AddRoleToUser(UserRoleRequest userRoleRequest)
    {
        var role = _roleService.AddRoleToUser(userRoleRequest.RoleName, userRoleRequest.Email);
        return role.Match(
            validResult => Ok(validResult),
            errors => Problem(errors)
        );
    }
    
    // Add remove role
}