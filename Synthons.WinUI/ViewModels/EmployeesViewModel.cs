using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Synthons.Domain;

namespace Synthons.WinUI.ViewModels;
internal class EmployeesViewModel : ObservableRecipient
{
    public IAsyncRelayCommand LoadEmployeesCommand { get; }

    public ObservableCollection<Employee> Employees { get; } = new ObservableCollection<Employee>();
}
