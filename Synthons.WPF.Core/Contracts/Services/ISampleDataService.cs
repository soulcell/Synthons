using Synthons.WPF.Core.Models;

namespace Synthons.WPF.Core.Contracts.Services;

public interface ISampleDataService
{
    Task<IEnumerable<SampleOrder>> GetGridDataAsync();
}
