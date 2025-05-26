using Core.Contracts.Requests;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Dapper;

namespace Infrastructure.Data.Repositories;

public class WebMenuRepository(IDapperExtensions<WebMenu> dapper, IUnitOfWork unitOfWork) : IWebMenuRepository
{
    // 菜单分页查询
    public async Task<(IEnumerable<WebMenu> items, int total)> GetWebMenuPageAsync(ByWebMenuListRequest request)
    {
        var conditions = new List<string>();
        var parameters = new DynamicParameters();

        if (!string.IsNullOrEmpty(request.Name))
        {
            conditions.Add("Name = @Name");
            parameters.Add("Name", request.Name);
        }

        var whereClause = conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty;

        var sql = $"""
                   SELECT
                   Id,
                   WebMenuId,
                   ParentWebMenuId,
                   Name,
                   Path,
                   Component,
                   Title,
                   Icon,
                   Redirect,
                   Sequence,
                   IsFrame,
                   FrameSrc,
                   IsCache,
                   IsVisible,
                   Permission,
                   MenuType,
                   Status,
                   Remark,
                   CreatedBy,
                   CreatedTime,
                   ModifiedBy,
                   ModifiedTime
                   FROM
                   WebMenu
                   {whereClause}
                   """;
        return await dapper.QueryPageAsync(
            request.Page,
            request.PageSize,
            sql,
            parameters,
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction
        );
    }

    // 新增菜单
    public async Task<int> AddWebMenuAsync(AddWebMenuRequest request)
    {
        const string sql = """
                           INSERT INTO WebMenu
                           (Id,
                           WebMenuId,
                           ParentWebMenuId,
                           Name,
                           Path,
                           Component,
                           Title,
                           Icon,
                           Redirect,
                           Sequence,
                           IsFrame,
                           FrameSrc,
                           IsCache,
                           IsVisible,
                           Permission,
                           MenuType,
                           Status,
                           Remark,
                           CreatedBy,
                           CreatedTime,
                           ModifiedBy,
                           ModifiedTime)
                           VALUES
                           (@Id,
                           @WebMenuId,
                           @ParentWebMenuId,
                           @Name,
                           @Path,
                           @Component,
                           @Title,
                           @Icon,
                           @Redirect,
                           @Sequence,
                           @IsFrame,
                           @FrameSrc,
                           @IsCache,
                           @IsVisible,
                           @Permission,
                           @MenuType,
                           @Status,
                           @Remark,
                           @CreatedBy,
                           @CreatedTime,
                           @ModifiedBy,
                           @ModifiedTime)
                           """;
        var currentTime = DateTime.Now;
        return await dapper.ExecuteAsync(sql,
            new
            {
                Id = HashHelper.GetUuid(),
                WebMenuId = HashHelper.GetUuid(),
                request.ParentWebMenuId,
                request.Name,
                request.Path,
                request.Component,
                request.Title,
                request.Icon,
                request.Redirect,
                request.Sequence,
                request.IsFrame,
                request.FrameSrc,
                request.IsCache,
                request.IsVisible,
                request.Permission,
                request.MenuType,
                request.Status,
                request.Remark,
                CreatedBy = request.StaffId,
                CreatedTime = currentTime,
                ModifiedBy = request.StaffId,
                ModifiedTime = currentTime
            },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    // 修改菜单
    public async Task<int> UpdateWebMenuAsync(UpdateWebMenuRequest request)
    {
        const string sql = """
                           UPDATE WebMenu SET
                               ParentWebMenuId = @ParentWebMenuId,
                               Name = @Name,
                               Path = @Path,
                               Component = @Component,
                               Title = @Title,
                               Icon = @Icon,
                               Redirect = @Redirect,
                               Sequence = @Sequence,
                               IsFrame = @IsFrame,
                               FrameSrc = @FrameSrc,
                               IsCache = @IsCache,
                               IsVisible = @IsVisible,
                               Permission = @Permission,
                               MenuType = @MenuType,
                               Status = @Status,
                               Remark = @Remark,
                               ModifiedBy = @ModifiedBy,
                               ModifiedTime = @ModifiedTime
                           WHERE
                               Id = @Id
                           """;
        return await dapper.ExecuteAsync(sql,
            new
            {
                request.Id,
                request.ParentWebMenuId,
                request.Name,
                request.Path,
                request.Component,
                request.Title,
                request.Icon,
                request.Redirect,
                request.Sequence,
                request.IsFrame,
                request.FrameSrc,
                request.IsCache,
                request.IsVisible,
                request.Permission,
                request.MenuType,
                request.Status,
                request.Remark,
                ModifiedBy = request.StaffId,
                ModifiedTime = DateTime.Now
            },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    // 删除菜单
    public async Task<int> DeleteWebMenuById(string id)
    {
        const string sql = """
                           DELETE FROM WebMenu
                           WHERE
                               Id = @Id
                           """;
        return await dapper.ExecuteAsync(sql,
            new { Id = id },
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }

    // 验证菜单
    public async Task<WebMenu?> VerifyWebMenuAsync(VerifyWebMenuRequest request)
    {
        var conditions = new List<string>();
        var parameters = new DynamicParameters();

        if (!string.IsNullOrEmpty(request.Sequence))
        {
            conditions.Add("Sequence = @Sequence");
            parameters.Add("Sequence", request.Sequence);
            
            conditions.Add("ParentWebMenuId = @ParentWebMenuId");
            parameters.Add("ParentWebMenuId", request.ParentWebMenuId);
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            conditions.Add("Name = @Name");
            parameters.Add("Name", request.Name);
        }
        
        var whereClause = conditions.Count > 0 ? $"WHERE {string.Join(" AND ", conditions)}" : string.Empty;

        var sql = $"""
                    SELECT * FROM WebMenu
                    {whereClause}
                   """;
        return await dapper.QueryFirstOrDefaultAsync(sql, parameters,
            unitOfWork.CurrentConnection,
            unitOfWork.CurrentTransaction);
    }
}