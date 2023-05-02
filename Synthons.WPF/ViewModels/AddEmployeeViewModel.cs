using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using Synthons.Domain;
using Synthons.Infrastructure;

namespace Synthons.WPF.ViewModels;
public partial class AddEmployeeViewModel : ObservableObject, IModalDialogViewModel
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

    public AddEmployeeViewModel(SynthonsDbContext context)
    {
        _context = context;
    }
    public AddEmployeeViewModel(SynthonsDbContext context, Employee employee)
    {
        _context = context;

        LastName = employee.LastName;
        FirstName = employee.FirstName;
        MiddleName = employee.MiddleName;
        BirthDate = employee.BirthDate;
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
