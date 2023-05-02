using System;
using System.Collections.Generic;

namespace Synthons.Domain;

public partial class ViProductSoldTotal
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public int? TotalSold { get; set; }

    public decimal? TotalPrice { get; set; }
}
