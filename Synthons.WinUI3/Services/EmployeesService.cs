
using Microsoft.EntityFrameworkCore;
using Synthons.Domain;
using Synthons.Infrastructure;
using Synthons.WinUI3.Contracts.Services;

namespace Synthons.WinUI3.Services;
public class EmployeesService : IEmployeesService
{
    private readonly SynthonsDbContext _dbContext;

    public EmployeesService(SynthonsDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<ICollection<Employee>> GetEmployeesAsync()
    {
        return await _dbContext.Employees.ToListAsync();
    }
}
