﻿using System.Windows.Controls;

using MahApps.Metro.Controls;

using Synthons.WPF.Contracts.Views;
using Synthons.WPF.ViewModels;

namespace Synthons.WPF.Views;

public partial class ShellWindow : MetroWindow, IShellWindow
{
    public ShellWindow(ShellViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    public Frame GetNavigationFrame()
        => shellFrame;

    public void ShowWindow()
        => Show();

    public void CloseWindow()
        => Close();
}
