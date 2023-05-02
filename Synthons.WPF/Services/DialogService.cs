using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using Synthons.WPF.Contracts.Services;
using Synthons.WPF.Contracts.ViewModels;
using Synthons.WPF.ViewModels;
using Synthons.WPF.Views;

namespace Synthons.WPF.Services;
public class DialogService : IDialogService
{
    private readonly Dictionary<string, Type> _dialogs = new Dictionary<string, Type>();
    private readonly IServiceProvider _serviceProvider;

    public DialogService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        Configure<AddSaleViewModel, AddSaleDialog>();
        Configure<AddEmployeeViewModel, AddEmployeeDialog>();
    }

    public Type GetDialogType(string key)
    {
        Type dialogType;
        lock (_dialogs)
        {
            if (!_dialogs.TryGetValue(key, out dialogType))
            {
                throw new ArgumentException($"Dialog not found: {key}. Did you forget to call DialogService.Configure?");
            }
        }

        return dialogType;
    }

    public Window GetDialog(string key)
    {
        var pageType = GetDialogType(key);
        return _serviceProvider.GetService(pageType) as Window;
    }

    public bool? ShowDialog(string key)
    {
        var dialog = GetDialog(key);

        dialog.Loaded += OnLoaded;
        dialog.Closed += OnWindowClosed;

        return dialog.ShowDialog();
    }

    private void Configure<VM, V>()
        where VM : ObservableObject
        where V : Window
    {
        lock (_dialogs)
        {
            var key = typeof(VM).FullName;
            if (_dialogs.ContainsKey(key))
            {
                throw new ArgumentException($"The key {key} is already configured in DialogService");
            }

            var type = typeof(V);
            if (_dialogs.Any(p => p.Value == type))
            {
                throw new ArgumentException($"This type is already configured with key {_dialogs.First(p => p.Value == type).Key}");
            }

            _dialogs.Add(key, type);
        }
    }

    private void OnLoaded(object sender, EventArgs e)
    {
        if (sender is Window window)
        {
            var dataContext = window.DataContext;
            if (dataContext is IDialog dialog)
            {
                dialog.OnLoaded();
            }
        }
    }

    private void OnWindowClosed(object sender, EventArgs e)
    {
        if (sender is Window window)
        {
            window.Loaded -= OnLoaded;

            window.Closed -= OnWindowClosed;
        }
    }
}
