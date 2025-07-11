﻿using Application.Commands;
using Application.Queries;
using AutoMapper;
using Core.Contracts;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.DTOs;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.Services;

namespace Application.Services;

public class EmployeeService(EmployeeQuery query, EmployeeCommand command, IMapper mapper) : IEmployeeService
{
    /// <summary>
    ///     通过用户ID获取详情
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ApiResult<EmployeeDto>> GetEmployeeById(ByEmployeeRequest request)
    {
        var employee = await query.GetEmployeeById(request);
        if (employee == null) throw new ValidationException(MsgCodeEnum.Warning, "用户不存在");

        var employeeDto = mapper.Map<EmployeeDto>(employee);
        return new ApiResult<EmployeeDto> { MsgCode = MsgCodeEnum.Success, Msg = "查询成功", Data = employeeDto };
    }

    /// <summary>
    ///     验证是否有该人员
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<bool> VerifyEmployeeAsync(ByEmployeeRequest request)
    {
        return await query.GetEmployeeById(request) != null;
    }

    /// <summary>
    ///     分页查询
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ApiResult<PagedResult<EmployeeDto>>> GetEmployeePageAsync(ByEmployeeListRequest request)
    {
        var result = await query.GetEmployeePageAsync(request);
        var employeeDto = mapper.Map<List<EmployeeDto>>(result.items);
        PagedResult<EmployeeDto> pagedResult = new()
        {
            Records = employeeDto,
            Page = request.Page,
            PageSize = request.PageSize,
            Total = result.total
        };
        return new ApiResult<PagedResult<EmployeeDto>>
            { MsgCode = MsgCodeEnum.Success, Msg = "查询成功", Data = pagedResult };
    }

    // 人员选择分页查询
    public async Task<ApiResult<PagedResult<EmployeeChangeResult>>> GetEmployeePageBySelectAsync(
        ByEmployeeListRequest request)
    {
        var result = await query.GetEmployeePageBySelectAsync(request);
        PagedResult<EmployeeChangeResult> pagedResult = new()
        {
            Records = result.items,
            Page = request.Page,
            PageSize = request.PageSize,
            Total = result.total
        };
        return new ApiResult<PagedResult<EmployeeChangeResult>>
            { MsgCode = MsgCodeEnum.Success, Msg = "查询成功", Data = pagedResult };
    }

    // 新增人员
    public async Task<ApiResult<int>> AddEmployeeAsync(AddEmployeeRequest request)
    {
        await command.AddEmployeeAsync(request);
        return new ApiResult<int> { MsgCode = MsgCodeEnum.Success, Msg = "新增成功" };
    }

    public Task<ApiResult<int>> UpdateEmployeeAsync(UpdateEmployeeRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<int>> DeleteEmployeeByIdAsync(string id, string companyId)
    {
        throw new NotImplementedException();
    }
}