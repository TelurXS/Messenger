using System.Text.Json.Serialization;

namespace Application.Entities;

public sealed class Group
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [JsonIgnore]
    public List<Message> Messages { get; set; } = new();

    [JsonIgnore]
    public List<Account> Accounts { get; set; } = new();
}
