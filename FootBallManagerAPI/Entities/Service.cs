using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Service
{
    public int IdService { get; set; }

    public string? ServiceName { get; set; }

    public string? Detail { get; set; }

    public byte[]? Images { get; set; }

    public virtual ICollection<Fieldservice> Fieldservices { get; set; } = new List<Fieldservice>();

    public virtual ICollection<Supplierservice> Supplierservices { get; set; } = new List<Supplierservice>();
}
