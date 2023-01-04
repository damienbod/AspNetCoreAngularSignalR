namespace Dtos;

public class MessageDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Amount { get; set; }
}
