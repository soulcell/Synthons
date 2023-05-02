using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using MvvmDialogs;
using Synthons.Domain;
using Synthons.Infrastructure;
using Synthons.WPF.Contracts.ViewModels;
using Synthons.WPF.Views;

namespace Synthons.WPF.ViewModels;

public partial class EmployeesViewModel : ObservableObject, INavigationAware
{
    private readonly SynthonsDbContext _context;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(EditEmployeeCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteEmployeeCommand))]
    private Employee? selectedEmployee;

    public ObservableCollection<Employee> Source { get; } = new ObservableCollection<Employee>();

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


    [RelayCommand]
    private async Task AddEmployeeAsync()
    {
        var dialogViewModel = new AddEmployeeViewModel(_context);

        var success = _dialogService.ShowDialog<AddEmployeeDialog>(this, dialogViewModel);

        if (success != true) return;

        var employee = new Employee()
        {
            LastName = dialogViewModel.LastName,
            FirstName = dialogViewModel.FirstName,
            MiddleName = dialogViewModel.MiddleName,
            BirthDate = dialogViewModel.BirthDate
        };

        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();

        await RefreshData();
    }

    [RelayCommand(CanExecute = nameof(CanEdit))]
    private async Task EditEmployeeAsync()
    {
        var dialogViewModel = new AddEmployeeViewModel(_context, SelectedEmployee);

        var success = _dialogService.ShowDialog<AddEmployeeDialog>(this, dialogViewModel);
        if (success != true) return;

        var employee = await _context.Employees.FindAsync(SelectedEmployee.EmployeeId);
        if (employee == null) return;

        employee.LastName = dialogViewModel.LastName;
        employee.FirstName = dialogViewModel.FirstName;
        employee.MiddleName = dialogViewModel.MiddleName;
        employee.BirthDate = dialogViewModel.BirthDate;

        await _context.SaveChangesAsync();
        await RefreshData();
    }

    [RelayCommand(CanExecute = nameof(CanEdit))]
    private async Task DeleteEmployeeAsync()
    {
        var success = _dialogService.ShowMessageBox(
            this,
            $"Вы уверены что хотите удалить {SelectedEmployee.FullName}?",
            $"Удалить {SelectedEmployee.FullName}",
            System.Windows.MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (success != MessageBoxResult.Yes) return;

        var employee = await _context.Employees.FindAsync(SelectedEmployee.EmployeeId);
        if (employee == null) return;

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        await RefreshData();
    }

    private bool CanEdit() => SelectedEmployee != null;

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
