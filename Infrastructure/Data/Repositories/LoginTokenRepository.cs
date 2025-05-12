using Core.Entities;
using Core.Interfaces.Repositories;

namespace Infrastructure.Data.Repositories;

public class LoginTokenRepository(IDapperExtensions<LoginToken> dapper) : ILoginTokenRepository
{
    public async Task<LoginToken?> GetByIdAsync(string id)
    {
        const string sql = """
                            select 
                            id,
                            user_Id,
                            token,
                            refresh_token,
                            expire_time,
                            device_id,
                            is_active,
                            created_by,
                            created_time,
                            modify_by,
                            modify_time
                            from login_token 
                            where token = @token 
                            and is_active = 1
                           """;
        return await dapper.QuerySingleOrDefaultAsync(sql, new { token = id });
    }
}