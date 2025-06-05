using Application.Queries;
using AutoMapper;
using Core.Contracts;
using Core.Contracts.Requests;
using Core.DTOs;
using Core.Enums;
using Core.Interfaces.Services;

namespace Application.Services;

public class CostCenterService(CostCenterQuery query, IMapper mapper) : ICostCenterService
{
    // 成本中心分页查询
    public async Task<ApiResult<PagedResult<CostCenterDto>>> GetCostCenterPageAsync(ByCostCenterListRequest request)
    {
        var result = await query.GetCostCenterPageAsync(request);
        var roleGroupDto = mapper.Map<List<CostCenterDto>>(result.items);
        PagedResult<CostCenterDto> pagedResult = new()
        {
            Records = roleGroupDto,
            Page = request.Page,
            PageSize = request.PageSize,
            Total = result.total
        };
        return new ApiResult<PagedResult<CostCenterDto>>
            { MsgCode = MsgCodeEnum.Success, Msg = "查询成功", Data = pagedResult };
    }
}