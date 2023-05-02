using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Synthons.Domain;
using Synthons.Infrastructure;
using Synthons.WPF.Contracts.ViewModels;

namespace Synthons.WPF.ViewModels;
public class AddEmployeeViewModel : ObservableObject, IDialog
{
    private readonly SynthonsDbContext _context;
    private ICommand _addCommand;

    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }

    public DateTime? BirthDate { get; set; }

    public bool IsValid => LastName != null && FirstName != null;

    public ICommand AddCommand => _addCommand ?? (_addCommand = new AsyncRelayCommand(OnAdd));

    public AddEmployeeViewModel(SynthonsDbContext context)
    {
        _context = context;
    }
    public void OnLoaded()
    {
    
    }

    public async Task OnAdd()
    {
        Employee employee = new Employee
        {
            LastName = LastName,
            FirstName = FirstName,
            MiddleName = MiddleName,
            BirthDate = BirthDate
        };

        await _context.Employees.AddAsync(employee);

        await _context.SaveChangesAsync();
    }
}
