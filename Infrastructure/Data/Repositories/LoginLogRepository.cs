using Core.Entities;
using Dapper;

namespace Infrastructure.Data.Repositories;

public interface ILoginLogRepository
{
    Task InsertAsync(LoginLog log);
}

public class LoginLogRepository(IDbConnectionFactory conn) : ILoginLogRepository
{
    public async Task InsertAsync(LoginLog log)
    {
        using var conn1 = conn.CreateConnection();
        await conn1.ExecuteAsync(
            "INSERT INTO LoginLogs (Username, LoginTime, IpAddress) VALUES (@Username, @LoginTime, @IpAddress)",
            log);
    }
}