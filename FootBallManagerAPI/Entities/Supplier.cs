using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Supplier
{
    public int IdSupplier { get; set; }

    public string? SupplierName { get; set; }

    public string? Addresss { get; set; }

    public string? PhoneNumber { get; set; }

    public string? RepresentativeName { get; set; }

    public DateTime? EstablishDate { get; set; }

    public string? Images { get; set; }

    public virtual ICollection<Service> IdServices { get; set; } = new List<Service>();
}
