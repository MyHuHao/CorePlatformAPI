namespace Core.DTOs;

public class RoleGroupDto
{
    public string Id { get; set; } = "";
    public string CompanyId { get; set; } = "";
    public string RoleGroupId { get; set; } = "";
    public string RoleGroupName { get; set; } = "";
    public string RoleGroupDesc { get; set; } = "";
    public byte Status { get; set; }
    public string CreatedBy { get; set; } = "";
    public string CreatedTime { get; set; } = "";
    public string ModifiedBy { get; set; } = "";
    public string? ModifiedTime { get; set; } = "";
}