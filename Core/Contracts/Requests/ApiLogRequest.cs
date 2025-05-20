namespace Core.Contracts.Requests;

public class ByApiLogListRequest
{
    public required string IpAddress { get; set; }
    public required string UserName { get; set; }
    public required string Path { get; set; }
    public required string Method { get; set; }
    public required int Page { get; set; } = 1;
    public required int PageSize { get; set; } = 20;
}

public class AddApiLogRequest
{
    public required string IpAddress { get; set; } 
    public required string UserName { get; set; }
    public required string Path { get; set; }
    public required string Method { get; set; }
    public required string RequestBody { get; set; }
    public required string ResponseBody { get; set; } 
    public required short StatusCode { get; set; }
    public required string ErrorMessage { get; set; }
    public required DateTime RequestTime { get; set; }
    public required long Duration { get; set; }
}