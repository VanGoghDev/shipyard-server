using System.Data;
using ErrorOr;
using Shipyard.Application.Common.Interfaces.Persistence;
using Shipyard.Application.Common.Interfaces.Persistence.Administation;
using Shipyard.Domain.Common.Errors;
using Shipyard.Domain.Entities;

namespace Shipyard.Application.Services.AdministrationServices;

public class RoleService : IRoleService
{
    private IRoleRepository _roleRepository;
    private IUserRepository _userRepository;

    public RoleService(IRoleRepository roleRepository, IUserRepository userRepository)
    {
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }
    
    public ErrorOr<Role> AddRole(string name)
    {
        // check if role already exists
        if (_roleRepository.GetRoleByName(name) is not null)
        {
            throw new DuplicateNameException("role already exists");
        }

        var role = new Role
        {
            Name = name
        };
        
        _roleRepository.AddRole(role);

        return role;
    }

    public ErrorOr<Role> AddRoleToUser(string roleName, string userEmail)
    {
        // check if role exists
        var role = _roleRepository.GetRoleByName(roleName);
        if (role is null)
        {
            throw new DuplicateNameException("Empty roles");
        }
        // check if user exists
        var user = _userRepository.GetUserByEmail(userEmail);
        if (user is not null)
        {
            return new[] { Errors.User.DuplicateEmail };
        }
        
        // check if user already has this role
        
        // add role to a user
        _userRepository.AddRoleToUser(user?.Email, role);
        
        return new Role();
    }
}