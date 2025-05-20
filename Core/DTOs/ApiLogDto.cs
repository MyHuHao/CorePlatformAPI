namespace Core.DTOs;

public class ApiLogDto
{
    public string Id { get; set; } = "";
    public string IpAddress { get; set; } = "";
    public string UserName { get; set; } = "";
    public string Path { get; set; } = "";
    public string Method { get; set; } = "";
    public string RequestBody { get; set; } = "";
    public string ResponseBody { get; set; } = "";
    public short StatusCode { get; set; }
    public string ErrorMessage { get; set; } = "";
    public string RequestTime { get; set; } = "";
    public double Duration { get; set; }
}