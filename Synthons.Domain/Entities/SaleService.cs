using System;
using System.Collections.Generic;

namespace Synthons.Domain;

public partial class SaleService
{
    public int SaleServiceId { get; set; }

    public int? SaleId { get; set; }

    public int ServiceId { get; set; }

    public decimal Price { get; set; }

    public virtual Sale? Sale { get; set; }

    public virtual Service Service { get; set; } = null!;
}
