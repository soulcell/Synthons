using System;
using System.Collections.Generic;

namespace Synthons.Domain;

public partial class SaleProduct
{
    public int SaleProductId { get; set; }

    public int? SaleId { get; set; }

    public int ProductId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Qty { get; set; }

    public decimal? TotalPrice { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Sale? Sale { get; set; }
}
