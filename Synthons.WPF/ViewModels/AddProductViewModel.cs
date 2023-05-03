using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using Synthons.Domain;
using Synthons.Infrastructure;

namespace Synthons.WPF.ViewModels;
public partial class AddProductViewModel : ObservableObject, IModalDialogViewModel
{
    private readonly SynthonsDbContext context;
    private readonly string title;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private string name;

    [ObservableProperty]
    private string? manufacturer;

    [ObservableProperty]
    private string? description;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private decimal price;

    [ObservableProperty]
    private bool? dialogResult;

    public string Title => title;

    public AddProductViewModel(SynthonsDbContext context)
    {
        title = "Добавить товар";
        this.context = context;
    }

    public AddProductViewModel(SynthonsDbContext context, Product product)
    {
        title = $"Редактировать товар {product.Name}";
        this.context = context;

        Name = product.Name;
        Manufacturer = product.Manufacturer;
        Description = product.Description;
        if (product.CurrentPrice.HasValue)
        {
            Price = product.CurrentPrice.Value;
        }
    }

    [RelayCommand(CanExecute = nameof(CanOk))]
    private void Ok() => DialogResult = true;

    private bool CanOk() => (!string.IsNullOrWhiteSpace(Name) && Price >= 0);
}
