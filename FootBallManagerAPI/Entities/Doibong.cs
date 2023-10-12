using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Doibong
{
    public string Id { get; set; } = null!;

    public int? Idquoctich { get; set; }

    public int? Thanhpho { get; set; }

    public byte[]? Hinhanh { get; set; }

    public string? Ten { get; set; }

    public int? Soluongthanhvien { get; set; }

    public DateTime? Ngaythanhlap { get; set; }

    public string? Sannha { get; set; }

    public string? Sodochienthuat { get; set; }

    public long? Giatri { get; set; }

    public virtual ICollection<Cauthu> Cauthus { get; set; } = new List<Cauthu>();

    public virtual ICollection<Chuyennhuong> Chuyennhuongs { get; set; } = new List<Chuyennhuong>();

    public virtual ICollection<Diem> Diems { get; set; } = new List<Diem>();

    public virtual ICollection<Doihinhchinh> Doihinhchinhs { get; set; } = new List<Doihinhchinh>();

    public virtual ICollection<Huanluyenvien> Huanluyenviens { get; set; } = new List<Huanluyenvien>();

    public virtual Quoctich? IdquoctichNavigation { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<Tapluyen> Tapluyens { get; set; } = new List<Tapluyen>();

    public virtual ICollection<Teamofleague> Teamofleagues { get; set; } = new List<Teamofleague>();

    public virtual Diadiem? ThanhphoNavigation { get; set; }

    public virtual ICollection<Thongtingiaidau> Thongtingiaidaus { get; set; } = new List<Thongtingiaidau>();

    public virtual ICollection<Thongtintrandau> Thongtintrandaus { get; set; } = new List<Thongtintrandau>();
}
