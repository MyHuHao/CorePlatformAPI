using Application.Queries;
using AutoMapper;
using Core.Contracts;
using Core.Contracts.Requests;
using Core.DTOs;
using Core.Enums;
using Core.Interfaces.Services;

namespace Application.Services;

public class DepartmentService(DepartmentQuery query, IMapper mapper) : IDepartmentService
{
    // 部门列表分页查询
    public async Task<ApiResult<PagedResult<DepartmentDto>>> GetDepartmentPageAsync(ByDepartmentListRequest request)
    {
        var result = await query.GetDepartmentPageAsync(request);
        var departmentDto = mapper.Map<List<DepartmentDto>>(result.items);
        PagedResult<DepartmentDto> pagedResult = new()
        {
            Records = departmentDto,
            Page = request.Page,
            PageSize = request.PageSize,
            Total = result.total
        };
        return new ApiResult<PagedResult<DepartmentDto>>
            { MsgCode = MsgCodeEnum.Success, Msg = "查询成功", Data = pagedResult };
    }
}