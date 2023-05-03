using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using MvvmDialogs;
using Synthons.Domain;
using Synthons.Infrastructure;
using Synthons.WPF.Contracts.ViewModels;
using Synthons.WPF.Views;

namespace Synthons.WPF.ViewModels;

public partial class ProductsViewModel : ObservableObject, INavigationAware
{
    private readonly SynthonsDbContext context;
    private readonly IDialogService dialogService;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(EditProductCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteProductCommand))]
    [NotifyCanExecuteChangedFor(nameof(ShowPriceHistoryCommand))]
    private Product? selectedProduct;

    public ObservableCollection<Product> Source { get; } = new ObservableCollection<Product>();

    public ProductsViewModel(SynthonsDbContext context, IDialogService dialogService)
    {
        this.context = context;
        this.dialogService = dialogService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        await RefreshDataAsync();
    }

    public void OnNavigatedFrom()
    {
    }

    [RelayCommand]
    private async Task AddProductAsync()
    {
        var dialogViewModel = new AddProductViewModel(context);
        var success = dialogService.ShowDialog<AddProductDialog>(this, dialogViewModel);

        if (success != true) return;

        var product = new Product()
        {
            Name = dialogViewModel.Name,
            Manufacturer = dialogViewModel.Manufacturer,
            Description = dialogViewModel.Description
        };
        await context.Products.AddAsync(product);

        var productPrice = new ProductPrice()
        {
            Price = dialogViewModel.Price,
            AssignmentDate = DateTime.UtcNow
        };
        await context.ProductPrices.AddAsync(productPrice);

        product.ProductPrices.Add(productPrice);

        await context.SaveChangesAsync();

        await RefreshDataAsync();
    }

    [RelayCommand(CanExecute = nameof(CanEdit))]
    private async Task EditProductAsync()
    {
        var dialogViewModel = new AddProductViewModel(context, SelectedProduct);
        var success = dialogService.ShowDialog<AddProductDialog>(this, dialogViewModel);

        if (success != true) return;

        var product = await context.Products.FindAsync(SelectedProduct.ProductId);
        if (product == null) return;

        product.Name = dialogViewModel.Name;
        product.Manufacturer = dialogViewModel.Manufacturer;
        product.Description = dialogViewModel.Description;
        if (product.CurrentPrice != dialogViewModel.Price)
        {
            var productPrice = new ProductPrice()
            {
                Price = dialogViewModel.Price,
                AssignmentDate = DateTime.UtcNow
            };
            await context.ProductPrices.AddAsync(productPrice);

            product.ProductPrices.Add(productPrice);
        }

        await context.SaveChangesAsync();

        await RefreshDataAsync();
    }

    [RelayCommand(CanExecute = nameof(CanEdit))]
    private async Task DeleteProductAsync()
    {
        var product = await context.Products.FindAsync(SelectedProduct.ProductId);
        if (product == null) return;

        var saleProducts = product.SaleProducts.ToList();

        var message = (saleProducts.Count > 0) ?
            $"Вы уверены что хотите удалить товар \"{product.Name}\"? \nУдаление товара приведет к удалению всех продаж с данным товаром. Кол-во продаж с данным товаром: {saleProducts.Count}" :
            $"Вы уверены что хотите удалить товар \"{product.Name}\"?";

        var result = dialogService.ShowMessageBox(this, message, $"Удалить товар", System.Windows.MessageBoxButton.YesNo);

        if (result != System.Windows.MessageBoxResult.Yes) return;


        product.ProductPrices.Clear();
        context.Remove(product);

        await context.SaveChangesAsync();

        await RefreshDataAsync();
    }

    [RelayCommand(CanExecute = nameof(CanEdit))]
    private async Task ShowPriceHistoryAsync()
    {
        var product = await context.Products.FindAsync(SelectedProduct.ProductId);
        if (product == null) return;

        var dialogViewModel = new ProductPriceHistoryViewModel(product);

        dialogService.ShowDialog<ProductPriceHistoryDialog>(this, dialogViewModel);
    }

    private bool CanEdit() => SelectedProduct != null;

    private async Task RefreshDataAsync()
    {
        Source.Clear();

        // Replace this with your actual data
        var data = await context.Products
            .Include(x => x.ProductPrices)
            .ToListAsync();

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }
}
