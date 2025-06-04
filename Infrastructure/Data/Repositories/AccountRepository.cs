using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Dapper;

namespace Infrastructure.Data.Repositories;

public class AccountRepository(
    IDapperExtensions<Account> dapper,
    IDapperExtensions<AccountResult> dapperResult,
    IUnitOfWork unitOfWork) : IAccountRepository
{
    /// <summary>
    ///     通过登录用户名查询账号信息
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<Account?> GetByAccountAsync(ByAccountRequest request)
    {
        const string sql = """
                            select 
                                Id,
                                CompanyId,
                                LoginName,
                                DisplayName,
                                EmpId,
                                PasswordHash,
                                PasswordSalt,
                                AccountType,
                                IsActive,
                                DeptId,
                                Email,
                                Phone,
                                Language,
                                LastLoginTime,
                                LastLoginIp,
                                FailedLoginAttempts,
                                IsLocked,
                                CreatedBy,
                                CreatedTime,
                                ModifiedBy,
                                ModifiedTime
                            FROM
                                Account
                            WHERE
                                CompanyId = @CompanyId
                            AND
                                LoginName = @LoginName
                           """;
        return await dapper.QueryFirstOrDefaultAsync(
            sql,
            new { request.CompanyId, request.LoginName },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    /// <summary>
    ///     通过ID查询账号详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<AccountResult?> GetAccountByIdAsync(string id)
    {
        const string sql = """
                            select 
                               a.LoginName,
                               a.DisplayName,
                               a.EmpId,
                               b.EmpName,
                               a.AccountType,
                               a.IsActive,
                               a.DeptId,
                               c.DeptName,
                               a.Email,
                               a.Phone,
                               a.`Language`
                               FROM
                               Account as a
                               LEFT JOIN employee as b on a.EmpId = b.EmpId and a.CompanyId = b.CompanyId
                               LEFT JOIN department as c on a.DeptId = c.DeptId and a.CompanyId = c.CompanyId
                               WHERE
                               a.Id = @Id
                           """;
        return await dapperResult.QueryFirstOrDefaultAsync(
            sql,
            new { Id = id },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    public async Task<int> UpdateAccountAsync(UpdateAccountRequest request)
    {
        const string sql = """
                           UPDATE Account SET 
                               DisplayName = @DisplayName,
                               IsActive = @IsActive,
                               AccountType = @AccountType,
                               Email = @Email,
                               Phone = @Phone,
                               Language = @Language,
                               ModifiedBy = @ModifiedBy,
                               ModifiedTime = @ModifiedTime
                           WHERE
                                LoginName = @LoginName
                           AND
                                CompanyId = @CompanyId
                           """;
        return await dapper.ExecuteAsync(
            sql,
            new
            {
                request.DisplayName,
                request.IsActive,
                request.AccountType,
                request.Email,
                request.Phone,
                request.Language,
                ModifiedBy = request.StaffId,
                ModifiedTime = DateTime.Now,
                request.LoginName,
                request.CompanyId
            },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    public async Task<int> UpdatePasswordAsync(VerifyPasswordRequest request)
    {
        var (passwordHash, passwordSalt) = HashHelper.GeneratePasswordHash(request.SurPassword);
        const string sql = """
                            UPDATE Account 
                            SET
                                PasswordHash = @PasswordHash,
                                PasswordSalt = @PasswordSalt,
                                ModifiedBy = @ModifiedBy,
                                ModifiedTime = @ModifiedTime
                            WHERE
                                LoginName = @LoginName
                            AND
                                CompanyId = @CompanyId
                           """;
        return await dapper.ExecuteAsync(sql,
            new
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                ModifiedBy = request.StaffId,
                ModifiedTime = DateTime.Now,
                request.LoginName,
                request.CompanyId
            },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    /// <summary>
    ///     获取账号列表
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<(IEnumerable<Account> items, int total)> GetByAccountListAsync(ByAccountListRequest request)
    {
        var conditions = new List<string>();
        var parameters = new DynamicParameters();

        if (!string.IsNullOrEmpty(request.CompanyId))
        {
            conditions.Add("CompanyId = @CompanyId");
            parameters.Add("CompanyId", request.CompanyId);
        }

        if (!string.IsNullOrEmpty(request.DisplayName))
        {
            conditions.Add("DisplayName = @DisplayName");
            parameters.Add("DisplayName", request.DisplayName);
        }

        // 构建动态WHERE子句
        var whereClause = conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty;

        var sql = $"""
                    select 
                        Id,
                        CompanyId,
                        LoginName,
                        DisplayName,
                        EmpId,
                        PasswordHash,
                        PasswordSalt,
                        AccountType,
                        IsActive,
                        DeptId,
                        Email,
                        Phone,
                        Language,
                        LastLoginTime,
                        LastLoginIp,
                        FailedLoginAttempts,
                        IsLocked,
                        CreatedBy,
                        CreatedTime,
                        ModifiedBy,
                        ModifiedTime
                    FROM
                        Account
                    {whereClause}
                    order by
                       CreatedTime desc
                   """;
        return await dapper.QueryPageAsync(request.Page, request.PageSize, sql, parameters);
    }

    /// <summary>
    ///     新增账号
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<int> AddAccountAsync(AddAccountRequest request)
    {
        const string sql = """
                            INSERT INTO Account
                               (Id,
                               CompanyId,
                               LoginName,
                               DisplayName,
                               EmpId,
                               PasswordHash,
                               PasswordSalt,
                               AccountType,
                               IsActive,
                               DeptId,
                               Email,
                               Phone,
                               Language,
                               LastLoginTime,
                               LastLoginIp,
                               FailedLoginAttempts,
                               IsLocked,
                               CreatedBy,
                               CreatedTime,
                               ModifiedBy,
                               ModifiedTime)
                           VALUES
                               (@Id,
                               @CompanyId,
                               @LoginName,
                               @DisplayName,
                               @EmpId,
                               @PasswordHash,
                               @PasswordSalt,
                               @AccountType,
                               @IsActive,
                               @DeptId,
                               @Email,
                               @Phone,
                               @Language,
                               @LastLoginTime,
                               @LastLoginIp,
                               @FailedLoginAttempts,
                               @IsLocked,
                               @CreatedBy,
                               @CreatedTime,
                               @ModifiedBy,
                               @ModifiedTime);
                           """;
        var currentTime = DateTime.Now;
        var (passwordHash, passwordSalt) = HashHelper.GeneratePasswordHash(request.Password);
        return await dapper.ExecuteAsync(sql,
            new
            {
                Id = HashHelper.GetUuid(),
                request.CompanyId,
                request.LoginName,
                request.DisplayName,
                request.EmpId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                request.AccountType,
                request.IsActive,
                request.DeptId,
                request.Email,
                request.Phone,
                request.Language,
                LastLoginTime = (DateTime?)null,
                LastLoginIp = "",
                FailedLoginAttempts = 0,
                IsLocked = 0,
                CreatedBy = request.StaffId,
                CreatedTime = currentTime,
                ModifiedBy = request.StaffId,
                ModifiedTime = currentTime
            },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    /// <summary>
    ///     批量新增账号
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public async Task<int> BatchAddAccountAsync(List<AddAccountRequest> list)
    {
        const string sql = """
                            INSERT INTO Account
                               (Id,
                               CompanyId,
                               LoginName,
                               DisplayName,
                               EmpId,
                               PasswordHash,
                               PasswordSalt,
                               AccountType,
                               IsActive,
                               DeptId,
                               Email,
                               Phone,
                               Language,
                               LastLoginTime,
                               LastLoginIp,
                               FailedLoginAttempts,
                               IsLocked,
                               CreatedBy,
                               CreatedTime,
                               ModifiedBy,
                               ModifiedTime)
                           VALUES
                               (@Id,
                               @CompanyId,
                               @LoginName,
                               @DisplayName,
                               @EmpId,
                               @PasswordHash,
                               @PasswordSalt,
                               @AccountType,
                               @IsActive,
                               @DeptId,
                               @Email,
                               @Phone,
                               @Language,
                               @LastLoginTime,
                               @LastLoginIp,
                               @FailedLoginAttempts,
                               @IsLocked,
                               @CreatedBy,
                               @CreatedTime,
                               @ModifiedBy,
                               @ModifiedTime);
                           """;
        var currentTime = DateTime.Now;
        var records = list.Select(item =>
        {
            var (passwordHash, passwordSalt) = HashHelper.GeneratePasswordHash(item.Password);
            return new
            {
                Id = HashHelper.GetUuid(),
                item.CompanyId,
                item.LoginName,
                item.DisplayName,
                item.EmpId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                item.AccountType,
                item.IsActive,
                item.DeptId,
                item.Email,
                item.Phone,
                item.Language,
                LastLoginTime = (DateTime?)null,
                LastLoginIp = "",
                FailedLoginAttempts = 0,
                IsLocked = 0,
                CreatedBy = item.StaffId,
                CreatedTime = currentTime,
                ModifiedBy = item.StaffId,
                ModifiedTime = currentTime
            };
        });
        return await dapper.ExecuteAsync(sql,
            records,
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    /// <summary>
    ///     删除账号
    /// </summary>
    /// <returns></returns>
    public async Task<int> DeleteAccountAsync(string id)
    {
        const string sql = """
                           DELETE FROM Account  
                           WHERE 
                               Id = @Id
                           """;
        return await dapper.ExecuteAsync(sql,
            new { Id = id },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    public async Task<Account?> IsExistAccountAsync(string companyId, string loginName)
    {
        const string sql = """
                               SELECT *
                               FROM Account
                               WHERE CompanyId = @CompanyId
                               AND LoginName = @LoginName;
                           """;
        return await dapper.QueryFirstOrDefaultAsync(sql,
            new { CompanyId = companyId, LoginName = loginName },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction
        );
    }
}