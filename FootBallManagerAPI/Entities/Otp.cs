using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Otp
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public DateTime? Time { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
