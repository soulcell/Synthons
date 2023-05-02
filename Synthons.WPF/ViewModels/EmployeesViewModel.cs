using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Synthons.Domain;
using Synthons.Infrastructure;
using Synthons.WPF.Contracts.Services;
using Synthons.WPF.Contracts.ViewModels;
using Synthons.WPF.Core.Contracts.Services;
using Synthons.WPF.Core.Models;

namespace Synthons.WPF.ViewModels;

public class EmployeesViewModel : ObservableObject, INavigationAware
{
    private readonly SynthonsDbContext _context;
    private readonly IDialogService _dialogService;
    private ICommand _addEmployeeCommand;
    public ObservableCollection<Employee> Source { get; } = new ObservableCollection<Employee>();

    public ICommand AddEmployeeCommand => _addEmployeeCommand ?? (_addEmployeeCommand = new RelayCommand(OnAddEmployee));

    public EmployeesViewModel(SynthonsDbContext context, IDialogService dialogService)
    {
        _context = context;
        _dialogService = dialogService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        await RefreshData();
    }

    public void OnNavigatedFrom()
    {
    }

    private async void OnAddEmployee()
    {
        _dialogService.ShowDialog(typeof(AddEmployeeViewModel).FullName);
        await RefreshData();
    }

    private async Task RefreshData()
    {
        Source.Clear();

        // Replace this with your actual data
        var data = await _context.Employees.ToListAsync();

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }
}
