using Core.Contracts;
using Core.Contracts.Requests;
using Core.DTOs;

namespace Core.Interfaces.Services;

public interface IAccountService
{
    // 账号分页查询
    Task<ApiResult<PagedResult<AccountDto>>> GetAccountPageAsync(ByAccountListRequest request);

    // 新增
    Task<ApiResult<string>> AddAccountAsync(AddAccountRequest request);

    // 删除
    Task<ApiResult<string>> DeleteAccountAsync(string id);

    // 通过ID查询详细
    Task<ApiResult<AccountDto>> GetAccountByIdAsync(string id);
}