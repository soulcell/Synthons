using System;
using System.Collections.Generic;

namespace Synthons.Domain;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Manufacturer { get; set; }

    public virtual ICollection<ProductPrice> ProductPrices { get; } = new List<ProductPrice>();

    public virtual ICollection<SaleProduct> SaleProducts { get; } = new List<SaleProduct>();

    public decimal? CurrentPrice => ProductPrices.OrderByDescending(x => x.AssignmentDate).FirstOrDefault()?.Price;
}
