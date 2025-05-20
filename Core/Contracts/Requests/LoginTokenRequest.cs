namespace Core.Contracts.Requests;

public class ByLoginTokenRequest
{
    public required string CompanyId { get; set; }
    public required string UserId { get; set; }
    public required string Token { get; set; }
    public required byte IsActive { get; set; }
}

public class ByLoginTokenListRequest
{
    public required string CompanyId { get; set; }
    public required string UserId { get; set; }
    public required byte IsActive { get; set; }
    public required int Page { get; set; } = 1;
    public required int PageSize { get; set; } = 20;
}

public class AddLoginTokenRequest
{
    public required string Id { get; set; }
    public required string CompanyId { get; set; }
    public required string UserId { get; set; }
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
    public required DateTime ExpireTime { get; set; }
    public required string DeviceId { get; set; }
    public required byte IsActive { get; set; }
    public required string CreatedBy { get; set; }
    public required string ModifiedBy { get; set; }
}