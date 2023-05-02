using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Synthons.Domain;
using Synthons.WinUI3.Contracts.Services;
using Windows.ApplicationModel.VoiceCommands;

namespace Synthons.WinUI3.ViewModels;

public partial class EmployeesViewModel : ObservableRecipient
{
    private readonly IEmployeesService employeesService;

    public EmployeesViewModel(IEmployeesService employeesService)
    {
        this.employeesService = employeesService;
    }

    public ObservableCollection<Employee> Employees { get; } = new ObservableCollection<Employee>();

    [RelayCommand]
    private async Task LoadEmployeesAsync()
    {
        var empl = await employeesService.GetEmployeesAsync();
        Employees.Clear();
        foreach (var employee in empl)
        {
            Employees.Add(employee);
        }
    }
}
