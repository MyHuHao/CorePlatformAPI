namespace Core.DTOs.Base;

public class ApiResponse<T>
{
    public required T Data { get; set; }
    public required string Msg { get; set; }
    public int MsgCode { get; set; }
    public string Time { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
}