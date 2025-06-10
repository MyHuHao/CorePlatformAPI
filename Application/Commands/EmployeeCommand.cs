using Core.Contracts.Requests;
using Core.Interfaces.Repositories;

namespace Application.Commands;

public class EmployeeCommand(IEmployeeRepository repository)
{
    // 新增人员
    public async Task AddEmployeeAsync(AddEmployeeRequest request)
    {
        await repository.AddEmployeeAsync(request);
    }
}