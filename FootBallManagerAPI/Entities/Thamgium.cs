using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Thamgium
{
    public int Idtran { get; set; }

    public int Idcauthu { get; set; }

    public int? Sobanthang { get; set; }

    public virtual Cauthu IdcauthuNavigation { get; set; } = null!;

    public virtual Footballmatch IdtranNavigation { get; set; } = null!;
}
