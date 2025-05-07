namespace Core.DTOs;

public class LoginRequest
{
    public required string Account { get; set; }
    public required string PassWord { get; set; }
    public required int LoginType { get; set; }
}