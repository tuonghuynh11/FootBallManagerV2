using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Thongtingiaidau
{
    public int Idgiaidau { get; set; }

    public string Iddoibong { get; set; } = null!;

    public int? Win { get; set; }

    public int? Draw { get; set; }

    public int? Lose { get; set; }

    public int? Ga { get; set; }

    public int? Gd { get; set; }

    public int? Points { get; set; }

    public virtual Doibong IddoibongNavigation { get; set; } = null!;

    public virtual League IdgiaidauNavigation { get; set; } = null!;
}
