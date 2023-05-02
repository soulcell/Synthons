using System;
using System.Collections.Generic;

namespace Synthons.Domain;

public partial class Sale
{
    public int SaleId { get; set; }

    public int? CustomerId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime? PaymentDate { get; set; }

    public decimal? TotalDue { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<SaleProduct> SaleProducts { get; } = new List<SaleProduct>();

    public virtual ICollection<SaleService> SaleServices { get; } = new List<SaleService>();
}
