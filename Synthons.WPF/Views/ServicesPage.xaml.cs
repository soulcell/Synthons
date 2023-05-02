using System.Windows.Controls;

using Synthons.WPF.ViewModels;

namespace Synthons.WPF.Views;

public partial class ServicesPage : Page
{
    public ServicesPage(ServicesViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
