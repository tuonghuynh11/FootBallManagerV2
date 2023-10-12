using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Service
{
    public int IdService { get; set; }

    public string? ServiceName { get; set; }

    public string? Detail { get; set; }

    public string? Images { get; set; }

    public virtual ICollection<Field> IdFields { get; set; } = new List<Field>();

    public virtual ICollection<Supplier> IdSuppliers { get; set; } = new List<Supplier>();
}
