using System.Text.Json.Serialization;

namespace Shipyard.Domain.Entities;

public class Role
{
    public Guid Id { get; set; } = new();
    public string Name { get; set; } = null!;

    [JsonIgnore]
    public List<User> Users { get; set; } = null!;
}