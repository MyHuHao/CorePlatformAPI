using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Infrastructure.Data;

public class MySqlConnectionFactory(IConfiguration configuration) : IMySqlConnectionFactory
{
    /// <summary>
    ///     获取指定名称的数据库连接
    /// </summary>
    /// <param name="name">连接字符串名称</param>
    public MySqlConnection CreateConnection(string name = "DefaultConnection")
    {
        var connString = configuration.GetConnectionString(name);
        if (string.IsNullOrEmpty(connString))
            throw new ArgumentException($"连接字符串未配置: {name}");
        // 使用 MySqlConnector 创建连接
        return new MySqlConnection(connString);
    }
}