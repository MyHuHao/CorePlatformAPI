using Application.Commands;
using Application.Queries;
using AutoMapper;
using Core.Contracts;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.DTOs;
using Core.Enums;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces.Services;

namespace Application.Services;

public class AccountService(
    IMapper mapper,
    AccountQuery query,
    LoginQuery loginQuery,
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
    public async Task<ApiResult<string>> DeleteAccountAsync(string id, string companyId)
    {
        if (id == "c7021a3a2254b49e4a4f64757fbd0890") throw new ValidationException(MsgCodeEnum.Warning, "超级管理员账号不能删除");

        var verifyRoleGroup = await query.VerifyWebMenuHasRoleGroupAsync(companyId, id);
        if (verifyRoleGroup) throw new ValidationException(MsgCodeEnum.Warning, "账号已绑定角色组，请先解除绑定");

        await command.DeleteAccountAsync(id);
        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "删除成功" };
    }

    // 通过id查询详情
    public async Task<ApiResult<AccountResult>> GetAccountByIdAsync(string id)
    {
        var account = await query.GetAccountByIdAsync(id);
        return new ApiResult<AccountResult> { MsgCode = MsgCodeEnum.Success, Data = account };
    }

    // 修改账号数据
    public async Task<ApiResult<string>> UpdateAccountAsync(UpdateAccountRequest request)
    {
        await command.UpdateAccountAsync(request);
        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "修改成功" };
    }

    /// <summary>
    /// 验证原密码是否正确
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<ApiResult<string>> VerifyPasswordAsync(VerifyPasswordRequest request)
    {
        // 验证账号，获取账号信息
        var accountResult = await loginQuery.GetAccountById(new ByAccountRequest
        {
            CompanyId = request.CompanyId,
            LoginName = request.LoginName
        });
        if (accountResult == null) throw new ValidationException(MsgCodeEnum.Warning, "该账户不存在");

        // 验证账户，密码是否正确
        var isValid = HashHelper.VerifyPassword(
            request.OldPassword,
            accountResult.PasswordHash,
            accountResult.PasswordSalt);
        if (isValid == false) throw new ValidationException(MsgCodeEnum.Warning, "密码错误");

        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "验证成功" };
    }

    public async Task<ApiResult<string>> UpdatePasswordAsync(VerifyPasswordRequest request)
    {
        await command.UpdatePasswordAsync(request);
        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "修改成功" };
    }

    public async Task<ApiResult<string>> GrantMenuRoleAsync(GrantMenuRoleRequest request)
    {
        var existingRoleGroupIds = await query.GetAccountRoleGroupIdsAsync(request.CompanyId, request.AccId);

        var requestRoleGroups = new HashSet<string>(request.RoleGroupIds);
        var existingRoleGroups = new HashSet<string>(existingRoleGroupIds);

        var toDelete = existingRoleGroups.Except(requestRoleGroups).ToList();

        var toAdd = requestRoleGroups.Except(existingRoleGroups).ToList();

        if (toAdd.Count != 0)
        {
            await command.GrantMenuRoleAsync(new GrantMenuRoleRequest
            {
                CompanyId = request.CompanyId,
                AccId = request.AccId,
                RoleGroupIds = toAdd,
                StaffId = request.StaffId
            });
        }

        if (toDelete.Count != 0)
        {
            await command.RevokeMenuRoleAsync(new GrantMenuRoleRequest
            {
                CompanyId = request.CompanyId,
                AccId = request.AccId,
                RoleGroupIds = toDelete,
                StaffId = request.StaffId
            });
        }

        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "授权成功" };
    }

    public async Task<ApiResult<List<string>>> GetGrantMenuRoleByIdAsync(string companyId, string id)
    {
        var result = await query.GetAccountRoleGroupIdsAsync(companyId, id);
        return new ApiResult<List<string>> { MsgCode = MsgCodeEnum.Success, Data = result };
    }
}