namespace Core.DTOs;

public class ResourceDto
{
    public string Id { get; set; } = "";
    public string CompanyId { get; set; } = "";
    public string ResCode { get; set; } = "";
    public string ResName { get; set; } = "";
    public string ResDesc { get; set; } = "";
    public string ResType { get; set; } = "";
    public int ResSequence { get; set; }
    public string ResPath { get; set; } = "";
    public string WebMenuId { get; set; } = "";
    public string CreatedBy { get; set; } = "";
    public string CreatedTime { get; set; } = "";
    public string ModifiedBy { get; set; } = "";
    public string ModifiedTime { get; set; } = "";
}