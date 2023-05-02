using System;
using System.Collections.Generic;

namespace Synthons.Domain;

public partial class ViServiceSoldTotal
{
    public int ServiceId { get; set; }

    public string Name { get; set; } = null!;

    public int? TotalSold { get; set; }

    public decimal? TotalPrice { get; set; }
}
