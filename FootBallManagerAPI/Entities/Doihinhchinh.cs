using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Doihinhchinh
{
    public string Iddoibong { get; set; } = null!;

    public int Idcauthu { get; set; }

    public string? Vitri { get; set; }

    public string? Vaitro { get; set; }

    public virtual Cauthu IdcauthuNavigation { get; set; } = null!;

    public virtual Doibong IddoibongNavigation { get; set; } = null!;
}
