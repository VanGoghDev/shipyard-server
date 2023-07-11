namespace Shipyard.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = new();
    
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public List<Role> Roles { get; set; } = new();
}