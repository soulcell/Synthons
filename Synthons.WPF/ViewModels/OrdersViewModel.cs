using System.Collections.ObjectModel;
using System.Linq.Expressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using MvvmDialogs;
using Synthons.Domain;
using Synthons.Infrastructure;
using Synthons.WPF.Contracts.ViewModels;
using Synthons.WPF.Reporting.ViewModels;
using Synthons.WPF.Reporting.Views;
using Synthons.WPF.Views;

namespace Synthons.WPF.ViewModels;

public partial class OrdersViewModel : ObservableObject, INavigationAware
{
    private readonly SynthonsDbContext _context;
    private readonly IDialogService _dialogService;

    private Expression<Func<Sale, bool>> predicate;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(EditSaleCommand))]
    [NotifyCanExecuteChangedFor(nameof(RemoveSaleCommand))]
    private Sale? selectedSale;
    public ObservableCollection<Sale> Source { get; } = new ObservableCollection<Sale>();
    public OrdersViewModel(SynthonsDbContext context, IDialogService dialogService)
    {
        _context = context;
        _dialogService = dialogService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        predicate = PredicateBuilder.New<Sale>(true);
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

    [RelayCommand(CanExecute = nameof(CanEdit))]
    private async Task EditSaleAsync()
    {

        if (SelectedSale == null) return;

        var dialogViewModel = new EditSaleViewModel(_context, _dialogService, SelectedSale);

        var success = _dialogService.ShowDialog<EditSaleDialog>(this, dialogViewModel);

        if (success != true) return;

        var sale = dialogViewModel.Sale;

        var newSaleProducts = dialogViewModel.SaleProducts.Except(sale.SaleProducts).ToList();
        var removedSaleProducts = sale.SaleProducts.Except(dialogViewModel.SaleProducts).ToList();

        foreach (var saleProduct in newSaleProducts)
        {
            await _context.SaleProducts.AddAsync(saleProduct);
            sale.SaleProducts.Add(saleProduct);
        }

        foreach (var removedSaleProduct in removedSaleProducts)
        {
            sale.SaleProducts.Remove(removedSaleProduct);
            _context.SaleProducts.Remove(removedSaleProduct);
        }

        var newSaleServices = dialogViewModel.SaleServices.Except(sale.SaleServices).ToList();
        var removedSaleServices = sale.SaleServices.Except(dialogViewModel.SaleServices).ToList();

        foreach (var newSaleService in newSaleServices)
        {
            await _context.SaleServices.AddAsync(newSaleService);
            sale.SaleServices.Add(newSaleService);
        }

        foreach (var removedSaleService in removedSaleServices)
        {
            sale.SaleServices.Remove(removedSaleService);
            _context.SaleServices.Remove(removedSaleService);
        }

        sale.TotalDue = dialogViewModel.Total;

        await _context.SaveChangesAsync();


        if (dialogViewModel.IsPaid)
        {
            sale.PaymentDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        await RefreshDataAsync();
    }

    [RelayCommand(CanExecute = nameof(CanEdit))]
    private async Task RemoveSaleAsync()
    {
        if (SelectedSale == null) return;

        var result = _dialogService.ShowMessageBox(
            this,
            $"Вы уверены что хотите удалить заказ?",
            $"Удалить заказ",
            System.Windows.MessageBoxButton.YesNo);

        if (result != System.Windows.MessageBoxResult.Yes) return;

        _context.Sales.Remove(SelectedSale);

        await _context.SaveChangesAsync();
        await RefreshDataAsync();
    }

    [RelayCommand]
    private async Task FilterSalesAsync()
    {
        var customers = await _context.Customers.ToListAsync();
        var employees = await _context.Employees.ToListAsync();

        var dialogViewModel = new FilterSalesViewModel(customers, employees);

        var success = _dialogService.ShowDialog<FilterSalesDialog>(this, dialogViewModel);

        if (success != true) return;


        predicate = PredicateBuilder.New<Sale>(true);

        if (dialogViewModel.FilterByStartTime && dialogViewModel.StartTime != null)
            predicate = predicate.And(s => s.OrderDate >= dialogViewModel.StartTime);

        if (dialogViewModel.FilterByEndTime && dialogViewModel.EndTime != null)
            predicate = predicate.And(s => s.OrderDate <= dialogViewModel.EndTime);

        if (dialogViewModel.FilterByCustomer && dialogViewModel.Customer != null)
            predicate = predicate.And(s => s.Customer == dialogViewModel.Customer);

        if (dialogViewModel.FilterByEmployee && dialogViewModel.Employee != null)
            predicate = predicate.And(s => s.Employee == dialogViewModel.Employee);


        await RefreshDataAsync();
    }

    [RelayCommand]
    private void ShowReport()
    {
        var dialogViewModel = new ReportViewerViewModel(_context);

        _dialogService.ShowDialog<ReportViewerDialog>(this, dialogViewModel);
    }

    private bool CanEdit() => SelectedSale != null && !SelectedSale.PaymentDate.HasValue;

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
            .AsExpandable()
            .Where(predicate)
            .ToListAsync();

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }
}
