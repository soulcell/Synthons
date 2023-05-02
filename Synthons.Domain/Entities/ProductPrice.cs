using System;
using System.Collections.Generic;

namespace Synthons.Domain;

public partial class ProductPrice
{
    public int ProductPriceId { get; set; }

    public int? ProductId { get; set; }

    public decimal Price { get; set; }

    public DateTime AssignmentDate { get; set; }

    public virtual Product? Product { get; set; }
}
