using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Supplierservice
{
    public int IdSupplier { get; set; }

    public int IdService { get; set; }

    public int? Status { get; set; }

    public virtual Service IdServiceNavigation { get; set; } = null!;

    public virtual Supplier IdSupplierNavigation { get; set; } = null!;
}
