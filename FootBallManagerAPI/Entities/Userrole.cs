using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Userrole
{
    public int Id { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
