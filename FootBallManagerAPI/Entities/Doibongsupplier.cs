using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Doibongsupplier
{
    public string IdDoiBong { get; set; } = null!;

    public int IdSupplier { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? Duration { get; set; }

    public int? Status { get; set; }

    public virtual Doibong DoiBong { get; set; } = null!;

    public virtual Supplier IdSupplierNavigation { get; set; } = null!;
}
