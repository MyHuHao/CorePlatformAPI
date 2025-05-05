using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Infrastructure.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection(string databaseName);
}

public class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private readonly string _connectionString = configuration.GetConnectionString("MasterDB")
                                                ?? throw new InvalidOperationException();


    public IDbConnection CreateConnection(string databaseName)
    {
        MySqlConnectionStringBuilder builder = new(_connectionString)
        {
            Database = databaseName
        };
        MySqlConnection connection = new(builder.ConnectionString);
        connection.Open();
        return connection;
    }
}