using System.Windows.Controls;

using Synthons.WPF.ViewModels;

namespace Synthons.WPF.Views;

public partial class CustomersPage : Page
{
    public CustomersPage(CustomersViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
