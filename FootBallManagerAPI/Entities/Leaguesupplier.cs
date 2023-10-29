using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Leaguesupplier
{
    public int IdLeague { get; set; }

    public int IdSupplier { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? Duration { get; set; }

    public int? Status { get; set; }

    public virtual League IdLeagueNavigation { get; set; } = null!;

    public virtual Supplier IdSupplierNavigation { get; set; } = null!;
}
