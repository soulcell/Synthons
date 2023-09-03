using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using Synthons.Domain;
using Synthons.Infrastructure;

namespace Synthons.WPF.ViewModels;
public partial class AddCustomerViewModel : ObservableObject, IModalDialogViewModel
{
    private readonly SynthonsDbContext _context;

    private bool? dialogResult;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private string lastName;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    private string firstName;

    [ObservableProperty]
    private string? middleName;

    [ObservableProperty]
    private DateTime? birthDate;

    [ObservableProperty]
    private string? phoneNumber;

    [ObservableProperty]
    private string? emailAddress;

    public string Title { get; }

    public AddCustomerViewModel(SynthonsDbContext context)
    {
        _context = context;

        Title = "Добавить клиента";
    }
    public AddCustomerViewModel(SynthonsDbContext context, Customer customer)
    {
        _context = context;

        LastName = customer.LastName;
        FirstName = customer.FirstName;
        MiddleName = customer.MiddleName;
        BirthDate = customer.BirthDate;
        PhoneNumber = customer.PhoneNumber;
        EmailAddress = customer.EmailAddress;

        Title = $"Изменить клиента {customer.FullName}";
    }
    public bool? DialogResult
    {
        get => dialogResult;
        private set => SetProperty(ref dialogResult, value);
    }

    [RelayCommand(CanExecute = nameof(CanOk))]
    private void OnOk()
    {
        DialogResult = true;
    }

    private bool CanOk()
    {
        return (!string.IsNullOrWhiteSpace(LastName) && !string.IsNullOrWhiteSpace(FirstName));
    }

}
