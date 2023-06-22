using Shipyard.Domain.Entities;

namespace Shipyard.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    public void AddUser(User user);

    public User? GetUserByEmail(string email);
}