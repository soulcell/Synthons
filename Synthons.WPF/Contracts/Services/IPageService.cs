using System.Windows.Controls;

namespace Synthons.WPF.Contracts.Services;

public interface IPageService
{
    Type GetPageType(string key);

    Page GetPage(string key);
}
