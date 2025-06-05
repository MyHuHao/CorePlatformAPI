namespace Core.Contracts.Requests;

public class ByCostCenterListRequest
{
    public string CompanyId { get; set; } = "";
    public string CostCenterName { get; set; } = "";
    public string Status { get; set; } = "";
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}