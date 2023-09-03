using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using MvvmDialogs;
using Synthons.Domain;
using Synthons.Infrastructure;
using Synthons.WPF.Contracts.ViewModels;
using Synthons.WPF.Views;

namespace Synthons.WPF.ViewModels;

public partial class ServicesViewModel : ObservableObject, INavigationAware
{
    private readonly SynthonsDbContext context;
    private readonly IDialogService dialogService;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(EditServiceCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteServiceCommand))]
    [NotifyCanExecuteChangedFor(nameof(ShowPriceHistoryCommand))]
    private Service? selectedService;

    public ObservableCollection<Service> Source { get; } = new ObservableCollection<Service>();

    public ServicesViewModel(SynthonsDbContext context, IDialogService dialogService)
    {
        this.context = context;
        this.dialogService = dialogService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        await RefreshDataAsync();
    }

    public void OnNavigatedFrom()
    {
    }

    [RelayCommand]
    private async Task AddServiceAsync()
    {
        var dialogViewModel = new AddServiceViewModel(context);
        var success = dialogService.ShowDialog<AddServiceDialog>(this, dialogViewModel);

        if (success != true) return;

        var service = new Service()
        {
            Name = dialogViewModel.Name,
            Description = dialogViewModel.Description
        };
        await context.Services.AddAsync(service);

        var servicePrice = new ServicePrice()
        {
            Price = dialogViewModel.Price,
            AssignmentDate = DateTime.UtcNow
        };
        await context.ServicePrices.AddAsync(servicePrice);

        service.ServicePrices.Add(servicePrice);

        await context.SaveChangesAsync();

        await RefreshDataAsync();
    }

    [RelayCommand(CanExecute = nameof(CanEdit))]
    private async Task EditServiceAsync()
    {
        var dialogViewModel = new AddServiceViewModel(context, SelectedService);
        var success = dialogService.ShowDialog<AddServiceDialog>(this, dialogViewModel);

        if (success != true) return;

        var service = await context.Services.FindAsync(SelectedService.ServiceId);
        if (service == null) return;

        service.Name = dialogViewModel.Name;
        service.Description = dialogViewModel.Description;
        if (service.CurrentPrice != dialogViewModel.Price)
        {
            var servicePrice = new ServicePrice()
            {
                Price = dialogViewModel.Price,
                AssignmentDate = DateTime.UtcNow
            };
            await context.ServicePrices.AddAsync(servicePrice);

            service.ServicePrices.Add(servicePrice);
        }

        await context.SaveChangesAsync();

        await RefreshDataAsync();
    }

    [RelayCommand(CanExecute = nameof(CanEdit))]
    private async Task DeleteServiceAsync()
    {
        var service = await context.Services.FindAsync(SelectedService.ServiceId);
        if (service == null) return;

        var saleServices = service.SaleServices.ToList();

        var message = (saleServices.Count > 0) ? 
            $"Вы уверены что хотите удалить услугу \"{service.Name}\"? \nУдаление услуги приведет к удалению всех продаж с данной услугой. Кол-во продаж с данной услугой: {saleServices.Count}" : 
            $"Вы уверены что хотите удалить услугу \"{service.Name}\"?";

        var result = dialogService.ShowMessageBox(this, message, $"Удалить услугу", System.Windows.MessageBoxButton.YesNo);

        if (result != System.Windows.MessageBoxResult.Yes) return;


        service.ServicePrices.Clear();
        context.Remove(service);

        await context.SaveChangesAsync();

        await RefreshDataAsync();
    }

    [RelayCommand(CanExecute = nameof(CanEdit))]
    private async Task ShowPriceHistoryAsync()
    {
        var service = await context.Services.FindAsync(SelectedService.ServiceId);
        if (service == null) return;

        var dialogViewModel = new ServicePriceHistoryViewModel(service);

        dialogService.ShowDialog<ServicePriceHistoryDialog>(this, dialogViewModel);
    }

    private bool CanEdit() => SelectedService != null;

    private async Task RefreshDataAsync()
    {
        Source.Clear();

        var data = await context.Services
            .Include(x => x.ServicePrices)
            .ToListAsync();

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }
}
