using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using Synthons.Domain;

namespace Synthons.WPF.ViewModels;
public partial class AddServiceToSaleViewModel : ObservableObject, IModalDialogViewModel
{
    private readonly List<Service> services;
    [ObservableProperty]
    private bool? dialogResult;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private Service? selectedService;


    public List<Service> Services => services;

    public AddServiceToSaleViewModel(List<Service> services)
    {
        this.services = services;
    }

    [RelayCommand(CanExecute = nameof(CanOk))]
    private void Ok()
    {
        DialogResult = true;
    }

    private bool CanOk() => SelectedService != null;

}
