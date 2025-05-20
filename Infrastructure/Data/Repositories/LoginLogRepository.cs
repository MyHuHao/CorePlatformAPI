using Core.Contracts.Requests;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Repositories;

namespace Infrastructure.Data.Repositories;

public class LoginLogRepository(IDapperExtensions<LoginLog> dapper, IUnitOfWork unitOfWork) : ILoginLogRepository
{
    /// <summary>
    /// 新增登录日志
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<int> AddLoginLogAsync(AddLoginLogRequest request)
    {
        const string sql = """
                               INSERT INTO LoginLog 
                               (Id,
                               CompanyId,
                               UserId,
                               LoginTime,
                               IpAddress,
                               DeviceInfo,
                               CreatedBy,
                               CreatedTime,
                               ModifiedBy,
                               ModifiedTime)
                               VALUES
                               (@Id,
                               @CompanyId,
                               @UserId,
                               @LoginTime,
                               @IpAddress,
                               @DeviceInfo,
                               @CreatedBy,
                               @CreatedTime,
                               @ModifiedBy,
                               @ModifiedTime);
                           """;
        var currentTime = DateTime.Now;
        return await dapper.ExecuteAsync(sql, new
            {
                Id = HashHelper.GetUuid(),
                request.CompanyId,
                request.UserId,
                request.LoginTime,
                request.IpAddress,
                request.DeviceInfo,
                CreatedBy = request.StaffId,
                CreatedTime = currentTime,
                ModifiedBy = request.StaffId,
                ModifiedTime = currentTime
            },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }
}