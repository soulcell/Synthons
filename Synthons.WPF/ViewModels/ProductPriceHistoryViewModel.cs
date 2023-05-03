using CommunityToolkit.Mvvm.ComponentModel;
using MvvmDialogs;
using Synthons.Domain;

namespace Synthons.WPF.ViewModels;

public partial class ProductPriceHistoryViewModel : ObservableObject, IModalDialogViewModel
{
    private readonly Product product;
    [ObservableProperty]
    private bool? dialogResult;

    public Product Product => product;

    public ProductPriceHistoryViewModel(Product product)
    {
        this.product = product;
    }

}
