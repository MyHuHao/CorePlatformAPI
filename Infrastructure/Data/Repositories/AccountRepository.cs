using Core.Contracts.Requests;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces.Repositories;

namespace Infrastructure.Data.Repositories;

public class AccountRepository(IDapperExtensions<Account?> dapper, IUnitOfWork unitOfWork) : IAccountRepository
{
    public async Task<Account?> GetByIdAsync(string id)
    {
        const string sql = """
                                select 
                                    id, 
                                    account_name as AccountName,
                                    user_id as UserId, 
                                    password_hash as PasswordHash,
                                    password_salt as PasswordSalt, 
                                    role_id as RoleId,
                                    login_attempts as LoginAttempts,
                                    last_login_time as LastLoginTime,
                                    created_by as CreatedBy, 
                                    created_time as CreatedTime, 
                                    modify_by as ModifyBy, 
                                    modify_time as ModifyTime
                                from 
                                    account 
                                where 
                                    account_name = @account_name
                           """;
        return await dapper.QueryFirstOrDefaultAsync(
            sql, 
            new { account_name = id }, 
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    public async Task<int> AddAsync(CreateAccountRequest request)
    {
        const string sql = """
                            insert into account
                                (id, 
                                account_name, 
                                user_id, 
                                password_hash,
                                password_salt,
                                role_id, 
                                login_attempts,
                                last_login_time, 
                                created_by, 
                                created_time,
                                modify_by,
                                modify_time) 
                            values 
                                (@id, 
                                @account_name, 
                                @user_id, 
                                @password_hash,
                                @password_salt,
                                @role_id, 
                                @login_attempts,
                                @last_login_time, 
                                @created_by, 
                                @created_time,
                                @modify_by,
                                @modify_time)
                           """;
        var (passwordHash, passwordSalt) = HashHelper.GeneratePasswordHash(request.PassWord);
        return await dapper.ExecuteAsync(sql,
            new
            {
                id = HashHelper.GetUuid(),
                account_name = request.Account,
                user_id = request.UserId,
                password_hash = passwordHash,
                password_salt = passwordSalt,
                role_id = "",
                login_attempts = 0,
                last_login_time = DateTime.Now,
                created_by = request.AccId,
                created_time = DateTime.Now,
                modify_by = request.AccId,
                modify_time = DateTime.Now
            },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }
}