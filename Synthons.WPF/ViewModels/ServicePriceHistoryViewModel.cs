using CommunityToolkit.Mvvm.ComponentModel;
using MvvmDialogs;
using Synthons.Domain;

namespace Synthons.WPF.ViewModels;

public partial class ServicePriceHistoryViewModel : ObservableObject, IModalDialogViewModel
{
    private readonly Service service;
    [ObservableProperty]
    private bool? dialogResult;

    public Service Service => service;

    public ServicePriceHistoryViewModel(Service service)
    {
        this.service = service;
    }

}
