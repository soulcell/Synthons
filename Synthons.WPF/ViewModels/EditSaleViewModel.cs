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
public partial class EditSaleViewModel : ObservableObject, IModalDialogViewModel
{
    private readonly SynthonsDbContext context;
    private readonly IDialogService dialogService;
    private readonly List<Customer> customers;
    private readonly List<Employee> employees;
    private readonly List<Product> products;
    private readonly List<Service> services;
    [ObservableProperty]
    private bool? dialogResult;

    public Sale Sale { get; set; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RemoveSaleProductCommand))]
    private SaleProduct? selectedSaleProduct;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RemoveSaleServiceCommand))]
    private SaleService? selectedSaleService;

    [ObservableProperty]
    private bool isPaid = false;

    public decimal? Total => SaleProducts.Select(x => x.TotalPrice).Sum() + SaleServices.Select(x => x.Price).Sum();

    public ObservableCollection<SaleProduct> SaleProducts { get; }
    public ObservableCollection<SaleService> SaleServices { get; }

    public EditSaleViewModel(
        SynthonsDbContext context,
        IDialogService dialogService,
        Sale sale)
    {
        this.context = context;
        this.dialogService = dialogService;
        Sale = sale;

        SaleProducts = new ObservableCollection<SaleProduct>(sale.SaleProducts.ToList());
        SaleServices = new ObservableCollection<SaleService>(sale.SaleServices.ToList());

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

    private bool CanOk() => !SaleProducts.IsNullOrEmpty() || !SaleServices.IsNullOrEmpty();
}
