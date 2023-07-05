using Shipyard.Application.Common.Interfaces.Persistence;
using Shipyard.Domain.Entities;

namespace Shipyard.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _dbContext;

    public UserRepository(UserDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void AddUser(User user)
    {
        _dbContext.Users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return _dbContext.Users.SingleOrDefault(u => u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
    }
}