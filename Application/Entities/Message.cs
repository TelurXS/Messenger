using System.Text.Json.Serialization;

namespace Application.Entities;

public sealed class Message
{
    public int Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public DateTime SentAt { get; set; } = default;

    [JsonIgnore]
    public Group Group { get; set; } = default;

    [JsonIgnore]
    public Account Sender { get; set; } = default;
}
