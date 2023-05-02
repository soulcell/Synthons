using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Synthons.Domain;
using Synthons.Infrastructure;
using Synthons.WPF.Contracts.ViewModels;

namespace Synthons.WPF.ViewModels;

public class EditSaleViewModel : ObservableObject, INavigationAware
{
    private readonly SynthonsDbContext _context;

    public Sale Sale
    {
        get;
        private set;
    }

    public ObservableCollection<Employee> Employees 
    {
        get; private set; 
    }
    public ObservableCollection<Customer> Customers
    {
        get; private set;
    }
    public ObservableCollection<Product> Products
    {
        get; private set;
    }
    public ObservableCollection<Service> Services
    {
        get; private set;
    }

    public EditSaleViewModel(SynthonsDbContext context)
    {
        _context = context;
    }

    private ICommand _testCommand;

    public ICommand TestCommand => _testCommand ?? (_testCommand = new RelayCommand(OnTest));

    public void OnNavigatedFrom() { }
    public async void OnNavigatedTo(object parameter)
    {
        Sale = parameter as Sale;

        //Employees = new ObservableCollection<Employee>(await _context.Employees.ToListAsync());
        await _context.Employees.LoadAsync();
        Employees = _context.Employees.Local.ToObservableCollection();

        await _context.Customers.LoadAsync();
        Customers = _context.Customers.Local.ToObservableCollection();

        await _context.Products.Include(p => p.ProductPrices).LoadAsync();
        Products = _context.Products.Local.ToObservableCollection();

        await _context.Services.Include(s => s.ServicePrices).LoadAsync();
        Services = _context.Services.Local.ToObservableCollection();
    }

    private void OnTest()
    {
        var test = 0;
    }
}
