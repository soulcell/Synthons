using System.Windows.Controls;

using Synthons.WPF.ViewModels;

namespace Synthons.WPF.Views;

public partial class OrdersPage : Page
{
    public OrdersPage(OrdersViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
