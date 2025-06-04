namespace Core.Contracts.Requests;

// 获取账号信息和删除账号
public class ByAccountRequest
{
    public required string CompanyId { get; set; }
    public required string LoginName { get; set; }
}

// 获取账号列表
public class ByAccountListRequest
{
    public required string CompanyId { get; set; }
    public required string DisplayName { get; set; }
    public required int Page { get; set; } = 1;
    public required int PageSize { get; set; } = 20;
}

// 添加账号
public class AddAccountRequest
{
    public required string CompanyId { get; set; }
    public required string LoginName { get; set; }
    public required string DisplayName { get; set; }
    public required string EmpId { get; set; }
    public required string Password { get; set; }
    public required string IsActive { get; set; }
    public required sbyte AccountType { get; set; }
    public required string DeptId { get; set; }
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public required string Language { get; set; }
    public required string StaffId { get; set; }
}

public class UpdateAccountRequest
{
    public required string CompanyId { get; set; }
    public required string LoginName { get; set; }
    public required string DisplayName { get; set; }
    public required string IsActive { get; set; }
    public required sbyte AccountType { get; set; }
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public required string Language { get; set; }
    public required string StaffId { get; set; }
}

public class VerifyPasswordRequest
{
    public required string CompanyId { get; set; }
    public required string LoginName { get; set; }
    public string Id { get; set; } = "";
    public string OldPassword { get; set; } = "";
    public string NewPassword { get; set; } = "";
    public string SurPassword { get; set; } = "";
    public string StaffId { get; set; } = "";
}