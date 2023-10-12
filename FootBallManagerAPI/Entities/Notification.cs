using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Notification
{
    public int Id { get; set; }

    public int? Idhlv { get; set; }

    public string? Notify { get; set; }

    public string? Checked { get; set; }

    public virtual Huanluyenvien? IdhlvNavigation { get; set; }
}
