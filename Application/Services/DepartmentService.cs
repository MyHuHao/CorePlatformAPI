using Application.Queries;
using AutoMapper;
using Core.Contracts;
using Core.Contracts.Requests;
using Core.Contracts.Results;
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

    public async Task<ApiResult<PagedResult<TreeDepartmentResult>>> GetDepartmentTreeAsync(
        ByDepartmentListRequest request)
    {
        var result = await query.GetDepartmentPageAsync(request);
        var departmentDto = mapper.Map<List<DepartmentDto>>(result.items);
        PagedResult<TreeDepartmentResult> pagedResult = new()
        {
            Records = FormatTreeDepartmentResult(departmentDto),
            Page = request.Page,
            PageSize = request.PageSize,
            Total = result.total
        };
        return new ApiResult<PagedResult<TreeDepartmentResult>>
            { MsgCode = MsgCodeEnum.Success, Msg = "查询成功", Data = pagedResult };
    }

    private static List<TreeDepartmentResult> FormatTreeDepartmentResult(List<DepartmentDto> departmentDto)
    {
        var lookup = departmentDto.ToLookup(d => d.ParentDeptId);
        return BuildTree(string.Empty).ToList();

        IEnumerable<TreeDepartmentResult> BuildTree(string parentId)
        {
            foreach (var dto in lookup[parentId])
            {
                yield return new TreeDepartmentResult
                {
                    Id = dto.Id,
                    IsCancel = dto.IsCancel,
                    CompanyId = dto.CompanyId,
                    DeptId = dto.DeptId,
                    DeptName = dto.DeptName,
                    DeptLevel = dto.DeptLevel,
                    ParentDeptId = dto.ParentDeptId,
                    Children = BuildTree(dto.DeptId).ToList()
                };
            }
        }
    }
}