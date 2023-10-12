using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Round
{
    public int Id { get; set; }

    public int? Idgiaidau { get; set; }

    public string? Tenvongdau { get; set; }

    public DateTime? Ngaybatdau { get; set; }

    public int? Soluongdoi { get; set; }

    public string? Iddisplay { get; set; }

    public virtual ICollection<Footballmatch> Footballmatches { get; set; } = new List<Footballmatch>();

    public virtual League? IdgiaidauNavigation { get; set; }
}
