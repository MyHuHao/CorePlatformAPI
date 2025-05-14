namespace Core.Contracts.Requests;

public class ApiLogRequest
{
    public required string IpAddress { get; set; }
    public required string UserName { get; set; }
    public required string Path { get; set; }
    public required string Method { get; set; }
    public required int Page { get; set; } = 1;
    public required int PageSize { get; set; } = 20;
}