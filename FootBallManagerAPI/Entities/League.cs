using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class League
{
    public int Id { get; set; }

    public DateTime? Ngaybatdau { get; set; }

    public DateTime? Ngayketthuc { get; set; }

    public string? Tengiaidau { get; set; }

    public int? Idquocgia { get; set; }

    public byte[]? Hinhanh { get; set; }

    public virtual Quoctich? IdquocgiaNavigation { get; set; }

    public virtual ICollection<Round> Rounds { get; set; } = new List<Round>();

    public virtual ICollection<Teamofleague> Teamofleagues { get; set; } = new List<Teamofleague>();

    public virtual ICollection<Thongtingiaidau> Thongtingiaidaus { get; set; } = new List<Thongtingiaidau>();
}
