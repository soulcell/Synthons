using System.Windows;
using MahApps.Metro.Controls;
using Synthons.WPF.ViewModels;

namespace Synthons.WPF.Views;
/// <summary>
/// Interaction logic for AddSaleDialog.xaml
/// </summary>
public partial class AddSaleDialog : MetroWindow
{
    public AddSaleDialog(AddSaleViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
