namespace Core.Contracts.Requests;

public class AddLoginLogRequest
{
    public required string CompanyId { get; set; }
    public required string UserId { get; set; }
    public required DateTime LoginTime { get; set; }
    public required string IpAddress { get; set; }
    public required string DeviceInfo { get; set; }
    public required string StaffId { get; set; }
}