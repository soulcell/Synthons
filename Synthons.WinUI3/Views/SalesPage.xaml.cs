using Microsoft.UI.Xaml.Controls;

using Synthons.WinUI3.ViewModels;

namespace Synthons.WinUI3.Views;

public sealed partial class SalesPage : Page
{
    public SalesViewModel ViewModel
    {
        get;
    }

    public SalesPage()
    {
        ViewModel = App.GetService<SalesViewModel>();
        InitializeComponent();
    }
}
