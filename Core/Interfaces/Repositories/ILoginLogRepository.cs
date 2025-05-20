using Core.Contracts.Requests;

namespace Core.Interfaces.Repositories;

public interface ILoginLogRepository
{
    /// <summary>
    /// 新增登录日志
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<int> AddLoginLogAsync(AddLoginLogRequest request);
}