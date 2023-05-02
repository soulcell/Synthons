using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Synthons.Domain;
using Synthons.Infrastructure;
using Synthons.WPF.Contracts.Services;
using Synthons.WPF.Contracts.ViewModels;

namespace Synthons.WPF.ViewModels;

public class OrdersViewModel : ObservableObject, INavigationAware
{
    private readonly SynthonsDbContext _context;
    private readonly IWindowManagerService _windowManager;
    private readonly IDialogService _dialogService;
    private ICommand _addSaleCommand;

    public ObservableCollection<Sale> Source { get; } = new ObservableCollection<Sale>();

    public ICommand AddSaleCommand => _addSaleCommand ?? (_addSaleCommand = new RelayCommand(OnAddSale));

    public OrdersViewModel(SynthonsDbContext context, IWindowManagerService windowManager, IDialogService dialogService)
    {
        _context = context;
        _windowManager = windowManager;
        _dialogService = dialogService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();

        // Replace this with your actual data
        var data = await _context.Sales
            .Include(x => x.Employee)
            .Include(x => x.Customer)
            .Include(x => x.SaleProducts)
            .ThenInclude(x => x.Product)
            .Include(x => x.SaleServices)
            .ThenInclude(X => X.Service)
            .ToListAsync();

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }

    private void OnAddSale()
    {
        //var viewModel = new AddSaleViewModel(_context);
        //var window = new AddSaleWindow(viewModel);
        //window.ShowDialog();

        //_windowManager.OpenInDialog(typeof(EditSaleViewModel).FullName, new Sale());
        _dialogService.ShowDialog(typeof(AddSaleViewModel).FullName);

    }
}
