using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using Synthons.App.Contracts.ViewModels;
using Synthons.App.Core.Contracts.Services;
using Synthons.App.Core.Models;

namespace Synthons.App.ViewModels;

public class ServiceListViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;

    public ObservableCollection<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

    public ServiceListViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();

        // TODO: Replace with real data.
        var data = await _sampleDataService.GetGridDataAsync();

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
