using System;
using System.Collections.Generic;

namespace Synthons.Domain;

public partial class ViSalesJournal
{
    public int SaleId { get; set; }

    public string Customer { get; set; } = null!;

    public string Employee { get; set; } = null!;

    public DateTime? PaymentDate { get; set; }

    public decimal? TotalDue { get; set; }
}
