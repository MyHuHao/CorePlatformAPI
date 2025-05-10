using MySql.Data.MySqlClient;

namespace Infrastructure.Interfaces;

public interface IMySqlConnectionFactory
{
    MySqlConnection CreateConnection(string name = "DefaultConnection");
}