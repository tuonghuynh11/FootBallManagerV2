using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Quoctich
{
    public int Id { get; set; }

    public string? Tenquocgia { get; set; }

    public virtual ICollection<Cauthu> Cauthus { get; set; } = new List<Cauthu>();

    public virtual ICollection<Diadiem> Diadiems { get; set; } = new List<Diadiem>();

    public virtual ICollection<Doibong> Doibongs { get; set; } = new List<Doibong>();

    public virtual ICollection<Huanluyenvien> Huanluyenviens { get; set; } = new List<Huanluyenvien>();

    public virtual ICollection<League> Leagues { get; set; } = new List<League>();
}
