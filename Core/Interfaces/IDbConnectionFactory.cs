using System.Data.Common;

namespace Core.Interfaces;

public interface IDbConnectionFactory
{
    DbConnection CreateConnection(string name = "DefaultConnection");
}