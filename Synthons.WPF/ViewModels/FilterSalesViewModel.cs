using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using Synthons.Domain;

namespace Synthons.WPF.ViewModels
{
    public partial class FilterSalesViewModel : ObservableObject, IModalDialogViewModel
    {
        [ObservableProperty]
        private bool? dialogResult;

        [ObservableProperty]
        private bool filterByStartTime;
        [ObservableProperty]
        DateTime? startTime;

        [ObservableProperty]
        private bool filterByEndTime;
        [ObservableProperty]
        private DateTime? endTime;

        [ObservableProperty]
        private bool filterByCustomer;
        [ObservableProperty]
        private Customer? customer;

        [ObservableProperty]
        private bool filterByEmployee;
        [ObservableProperty]
        private Employee? employee;

        public List<Customer> Customers { get; }
        public List<Employee> Employees { get; }

        public FilterSalesViewModel(List<Customer> customers, List<Employee> employees)
        {
            Customers = customers;
            Employees = employees;

            StartTime = DateTime.Now.Date;
            EndTime = DateTime.Now.Date;
        }

        [RelayCommand]
        private void Ok()
        {
            DialogResult = true;
        }
    }
}
