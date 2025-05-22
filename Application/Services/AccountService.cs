using Application.Commands;
using Application.Queries;
using AutoMapper;
using Core.Contracts;
using Core.Contracts.Requests;
using Core.DTOs;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.Services;

namespace Application.Services;

public class AccountService(
    IMapper mapper,
    AccountQuery query,
    AccountCommand command) : IAccountService
{
    // 分页查询账号
    public async Task<ApiResult<PagedResult<AccountDto>>> GetAccountPageAsync(ByAccountListRequest request)
    {
        var result = await query.GetAccountPageAsync(request);
        var accountDto = mapper.Map<List<AccountDto>>(result.items);
        PagedResult<AccountDto> pagedResult = new()
        {
            Records = accountDto,
            Page = request.Page,
            PageSize = request.PageSize,
            Total = result.total
        };
        return new ApiResult<PagedResult<AccountDto>>
            { MsgCode = MsgCodeEnum.Success, Msg = "查询成功", Data = pagedResult };
    }

    // 新增账号
    public async Task<ApiResult<string>> AddAccountAsync(AddAccountRequest request)
    {
        // 验证账号是否已存在
        var verifyAccount = await query.IsExistAccountAsync(request.CompanyId, request.LoginName);
        if (verifyAccount) throw new ValidationException(MsgCodeEnum.Warning, "账号已存在，请重新输入");

        // 新增账号
        await command.AddAccountAsync(request);
        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "创建成功" };
    }

    // 删除账号
    public async Task<ApiResult<string>> DeleteAccountAsync(string id)
    {
        await command.DeleteAccountAsync(id);
        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "删除成功" };
    }
    
    // 通过id查询详情
    public async Task<ApiResult<AccountDto>> GetAccountByIdAsync(string id)
    {
        var account = await query.GetAccountByIdAsync(id);
        return new ApiResult<AccountDto> { MsgCode = MsgCodeEnum.Success, Data = mapper.Map<AccountDto>(account) };
    }
}