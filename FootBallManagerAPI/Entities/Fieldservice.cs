using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Fieldservice
{
    public int IdField { get; set; }

    public int IdService { get; set; }

    public int? Status { get; set; }

    public virtual Field IdFieldNavigation { get; set; } = null!;

    public virtual Service IdServiceNavigation { get; set; } = null!;
}
