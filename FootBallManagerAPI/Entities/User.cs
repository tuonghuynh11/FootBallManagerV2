using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class User
{
    public int Id { get; set; }

    public int? Iduserrole { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Displayname { get; set; }

    public string? Email { get; set; }

    public int? Idotp { get; set; }

    public int? Idavatar { get; set; }

    public int? Idnhansu { get; set; }

    public byte[]? Avatar { get; set; }

    public virtual Otp? IdotpNavigation { get; set; }

    public virtual Userrole? IduserroleNavigation { get; set; }
}
