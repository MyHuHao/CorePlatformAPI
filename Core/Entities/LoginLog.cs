namespace Core.Entities;

public class LoginLog
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public DateTime LoginTime { get; set; }
    public required string IpAddress { get; set; }
}