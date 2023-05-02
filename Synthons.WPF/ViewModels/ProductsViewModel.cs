using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Synthons.Domain;
using Synthons.Infrastructure;
using Synthons.WPF.Contracts.ViewModels;
using Synthons.WPF.Core.Contracts.Services;
using Synthons.WPF.Core.Models;

namespace Synthons.WPF.ViewModels;

public class ProductsViewModel : ObservableObject, INavigationAware
{
    private readonly SynthonsDbContext _context;

    public ObservableCollection<Product> Source { get; } = new ObservableCollection<Product>();

    public ProductsViewModel(SynthonsDbContext context)
    {
        _context = context;
    }

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();

        // Replace this with your actual data
        var data = await _context.Products
            .Include(x => x.ProductPrices)
            .ToListAsync();

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
