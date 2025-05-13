using Core.Contracts.Requests;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces.Repositories;

namespace Infrastructure.Data.Repositories;

public class AccountRepository(IDapperExtensions<Account> dapper, IUnitOfWork unitOfWork) : IAccountRepository
{
    public async Task<Account?> GetByIdAsync(string id)
    {
        const string sql = """
                                select 
                                    id, 
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
                                    modify_time
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

    public async Task<int> InsertLoginToken(InsertLoginToken loginToken)
    {
        const string sql = """
                               insert into login_token
                               (id,
                               user_Id,
                               token,
                               refresh_token,
                               expire_time,
                               device_id,
                               is_active,
                               created_by,
                               created_time,
                               modify_by,
                               modify_time)
                               values
                               (@id,
                               @user_Id,
                               @token,
                               @refresh_token,
                               @expire_time,
                               @device_id,
                               @is_active,
                               @created_by,
                               @created_time,
                               @modify_by,
                               @modify_time)
                           """;
        return await dapper.ExecuteAsync(sql,
            new
            {
                id = HashHelper.GetUuid(),
                user_Id = loginToken.UserId,
                token = loginToken.Token,
                refresh_token = loginToken.RefreshToken,
                expire_time = loginToken.ExpireTime,
                device_id = loginToken.DeviceId,
                is_active = 1,
                created_by = loginToken.UserId,
                created_time = DateTime.Now,
                modify_by = loginToken.UserId,
                modify_time = DateTime.Now
            });
    }

    public async Task<int> InsertLogLog(InsertLoginToken loginToken)
    {
        const string sql = """
                           insert into login_log
                           (id,
                           user_id,
                           login_time,
                           ip_address,
                           device_info,
                           created_by,
                           created_time,
                           modify_by,
                           modify_time)
                           values
                           (@id,
                           @user_id,
                           @login_time,
                           @ip_address,
                           @device_info,
                           @created_by,
                           @created_time,
                           @modify_by,
                           @modify_time)
                           """;
        return await dapper.ExecuteAsync(sql,
            new
            {
                id = HashHelper.GetUuid(),
                user_id = loginToken.UserId,
                login_time = DateTime.Now,
                ip_address = loginToken.IpAddress,
                device_info = loginToken.DeviceInfo,
                created_by = loginToken.UserId,
                created_time = DateTime.Now,
                modify_by = loginToken.UserId,
                modify_time = DateTime.Now
            });
    }
}