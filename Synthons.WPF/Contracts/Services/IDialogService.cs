using System.Windows;

namespace Synthons.WPF.Contracts.Services;
public interface IDialogService
{
    Type GetDialogType(string key);

    Window GetDialog(string key);

    public bool? ShowDialog(string key);
}

