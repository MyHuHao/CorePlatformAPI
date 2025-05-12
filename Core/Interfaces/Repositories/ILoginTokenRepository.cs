using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ILoginTokenRepository
{
    Task<LoginToken?> GetByIdAsync(string id);
}