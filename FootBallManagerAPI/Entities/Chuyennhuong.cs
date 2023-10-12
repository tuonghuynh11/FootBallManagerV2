using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Chuyennhuong
{
    public int Id { get; set; }

    public int? Idcauthu { get; set; }

    public string? Iddoimua { get; set; }

    public virtual Cauthu? IdcauthuNavigation { get; set; }

    public virtual Doibong? IddoimuaNavigation { get; set; }
}
