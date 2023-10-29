using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Refreshtoken
{
    public Guid Id { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; } = null!;

    public string JwtId { get; set; } = null!;

    public bool IsRevoked { get; set; }

    public DateTime IssuedAt { get; set; }

    public DateTime Expired { get; set; }

    public bool? IsUsed { get; set; }

    public virtual User User { get; set; } = null!;
}
