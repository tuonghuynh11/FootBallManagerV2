using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Item
{
    public int Id { get; set; }

    public int? Idthongtintrandau { get; set; }

    public string? Iddoibong { get; set; }

    public int? Iditemtype { get; set; }

    public string? Thoigian { get; set; }

    public int? Idcauthu { get; set; }

    public virtual Cauthu? IdcauthuNavigation { get; set; }

    public virtual Doibong? IddoibongNavigation { get; set; }

    public virtual Itemtype? IditemtypeNavigation { get; set; }

    public virtual Thongtintrandau? IdthongtintrandauNavigation { get; set; }
}
