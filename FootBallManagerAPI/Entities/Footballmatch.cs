using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Footballmatch
{
    public int Id { get; set; }

    public string? Tentrandau { get; set; }

    public int? Vongbang { get; set; }

    public int? Idvong { get; set; }

    public int? Diadiem { get; set; }

    public DateTime? Thoigian { get; set; }

    public virtual Field? DiadiemNavigation { get; set; }

    public virtual Round? IdvongNavigation { get; set; }

    public virtual ICollection<Thamgium> Thamgia { get; set; } = new List<Thamgium>();

    public virtual ICollection<Thongtintrandau> Thongtintrandaus { get; set; } = new List<Thongtintrandau>();
}
