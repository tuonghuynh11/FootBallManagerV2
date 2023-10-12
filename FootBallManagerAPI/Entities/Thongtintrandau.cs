using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Thongtintrandau
{
    public int Id { get; set; }

    public int? Diem { get; set; }

    public int? Thedo { get; set; }

    public int? Thevang { get; set; }

    public int? Ketqua { get; set; }

    public string? Iddoibong { get; set; }

    public int? Idtrandau { get; set; }

    public virtual Doibong? IddoibongNavigation { get; set; }

    public virtual Footballmatch? IdtrandauNavigation { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
