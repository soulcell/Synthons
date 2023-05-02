using CommunityToolkit.Mvvm.ComponentModel;
using Synthons.Domain;

namespace Synthons.WPF.ObservableEntities;
public class ObservableEmployee : ObservableObject
{
    private readonly Employee employee;

    public ObservableEmployee(Employee employee) => this.employee = employee;

    public Employee Source => employee;
    public string LastName
    {
        get => employee.LastName;
        set => SetProperty(employee.LastName, value, employee, (e, ln) => e.LastName = ln);
    }

    public string FirstName
    {
        get => employee.FirstName;
        set => SetProperty(employee.FirstName, value, employee, (e, fn) => e.FirstName = fn);
    }

    public string? MiddleName
    {
        get => employee.MiddleName;
        set => SetProperty(employee.MiddleName, value, employee, (e, fn) => e.MiddleName = fn);
    }

    public DateTime? BirthDate
    {
        get => employee.BirthDate;
        set => SetProperty(employee.BirthDate, value, employee, (e, fn) => e.BirthDate = fn);
    }

    public string FullName => employee.FullName;
}
