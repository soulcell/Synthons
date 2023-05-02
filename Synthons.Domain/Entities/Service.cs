using System;
using System.Collections.Generic;

namespace Synthons.Domain;

public partial class Service
{
    public int ServiceId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<SaleService> SaleServices { get; } = new List<SaleService>();

    public virtual ICollection<ServicePrice> ServicePrices { get; } = new List<ServicePrice>();

    public decimal? CurrentPrice => ServicePrices.OrderByDescending(x => x.AssignmentDate).FirstOrDefault()?.Price;
}
