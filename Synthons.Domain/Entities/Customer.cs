using System;
using System.Collections.Generic;

namespace Synthons.Domain;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? PhoneNumber { get; set; }

    public string? EmailAddress { get; set; }

    public virtual ICollection<Sale> Sales { get; } = new List<Sale>();

    public string FullName => $"{LastName} {FirstName} {MiddleName}";
}
