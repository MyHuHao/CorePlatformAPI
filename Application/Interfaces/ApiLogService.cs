using Core.Entities;

namespace Application.Interfaces;

public interface IApiLogService
{
    /// <summary>
    ///     插入api日志
    /// </summary>
    /// <param name="apiLog"></param>
    /// <returns></returns>
    Task InsertApiLog(ApiLog apiLog);
}