using System.Windows.Controls;

using Synthons.WPF.ViewModels;

namespace Synthons.WPF.Views;

public partial class EmployeesPage : Page
{
    public EmployeesPage(EmployeesViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
