﻿namespace Synthons.WinUI3.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
