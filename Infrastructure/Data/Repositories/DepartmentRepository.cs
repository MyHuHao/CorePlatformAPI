using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Dapper;

namespace Infrastructure.Data.Repositories;

public class DepartmentRepository(IDapperExtensions<Department> dapper, IUnitOfWork unitOfWork) : IDepartmentRepository
{
    public async Task<(IEnumerable<Department> items, int total)> GetDepartmentPageAsync(
        ByDepartmentListRequest request)
    {
        var conditions = new List<string>();
        var parameters = new DynamicParameters();

        conditions.Add("CompanyId = @CompanyId");
        parameters.Add("CompanyId", request.CompanyId);

        if (!string.IsNullOrEmpty(request.DeptName))
        {
            conditions.Add("DeptName = @DeptName");
            parameters.Add("DeptName", request.DeptName);
        }
        
        if (!string.IsNullOrEmpty(request.IsCancel))
        {
            conditions.Add("IsCancel = @IsCancel");
            parameters.Add("IsCancel", request.IsCancel);
        }

        var whereClause = conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty;

        var sql = $"""
                   SELECT 
                       Id,
                       CompanyId,
                       DeptId,
                       DeptName,
                       DeptLevel,
                       ParentDeptId,
                       CostCenterId,
                       CancelDate,
                       IsCancel,
                       CreatedBy,
                       CreatedTime,
                       ModifiedBy,
                       ModifiedTime
                   FROM 
                        Department
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