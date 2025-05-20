using Core.Contracts.Requests;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Dapper;

namespace Infrastructure.Data.Repositories;

public class EmployeeRepository(IDapperExtensions<Employee> dapper, IUnitOfWork unitOfWork) : IEmployeeRepository
{
    /// <summary>
    /// 通过人员id查询人员信息
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<Employee?> GetByEmployeeAsync(ByEmployeeRequest request)
    {
        const string sql = """
                           SELECT
                            Id,
                            CompanyId,
                            EmpId,
                            EmpName,
                            EmpMobilePhone,
                            EmpEmail,
                            EmpEntryDate,
                            EmpDepartureDate,
                            UserType,
                            DeptId,
                            CostCenterId,
                            JobCategoryId,
                            Status,
                            Direct,
                            DeliveredDate,
                            Birthday,
                            Sex,
                            WorkYear,
                            EducationName,
                            CreatedBy,
                            CreatedTime,
                            ModifiedBy,
                            ModifiedTime
                           FROM
                            Employee
                           WHERE
                            CompanyId = @CompanyId
                           AND
                            EmpId = @EmpId;
                           """;
        return await dapper.QueryFirstOrDefaultAsync(sql,
            new { request.CompanyId, request.EmpId },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    /// <summary>
    /// 获取人员列表
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<(IEnumerable<Employee> items, int total)> GetByEmployeeListAsync(ByEmployeeListRequest request)
    {
        var conditions = new List<string>();
        var parameters = new DynamicParameters();

        if (!string.IsNullOrEmpty(request.CompanyId))
        {
            conditions.Add("CompanyId = @CompanyId");
            parameters.Add("CompanyId", request.CompanyId);
        }

        if (!string.IsNullOrEmpty(request.EmpId))
        {
            conditions.Add("EmpId = @EmpId");
            parameters.Add("EmpId", request.EmpId);
        }

        var whereClause = conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty;

        var sql = $"""
                   SELECT
                       Id,
                       CompanyId,
                       EmpId,
                       EmpName,
                       EmpMobilePhone,
                       EmpEmail,
                       EmpEntryDate,
                       EmpDepartureDate,
                       UserType,
                       DeptId,
                       CostCenterId,
                       JobCategoryId,
                       Status,
                       Direct,
                       DeliveredDate,
                       Birthday,
                       Sex,
                       WorkYear,
                       EducationName,
                       CreatedBy,
                       CreatedTime,
                       ModifiedBy,
                       ModifiedTime
                   FROM
                       Employee
                   {whereClause}   
                   ORDER BY 
                       CreatedTime DESC;
                   """;
        return await dapper.QueryPageAsync(
            request.Page,
            request.PageSize,
            sql,
            parameters,
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    /// <summary>
    /// 新增人员
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<int> AddEmployeeAsync(AddEmployeeRequest request)
    {
        const string sql = """
                           INSERT INTO Employee
                               (Id,
                               CompanyId,
                               EmpId,
                               EmpName,
                               EmpMobilePhone,
                               EmpEmail,
                               EmpEntryDate,
                               EmpDepartureDate,
                               UserType,
                               DeptId,
                               CostCenterId,
                               JobCategoryId,
                               Status,
                               Direct,
                               DeliveredDate,
                               Birthday,
                               Sex,
                               WorkYear,
                               EducationName,
                               CreatedBy,
                               CreatedTime,
                               ModifiedBy,
                               ModifiedTime)
                           VALUES
                               (@Id,
                               @CompanyId,
                               @EmpId,
                               @EmpName,
                               @EmpMobilePhone,
                               @EmpEmail,
                               @EmpEntryDate,
                               @EmpDepartureDate,
                               @UserType,
                               @DeptId,
                               @CostCenterId,
                               @JobCategoryId,
                               @Status,
                               @Direct,
                               @DeliveredDate,
                               @Birthday,
                               @Sex,
                               @WorkYear,
                               @EducationName,
                               @CreatedBy,
                               @CreatedTime,
                               @ModifiedBy,
                               @ModifiedTime);
                           """;
        var currentTime = DateTime.Now;
        return await dapper.ExecuteAsync(sql,
            new
            {
                Id = HashHelper.GetUuid(),
                request.CompanyId,
                request.EmpId,
                request.EmpName,
                request.EmpMobilePhone,
                request.EmpEmail,
                request.EmpEntryDate,
                request.EmpDepartureDate,
                request.UserType,
                request.DeptId,
                request.CostCenterId,
                request.JobCategoryId,
                request.Status,
                request.Direct,
                request.DeliveredDate,
                request.Birthday,
                request.Sex,
                request.WorkYear,
                request.EducationName,
                CreatedBy = request.StaffId,
                CreatedTime = currentTime,
                ModifiedBy = request.StaffId,
                ModifiedTime = currentTime
            },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    /// <summary>
    /// 批量新增账号
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public async Task<int> BatchAddEmployeeAsync(List<AddEmployeeRequest> list)
    {
        const string sql = """
                           INSERT INTO Employee
                               (Id,
                               CompanyId,
                               EmpId,
                               EmpName,
                               EmpMobilePhone,
                               EmpEmail,
                               EmpEntryDate,
                               EmpDepartureDate,
                               UserType,
                               DeptId,
                               CostCenterId,
                               JobCategoryId,
                               Status,
                               Direct,
                               DeliveredDate,
                               Birthday,
                               Sex,
                               WorkYear,
                               EducationName,
                               CreatedBy,
                               CreatedTime,
                               ModifiedBy,
                               ModifiedTime)
                           VALUES
                               (@Id,
                               @CompanyId,
                               @EmpId,
                               @EmpName,
                               @EmpMobilePhone,
                               @EmpEmail,
                               @EmpEntryDate,
                               @EmpDepartureDate,
                               @UserType,
                               @DeptId,
                               @CostCenterId,
                               @JobCategoryId,
                               @Status,
                               @Direct,
                               @DeliveredDate,
                               @Birthday,
                               @Sex,
                               @WorkYear,
                               @EducationName,
                               @CreatedBy,
                               @CreatedTime,
                               @ModifiedBy,
                               @ModifiedTime);
                           """;
        var currentTime = DateTime.Now;
        var records = list.Select(item => new
        {
            Id = HashHelper.GetUuid(),
            item.CompanyId,
            item.EmpId,
            item.EmpName,
            item.EmpMobilePhone,
            item.EmpEmail,
            item.EmpEntryDate,
            item.EmpDepartureDate,
            item.UserType,
            item.DeptId,
            item.CostCenterId,
            item.JobCategoryId,
            item.Status,
            item.Direct,
            item.DeliveredDate,
            item.Birthday,
            item.Sex,
            item.WorkYear,
            item.EducationName,
            CreatedBy = item.StaffId,
            CreatedTime = currentTime,
            ModifiedBy = item.StaffId,
            ModifiedTime = currentTime
        });
        return await dapper.ExecuteAsync(sql, records,
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    /// <summary>
    /// 删除账号
    /// </summary>
    /// <param name="request"></param>
    public async Task DeleteEmployeeAsync(ByEmployeeRequest request)
    {
        const string sql = """
                            DELETE FROM Employee 
                            WHERE 
                               CompanyId = @CompanyId 
                            AND 
                                EmpId = @EmpId;
                           """;
        await dapper.ExecuteAsync(sql,
            new { request.CompanyId, request.EmpId },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }
}