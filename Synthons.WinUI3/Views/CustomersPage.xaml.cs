using Microsoft.UI.Xaml.Controls;

using Synthons.WinUI3.ViewModels;

namespace Synthons.WinUI3.Views;

public sealed partial class CustomersPage : Page
{
    public CustomersViewModel ViewModel
    {
        get;
    }

    public CustomersPage()
    {
        ViewModel = App.GetService<CustomersViewModel>();
        InitializeComponent();
    }
}
