using Core.Entities;

namespace Core.Interfaces;

public interface IApiLogRepository
{
    /// <summary>
    ///     插入数据
    /// </summary>
    /// <param name="apiLog"></param>
    /// <returns></returns>
    Task<bool> AddAsync(ApiLog apiLog);
}