using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Synthons.Domain;
using Synthons.Infrastructure;
using Synthons.WPF.Contracts.ViewModels;

namespace Synthons.WPF.ViewModels;
public class AddSaleViewModel : ObservableObject, IDialog
{
    private readonly SynthonsDbContext _context;
    private ICommand _addProductCommand;

    public Sale Sale { get; set; } = new Sale();
    public ObservableCollection<Customer> Customers { get; } = new ObservableCollection<Customer>();
    public ObservableCollection<Employee> Employees { get; } = new ObservableCollection<Employee>();


    public ObservableCollection<SaleProduct> SaleProducts { get; } = new ObservableCollection<SaleProduct>();


    public ICommand AddProductCommand => _addProductCommand ?? (_addProductCommand = new RelayCommand(OnAddProduct));

    public AddSaleViewModel(SynthonsDbContext context)
    {
        _context = context;
    }
    public async void OnLoaded()
    {
        Customers.Clear();
        Employees.Clear();

        var customers = await _context.Customers.ToListAsync();
        customers.ForEach(customer => Customers.Add(customer));

        var employees = await _context.Employees.ToListAsync();
        employees.ForEach(employee => Employees.Add(employee));
    }

    private async void OnAddProduct()
    {
        SaleProducts.Add(new SaleProduct() { Product = new Product() { Name = "Temp" }, Qty = 15, UnitPrice = 10, TotalPrice = 150 });
    }
}
