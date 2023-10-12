using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Cauthu
{
    public int Id { get; set; }

    public string? Iddoibong { get; set; }

    public int? Idquoctich { get; set; }

    public string? Hoten { get; set; }

    public int? Tuoi { get; set; }

    public int? Sogiai { get; set; }

    public int? Sobanthang { get; set; }

    public byte[]? Hinhanh { get; set; }

    public string? Chanthuan { get; set; }

    public string? Thetrang { get; set; }

    public string? Vitri { get; set; }

    public int? Soao { get; set; }

    public string? Chieucao { get; set; }

    public string? Cannang { get; set; }

    public long? Giatricauthu { get; set; }

    public virtual ICollection<Chuyennhuong> Chuyennhuongs { get; set; } = new List<Chuyennhuong>();

    public virtual ICollection<Doihinhchinh> Doihinhchinhs { get; set; } = new List<Doihinhchinh>();

    public virtual Doibong? IddoibongNavigation { get; set; }

    public virtual Quoctich? IdquoctichNavigation { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<Thamgium> Thamgia { get; set; } = new List<Thamgium>();
}
