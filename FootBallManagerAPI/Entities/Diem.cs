using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Diem
{
    public string Iddoibong { get; set; } = null!;

    public int Idgiaidau { get; set; }

    public int? Sodiem { get; set; }

    public virtual Doibong IddoibongNavigation { get; set; } = null!;
}
