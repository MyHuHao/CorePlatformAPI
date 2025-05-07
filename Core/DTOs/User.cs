namespace Core.DTOs;

public class GetAllUserRequest
{
    public required string Id { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}