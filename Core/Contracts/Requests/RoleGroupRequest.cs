namespace Core.Contracts.Requests;

public class AddRoleGroupRequest
{
    public required string CompanyId { get; set; }
    public required string RoleGroupName { get; set; }
    public string RoleGroupDesc { get; set; } = "";
    public required string Status { get; set; }
    public required string StaffId { get; set; }
}

public class ByRoleGroupListRequest
{
    public required string CompanyId { get; set; }
    public required string RoleGroupName { get; set; }
    public required string Status { get; set; }
    public required int Page { get; set; } = 1;
    public required int PageSize { get; set; } = 20;
}

public class ByRoleGroupRequest
{
    public string CompanyId { get; set; } = "";
    public string RoleGroupId { get; set; } = "";
}

// 验证是否重复
public class ValidRoleGroupRequest
{
    public string CompanyId { get; set; } = "";
    public string RoleGroupId { get; set; } = "";
    public string RoleGroupName { get; set; } = "";
    public string Status { get; set; } = "";
}

public class UpdateRoleGroupRequest
{
    public required string CompanyId { get; set; }
    public required string RoleGroupId { get; set; }
    public required string RoleGroupName { get; set; }
    public string RoleGroupDesc { get; set; } = "";
    public required string Status { get; set; }
    public required string StaffId { get; set; }
}

public class RoleGroupAuthorizeRequest
{
    public required string CompanyId { get; set; }
    public required string RoleGroupId { get; set; }
    public required List<string> ResIds { get; set; }
    public required List<string> MenuIds { get; set; }
    public required string StaffId { get; set; }
}