using Microsoft.EntityFrameworkCore;
using Shipyard.Domain.Entities;

namespace Shipyard.Infrastructure.Persistence;

public class UserDbContext: DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
}