﻿using Microsoft.UI.Xaml.Controls;

using Synthons.App.ViewModels;

namespace Synthons.App.Views;

// TODO: Change the grid as appropriate for your app. Adjust the column definitions on DataGridPage.xaml.
// For more details, see the documentation at https://docs.microsoft.com/windows/communitytoolkit/controls/datagrid.
public sealed partial class ProductListPage : Page
{
    public ProductListViewModel ViewModel
    {
        get;
    }

    public ProductListPage()
    {
        ViewModel = App.GetService<ProductListViewModel>();
        InitializeComponent();
    }
}
