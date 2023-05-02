using Microsoft.UI.Xaml.Controls;

using Synthons.WinUI3.ViewModels;

namespace Synthons.WinUI3.Views;

public sealed partial class ProductsPage : Page
{
    public ProductsViewModel ViewModel
    {
        get;
    }

    public ProductsPage()
    {
        ViewModel = App.GetService<ProductsViewModel>();
        InitializeComponent();
    }
}
