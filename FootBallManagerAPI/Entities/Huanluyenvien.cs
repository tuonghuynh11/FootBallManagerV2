using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Huanluyenvien
{
    public int Id { get; set; }

    public string? Iddoibong { get; set; }

    public int? Idquoctich { get; set; }

    public string? Hoten { get; set; }

    public int? Tuoi { get; set; }

    public string? Gmail { get; set; }

    public DateTime? Ngaysinh { get; set; }

    public string? Chucvu { get; set; }

    public byte[]? Hinhanh { get; set; }

    public virtual Doibong? IddoibongNavigation { get; set; }

    public virtual Quoctich? IdquoctichNavigation { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Tapluyen> Tapluyens { get; set; } = new List<Tapluyen>();
}
