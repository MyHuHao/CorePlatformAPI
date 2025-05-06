using System.Data;

namespace Core.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection(string name = "DefaultConnection");
}