namespace Core.Dtos;

public class PagedResult<T>
{
    public required IEnumerable<T> Records { get; set; }
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}