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

    public byte[]? Images { get; set; }

    public virtual ICollection<Doibongsupplier> Doibongsuppliers { get; set; } = new List<Doibongsupplier>();

    public virtual ICollection<Leaguesupplier> Leaguesuppliers { get; set; } = new List<Leaguesupplier>();

    public virtual ICollection<Supplierservice> Supplierservices { get; set; } = new List<Supplierservice>();
}
