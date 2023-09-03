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

    public string Title { get; }
    public AddEmployeeViewModel(SynthonsDbContext context)
    {
        _context = context;

        Title = "Добавить сотрудника";
    }
    public AddEmployeeViewModel(SynthonsDbContext context, Employee employee)
    {
        _context = context;

        LastName = employee.LastName;
        FirstName = employee.FirstName;
        MiddleName = employee.MiddleName;
        BirthDate = employee.BirthDate;

        Title = $"Изменить сотрудника {employee.FullName}";
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
