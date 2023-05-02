using Microsoft.UI.Xaml.Controls;

using Synthons.WinUI3.ViewModels;

namespace Synthons.WinUI3.Views;

public sealed partial class EmployeesPage : Page
{
    public EmployeesViewModel ViewModel
    {
        get;
    }

    public EmployeesPage()
    {
        ViewModel = App.GetService<EmployeesViewModel>();
        InitializeComponent();
    }
}
