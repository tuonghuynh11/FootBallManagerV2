using System;

namespace FootBallManagerAPI.Models
{
    public class FootBallTeamJoin
    {
        public string ID { get; set; } = null!;

        public Nullable<int> IDQUOCTICH { get; set; }

        public Nullable<int> THANHPHO { get; set; }

        public byte[] HINHANH { get; set; }

        public string? TEN { get; set; }

        public Nullable<int> SOLUONGTHANHVIEN { get; set; }

        public Nullable<DateTime> NGAYTHANHLAP { get; set; }

        public string? SANNHA { get; set; }

        public string? SODOCHIENTHUAT { get; set; }

        public Nullable<long> GIATRI { get; set; }

      
    }
}