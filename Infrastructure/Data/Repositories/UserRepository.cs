using Core.Contracts.Requests;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data.Repositories;

public class UserRepository(IDapperExtensions<User> dapper) : IUserRepository
{
    /// <summary>
    /// 通过ID获取用户详细
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<User> GetByIdAsync(string id)
    {
        const string sql = """
                           select 
                           id,
                           name,
                           phone,
                           email,
                           avatar,
                           gender,
                           birthday,
                           status,
                           create_by as createBy,
                           create_time as createTime,
                           modify_by as modifyBy,
                           modify_time as modifyTime
                           from 
                           `user`
                           where 
                           id = @Id 
                           """;
        return await dapper.QuerySingleOrDefaultAsync(sql,
            new { Id = id }) ?? throw new InvalidOperationException();
    }

    public async Task<IEnumerable<User>> GetAllAsync(GetAllUserRequest request)
    {
        const string sql = """
                           SET @page;
                           SET @page_size;
                           SET @offset = (@page - 1) * @page_size;

                           SELECT 
                               id,
                               name,
                               phone,
                               email,
                               avatar,
                               gender,
                               birthday,
                               status,
                               create_by,
                               create_time,
                               modify_by,
                               modify_time
                           FROM 
                               user
                           LIMIT @page_size OFFSET @offset;
                           """;
        return await dapper.QueryAsync(sql,
            new { page = request.Page, page_size = request.PageSize });
    }

    public async Task<string> AddAsync(User user)
    {
        const string sql = """
                               insert into `user`
                               (id,
                               name,
                               phone,
                               email,
                               avatar,
                               gender,
                               birthday,
                               status,
                               create_by,
                               create_time,
                               modify_by,
                               modify_time)
                               values
                               (@id,
                               @name,
                               @phone,
                               @email,
                               @avatar,
                               @gender,
                               @birthday,
                               @status,
                               @create_by,
                               @create_time,
                               @modify_by,
                               @modify_time);

                               SELECT @id;
                           """;
        return await dapper.ExecuteScalarStringAsync(sql,
            new
            {
                id = user.Id,
                name = user.Name,
                phone = user.Phone,
                email = user.Email,
                avatar = user.Avatar,
                gender = user.Gender,
                birthday = user.Birthday,
                status = user.Status,
                create_by = user.CreateBy,
                create_time = user.CreateTime,
                modify_by = user.ModifyBy,
                modify_time = user.ModifyTime
            }) ?? throw new InvalidOperationException();
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var rows = await dapper.ExecuteAsync("DELETE FROM `user` WHERE id = @Id", new { Id = id });
        return rows > 0;
    }
}