using Microsoft.UI.Xaml.Controls;

using Synthons.WinUI3.ViewModels;

namespace Synthons.WinUI3.Views;

public sealed partial class ServicesPage : Page
{
    public ServicesViewModel ViewModel
    {
        get;
    }

    public ServicesPage()
    {
        ViewModel = App.GetService<ServicesViewModel>();
        InitializeComponent();
    }
}
