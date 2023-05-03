using System.Collections.ObjectModel;
using System.Net.WebSockets;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using MvvmDialogs;
using Synthons.Domain;
using Synthons.Infrastructure;
using Synthons.WPF.Contracts.Services;
using Synthons.WPF.Contracts.ViewModels;
using Synthons.WPF.Views;

namespace Synthons.WPF.ViewModels;

public partial class OrdersViewModel : ObservableObject, INavigationAware
{
    private readonly SynthonsDbContext _context;
    private readonly IDialogService _dialogService;

    public ObservableCollection<Sale> Source { get; } = new ObservableCollection<Sale>();


    public OrdersViewModel(SynthonsDbContext context, IDialogService dialogService)
    {
        _context = context;
        _dialogService = dialogService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        await RefreshDataAsync();
    }

    public void OnNavigatedFrom()
    {
    }

    [RelayCommand]
    private async Task AddSaleAsync()
    {
        var customers = await _context.Customers.ToListAsync();
        var employees = await _context.Employees.ToListAsync();

        var dialogViewModel = new AddSaleViewModel(_context, _dialogService, customers, employees);

        var success = _dialogService.ShowDialog<AddSaleDialog>(this, dialogViewModel);

        if (success != true) return;

        Sale sale = new Sale
        {
            Customer = dialogViewModel.SelectedCustomer,
            Employee = dialogViewModel.SelectedEmployee,
            OrderDate = DateTime.UtcNow,
            TotalDue = dialogViewModel.Total
        };
        await _context.Sales.AddAsync(sale);

        foreach (var saleProduct in dialogViewModel.SaleProducts)
        {
            await _context.SaleProducts.AddAsync(saleProduct);
            sale.SaleProducts.Add(saleProduct);
        }

        foreach (var saleService in dialogViewModel.SaleServices)
        {
            await _context.SaleServices.AddAsync(saleService);
            sale.SaleServices.Add(saleService);
        }

        await _context.SaveChangesAsync();

        if (dialogViewModel.IsPaid)
        {
            sale.PaymentDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        await RefreshDataAsync();

    }

    private async Task RefreshDataAsync()
    {
        Source.Clear();

        var data = await _context.Sales
            .Include(x => x.Employee)
            .Include(x => x.Customer)
            .Include(x => x.SaleProducts)
            .ThenInclude(x => x.Product)
            .Include(x => x.SaleServices)
            .ThenInclude(X => X.Service)
            .ToListAsync();

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }
}
