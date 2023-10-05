using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FootBallProject.Class
{
    public class BestTeam
    {
        public string ID { get; set; }
        public Nullable<int> IDQUOCTICH { get; set; }
        public Nullable<int> THANHPHO { get; set; }
        public byte[] HINHANH { get; set; }
        public string TEN { get; set; }
        public Nullable<int> SOLUONGTHANHVIEN { get; set; }
        public Nullable<System.DateTime> NGAYTHANHLAP { get; set; }
        public string SANNHA { get; set; }
        public string SODOCHIENTHUAT { get; set; }
        public  Nullable<long> GIATRI { get; set; }

        public int IDGIAIDAU { get; set; }
        public string IDDOIBONG { get; set; }
        public Nullable<int> WIN { get; set; }
        public Nullable<int> DRAW { get; set; }
        public Nullable<int> LOSE { get; set; }
        public Nullable<int> GA { get; set; }
        public Nullable<int> GD { get; set; }
        public int POINTS { get; set; }
        public System.Windows.Media.Brush brush
        {
            get
            {
                Random r = new Random();

                SolidColorBrush brush = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 233)));
                return brush;
            }

            set
            {
            }
        }

    }
}
