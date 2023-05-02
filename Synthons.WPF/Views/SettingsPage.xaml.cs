using System.Windows.Controls;

using Synthons.WPF.ViewModels;

namespace Synthons.WPF.Views;

public partial class SettingsPage : Page
{
    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
