using System.Data.Common;

namespace Core.Interfaces.Repositories;

public interface IDbConnectionFactory
{
    DbConnection CreateConnection(string name = "DefaultConnection");
}