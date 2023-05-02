using Synthons.Domain;

namespace Synthons.WinUI3.Contracts.Services;
public interface IEmployeesService
{
    Task<ICollection<Employee>> GetEmployeesAsync();
}