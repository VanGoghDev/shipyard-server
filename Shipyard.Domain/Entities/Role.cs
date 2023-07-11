namespace Shipyard.Domain.Entities;

public class Role
{
    public Guid Id { get; set; } = new();
    public string Name { get; set; } = null!;

    public List<User> Users { get; set; } = null!;
}