using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using Synthons.Domain;

namespace Synthons.WPF.ViewModels;
public partial class AddProductToSaleViewModel : ObservableObject, IModalDialogViewModel
{
    private readonly List<Product> products;
    [ObservableProperty]
    private bool? dialogResult;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalPrice))]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private Product? selectedProduct;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalPrice))]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private int? quantity;

    public int? TotalPrice => (int?)(SelectedProduct?.CurrentPrice * Quantity);

    public List<Product> Products => products;

    public AddProductToSaleViewModel(List<Product> products)
    {
        this.products = products;
    }

    [RelayCommand(CanExecute = nameof(CanOk))]
    private void Ok()
    {
        DialogResult = true;
    }

    private bool CanOk() => (SelectedProduct != null && Quantity > 0);

}
