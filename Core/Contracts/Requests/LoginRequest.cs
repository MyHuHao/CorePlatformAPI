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

public class InsertLoginToken
{
    public string IpAddress { get; set; } = "172.0.0.1";
    public string DeviceInfo { get; set; } = "PC";
    public required string UserId { get; set; }
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
    public required DateTime ExpireTime { get; set; }
    public required string DeviceId { get; set; }
}