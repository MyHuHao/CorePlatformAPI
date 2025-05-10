namespace Core.Contracts.Requests;

public class LoginRequest
{
    public required string Account { get; set; }
    public required string PassWord { get; set; }
    public required int LoginType { get; set; }
    public required string Language { get; set; } = "Zh-cn";
}

public class CreateAccountRequest
{
    public required string Account { get; set; }
    public required string PassWord { get; set; }
    public required string UserId { get; set; }
    public required string AccId { get; set; }
}