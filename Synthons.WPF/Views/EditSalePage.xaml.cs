using System.Windows.Controls;

using Synthons.WPF.ViewModels;

namespace Synthons.WPF.Views;

public partial class EditSalePage : Page
{
    public EditSalePage(EditSaleViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
