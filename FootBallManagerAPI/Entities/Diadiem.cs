using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Diadiem
{
    public int Id { get; set; }

    public string? Tendiadiem { get; set; }

    public int? Idquocgia { get; set; }

    public virtual ICollection<Doibong> Doibongs { get; set; } = new List<Doibong>();

    public virtual ICollection<Field> Fields { get; set; } = new List<Field>();

    public virtual Quoctich? IdquocgiaNavigation { get; set; }
}
