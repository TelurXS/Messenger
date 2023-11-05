namespace Application.Entities;

public sealed class Message
{
    public int Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public DateTime SendedAt { get; set; } = default;

    public Group Group { get; set; } = default;

    public Account Sender { get; set; } = default;
}
