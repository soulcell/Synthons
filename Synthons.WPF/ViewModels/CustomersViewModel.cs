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

public partial class CustomersViewModel : ObservableObject, INavigationAware
{
    private readonly SynthonsDbContext _context;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(EditCustomerCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteCustomerCommand))]
    private Customer? selectedCustomer;

    public ObservableCollection<Customer> Source { get; } = new ObservableCollection<Customer>();

    public CustomersViewModel(SynthonsDbContext context, IDialogService dialogService)
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
    private async Task AddCustomerAsync()
    {
        var dialogViewModel = new AddCustomerViewModel(_context);

        var success = _dialogService.ShowDialog<AddCustomerDialog>(this, dialogViewModel);

        if (success != true) return;

        var customer = new Customer()
        {
            LastName = dialogViewModel.LastName,
            FirstName = dialogViewModel.FirstName,
            MiddleName = dialogViewModel.MiddleName,
            BirthDate = dialogViewModel.BirthDate,
            PhoneNumber = dialogViewModel.PhoneNumber,
            EmailAddress = dialogViewModel.EmailAddress
        };

        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();

        await RefreshData();
    }

    [RelayCommand(CanExecute = nameof(CanEdit))]
    private async Task EditCustomerAsync()
    {
        var dialogViewModel = new AddCustomerViewModel(_context, SelectedCustomer);

        var success = _dialogService.ShowDialog<AddCustomerDialog>(this, dialogViewModel);
        if (success != true) return;

        var customer = await _context.Customers.FindAsync(SelectedCustomer.CustomerId);
        if (customer == null) return;

        customer.LastName = dialogViewModel.LastName;
        customer.FirstName = dialogViewModel.FirstName;
        customer.MiddleName = dialogViewModel.MiddleName;
        customer.BirthDate = dialogViewModel.BirthDate;
        customer.PhoneNumber = dialogViewModel.PhoneNumber;
        customer.EmailAddress = dialogViewModel.EmailAddress;

        await _context.SaveChangesAsync();
        await RefreshData();
    }

    [RelayCommand(CanExecute = nameof(CanEdit))]
    private async Task DeleteCustomerAsync()
    {
        var success = _dialogService.ShowMessageBox(
            this,
            $"Вы уверены что хотите удалить {SelectedCustomer.FullName}?",
            $"Удалить {SelectedCustomer.FullName}",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (success != MessageBoxResult.Yes) return;

        var customer = await _context.Customers.FindAsync(SelectedCustomer.CustomerId);
        if (customer == null) return;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        await RefreshData();
    }

    private bool CanEdit() => SelectedCustomer != null;

    private async Task RefreshData()
    {
        Source.Clear();

        // Replace this with your actual data
        var data = await _context.Customers.ToListAsync();

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }
}
