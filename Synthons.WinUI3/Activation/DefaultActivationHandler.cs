using Microsoft.UI.Xaml;

using Synthons.WinUI3.Contracts.Services;
using Synthons.WinUI3.ViewModels;

namespace Synthons.WinUI3.Activation;

public class DefaultActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    private readonly INavigationService _navigationService;

    public DefaultActivationHandler(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
    {
        // None of the ActivationHandlers has handled the activation.
        return _navigationService.Frame?.Content == null;
    }

    protected async override Task HandleInternalAsync(LaunchActivatedEventArgs args)
    {
        _navigationService.NavigateTo(typeof(SalesViewModel).FullName!, args.Arguments);

        await Task.CompletedTask;
    }
}
