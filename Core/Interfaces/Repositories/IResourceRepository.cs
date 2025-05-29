using Core.Contracts.Requests;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IResourceRepository
{
    /// <summary>
    /// 查询资源列表
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<(IEnumerable<Resource> items, int total)> GetResourceByPageAsync(ByResourceListRequest request);

    /// <summary>
    /// 新增资源
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<int> AddResourceAsync(AddResourceRequest request);
    
    /// <summary>
    /// 修改资源
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<int> UpdateResourceAsync(UpdateResourceRequest request);

    /// <summary>
    /// 通过ID查询资源
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Resource?> GetResourceByIdAsync(string id);

    /// <summary>
    /// 通过ID删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<int> DeleteResourceByIdAsync(string id);

    /// <summary>
    /// 验证资源Code是否重复
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<Resource?> ValidResourceCodeAsync(ValidResourceCodeRequest request);
}