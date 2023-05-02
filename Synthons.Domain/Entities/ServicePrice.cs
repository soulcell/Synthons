using System;
using System.Collections.Generic;

namespace Synthons.Domain;

public partial class ServicePrice
{
    public int ServicePriceId { get; set; }

    public int? ServiceId { get; set; }

    public decimal Price { get; set; }

    public DateTime AssignmentDate { get; set; }

    public virtual Service? Service { get; set; }
}
