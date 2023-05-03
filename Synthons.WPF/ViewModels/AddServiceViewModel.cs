using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using Synthons.Domain;
using Synthons.Infrastructure;

namespace Synthons.WPF.ViewModels;
public partial class AddServiceViewModel : ObservableObject, IModalDialogViewModel
{
    private readonly SynthonsDbContext context;
    private readonly string title;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private string name;

    [ObservableProperty]
    private string? description;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private decimal price;

    [ObservableProperty]
    private bool? dialogResult;

    public string Title => title;

    public AddServiceViewModel(SynthonsDbContext context)
    {
        title = "Добавить услугу";
        this.context = context;
    }

    public AddServiceViewModel(SynthonsDbContext context, Service service)
    {
        title = $"Редактировать услугу {service.Name}";
        this.context = context;

        Name = service.Name;
        Description = service.Description;
        if (service.CurrentPrice.HasValue)
        {
            Price = service.CurrentPrice.Value;
        }
    }

    [RelayCommand(CanExecute = nameof(CanOk))]
    private void Ok() => DialogResult = true;

    private bool CanOk() => (!string.IsNullOrWhiteSpace(Name) && Price >= 0);
}
