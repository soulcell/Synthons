using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MvvmDialogs;
using Synthons.Domain;
using Synthons.Infrastructure;
using Synthons.WPF.Views;

namespace Synthons.WPF.ViewModels;
public partial class AddSaleViewModel : ObservableObject, IModalDialogViewModel
{
    private readonly SynthonsDbContext context;
    private readonly IDialogService dialogService;
    private readonly List<Customer> customers;
    private readonly List<Employee> employees;
    private readonly List<Product> products;
    private readonly List<Service> services;
    [ObservableProperty]
    private bool? dialogResult;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private Customer? selectedCustomer;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private Employee? selectedEmployee;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RemoveSaleProductCommand))]
    private SaleProduct? selectedSaleProduct;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RemoveSaleServiceCommand))]
    private SaleService? selectedSaleService;

    [ObservableProperty]
    private bool isPaid = false;

    public List<Customer> Customers => customers;
    public List<Employee> Employees => employees;

    public decimal? Total => SaleProducts.Select(x => x.TotalPrice).Sum() + SaleServices.Select(x => x.Price).Sum();

    public ObservableCollection<SaleProduct> SaleProducts { get; } = new ObservableCollection<SaleProduct>();
    public ObservableCollection<SaleService> SaleServices { get; } = new ObservableCollection<SaleService>();

    public AddSaleViewModel(
        SynthonsDbContext context,
        IDialogService dialogService,
        List<Customer> customers,
        List<Employee> employees)
    {
        this.context = context;
        this.dialogService = dialogService;
        this.customers = customers;
        this.employees = employees;

        SaleProducts.CollectionChanged += SaleProductsOrServicesChanged;
        SaleServices.CollectionChanged += SaleProductsOrServicesChanged;
    }

    [RelayCommand]
    private async Task AddProductAsync()
    {
        var products = await context.Products.Include(p => p.ProductPrices).ToListAsync();

        var dialogViewModel = new AddProductToSaleViewModel(products);

        var success = dialogService.ShowDialog<AddProductToSaleDialog>(this, dialogViewModel);

        if (success != true) return;

        var product = dialogViewModel.SelectedProduct;

        if (product == null) return;

        var saleProduct = new SaleProduct()
        {
            Product = product,
            UnitPrice = (decimal)product.CurrentPrice,
            Qty = (int)dialogViewModel.Quantity,
            TotalPrice = dialogViewModel.TotalPrice
        };

        SaleProducts.Add(saleProduct);
    }

    [RelayCommand(CanExecute = nameof(CanRemoveSaleProduct))]
    private void RemoveSaleProduct()
    {
        SaleProducts.Remove(SelectedSaleProduct);
    }

    [RelayCommand]
    private async Task AddServiceAsync()
    {
        var services = await context.Services.Include(p => p.ServicePrices).ToListAsync();

        var dialogViewModel = new AddServiceToSaleViewModel(services);

        var success = dialogService.ShowDialog<AddServiceToSaleDialog>(this, dialogViewModel);

        if (success != true) return;

        var service = dialogViewModel.SelectedService;

        if (service == null) return;

        var saleService = new SaleService()
        {
            Service = service,
            Price = (decimal)service.CurrentPrice,
        };

        SaleServices.Add(saleService);
    }

    [RelayCommand(CanExecute = nameof(CanRemoveSaleService))]
    private void RemoveSaleService()
    {
        SaleServices.Remove(SelectedSaleService);
    }

    [RelayCommand(CanExecute = nameof(CanOk))]
    private void Ok()
    {
        DialogResult = true;
    }


    private void SaleProductsOrServicesChanged(object sender, EventArgs e)
    {
        OnPropertyChanged(nameof(Total));
        OkCommand.NotifyCanExecuteChanged();
    }

    private bool CanRemoveSaleProduct() => SelectedSaleProduct != null;
    private bool CanRemoveSaleService() => SelectedSaleService != null;

    private bool CanOk() => (
        SelectedCustomer != null && 
        SelectedEmployee != null && 
        (!SaleProducts.IsNullOrEmpty() || !SaleServices.IsNullOrEmpty())
        );
}
