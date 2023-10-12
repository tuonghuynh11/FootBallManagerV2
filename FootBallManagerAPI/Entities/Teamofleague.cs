using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Teamofleague
{
    public int Id { get; set; }

    public int? Idgiaidau { get; set; }

    public string? Iddoibong { get; set; }

    public virtual Doibong? IddoibongNavigation { get; set; }

    public virtual League? IdgiaidauNavigation { get; set; }
}
