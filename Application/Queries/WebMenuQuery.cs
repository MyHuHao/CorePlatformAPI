using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class WebMenuQuery(IWebMenuRepository repository)
{
    public async Task<(IEnumerable<WebMenu> items, int total)> GetWebMenuPageAsync(ByWebMenuListRequest request)
    {
        return await repository.GetWebMenuPageAsync(request);
    }
}