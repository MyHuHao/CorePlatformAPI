using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class UserQuery(IUserRepository repository)
{
    public async Task<User> GetByIdAsync(string id)
    {
        return await repository.GetByIdAsync(id);
    }
}