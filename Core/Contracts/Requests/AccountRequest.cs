namespace Core.Contracts.Requests;

// 获取账号信息和删除账号
public class ByAccountRequest
{
    public required string CompanyId { get; set; }
    public required string DisplayName { get; set; }
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
    public required string UserName { get; set; }
    public required string DisplayName { get; set; }
    public required string Password { get; set; }
    public sbyte IsActive { get; set; } = 1;
    public required sbyte AccountType { get; set; }
    public required string DeptId { get; set; }
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public sbyte FailedLoginAttempts { get; set; } = 0;
    public sbyte IsLocked { get; set; } = 0;
    public required string Language { get; set; }
    public required string StaffId { get; set; }
}