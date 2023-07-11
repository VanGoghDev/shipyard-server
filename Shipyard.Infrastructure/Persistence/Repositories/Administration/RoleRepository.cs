using Shipyard.Application.Common.Interfaces.Persistence.Administation;
using Shipyard.Domain.Entities;

namespace Shipyard.Infrastructure.Persistence.Repositories.Administration;

public class RoleRepository: IRoleRepository
{
    private readonly UserDbContext _dbContext;

    public RoleRepository(UserDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddRole(Role role)
    {
        _dbContext.Roles.Add(role);
        _dbContext.SaveChanges();
    }

    public Role? GetRoleByName(string roleName)
    {
        return _dbContext.Roles.SingleOrDefault(r => r.Name.Equals(roleName));
    }
}