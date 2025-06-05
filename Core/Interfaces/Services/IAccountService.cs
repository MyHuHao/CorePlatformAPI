using Core.Contracts;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.DTOs;

namespace Core.Interfaces.Services;

public interface IAccountService
{
    // 账号分页查询
    Task<ApiResult<PagedResult<AccountDto>>> GetAccountPageAsync(ByAccountListRequest request);

    // 新增
    Task<ApiResult<string>> AddAccountAsync(AddAccountRequest request);

    // 删除
    Task<ApiResult<string>> DeleteAccountAsync(string id, string companyId);

    // 通过ID查询详细
    Task<ApiResult<AccountResult>> GetAccountByIdAsync(string id);

    // 修改账号数据
    Task<ApiResult<string>> UpdateAccountAsync(UpdateAccountRequest request);

    // 验证原密码是否正确
    Task<ApiResult<string>> VerifyPasswordAsync(VerifyPasswordRequest request);

    // 修改密码
    Task<ApiResult<string>> UpdatePasswordAsync(VerifyPasswordRequest request);

    // 给菜单授权角色组
    Task<ApiResult<string>> GrantMenuRoleAsync(GrantMenuRoleRequest request);

    // 通过ID查询已经授权的角色组
    Task<ApiResult<List<string>>> GetGrantMenuRoleByIdAsync(string companyId, string id);
}