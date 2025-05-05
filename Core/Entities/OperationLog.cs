namespace Core.Entities;

public class OperationLog
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Action { get; set; }
    public required string Controller { get; set; }
    public required string IpAddress { get; set; }
    public DateTime Timestamp { get; set; }
}