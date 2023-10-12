using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Itemtype
{
    public int Id { get; set; }

    public string? Tenitem { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
