using System;
using System.Collections.Generic;

namespace FootBallManagerAPI.Entities;

public partial class Tapluyen
{
    public int Id { get; set; }

    public int? Idnguoiquanly { get; set; }

    public string? Iddoibong { get; set; }

    public string? Trangthai { get; set; }

    public DateTime? Thoigianbatdau { get; set; }

    public DateTime? Thoigianketthuc { get; set; }

    public string? Hoatdong { get; set; }

    public string? Ghichu { get; set; }

    public virtual Doibong? IddoibongNavigation { get; set; }

    public virtual Huanluyenvien? IdnguoiquanlyNavigation { get; set; }
}
