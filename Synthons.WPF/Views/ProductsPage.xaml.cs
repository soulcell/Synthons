using System.Windows.Controls;

using Synthons.WPF.ViewModels;

namespace Synthons.WPF.Views;

public partial class ProductsPage : Page
{
    public ProductsPage(ProductsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
