using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Dapper;

namespace Infrastructure.Data.Repositories;

public class CostCenterRepository(IDapperExtensions<CostCenter> dapper, IUnitOfWork unitOfWork) : ICostCenterRepository
{
    public async Task<(IEnumerable<CostCenter> items, int total)> GetCostCenterPageAsync(
        ByCostCenterListRequest request)
    {
        var conditions = new List<string>();
        var parameters = new DynamicParameters();

        conditions.Add("CompanyId = @CompanyId");
        parameters.Add("CompanyId", request.CompanyId);

        if (!string.IsNullOrEmpty(request.CostCenterName))
        {
            conditions.Add("CostCenterName = @CostCenterName");
            parameters.Add("CostCenterName", request.CostCenterName);
        }

        if (!string.IsNullOrEmpty(request.Status))
        {
            conditions.Add("Status = @Status");
            parameters.Add("Status", request.Status);
        }

        var whereClause = conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty;
        
        var sql = $"""
                   SELECT
                       Id,
                       CompanyId,
                       CostCenterId,
                       CostCenterName,
                       Status,
                       CreatedBy,
                       CreatedTime,
                       ModifiedBy,
                       ModifiedTime
                   FROM
                   CostCenter
                    {whereClause}
                   ORDER BY
                   CreatedTime DESC
                   """;
        return await dapper.QueryPageAsync(
            request.Page,
            request.PageSize,
            sql,
            parameters,
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction
        );
    }
}