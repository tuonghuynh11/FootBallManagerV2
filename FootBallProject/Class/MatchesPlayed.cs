using FootBallProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.Class
{
    public class MatchesPlayed
    {
        public int DIEM1 { get; set; }
        public string IDDOIBONG1 { get; set; }

        public string _TENDOIBONG1;
        public string TENDOIBONG1
        {
            get
            {
                var qg = DataProvider.ins.DB.DOIBONGs.Find(IDDOIBONG1);

                return qg == null ? " " : qg.TEN;
            }
            set
            {

            }

        }

        public byte[] HINHANHDOIBONG1
        {
            get
            {
                var qg = DataProvider.ins.DB.DOIBONGs.Find(IDDOIBONG1);
                return qg.HINHANH;

            }
            set { }

        }
        public int DIEM2 { get; set; }
        public string IDDOIBONG2 { get; set; }
        public string _TENDOIBONG2;
        public string TENDOIBONG2
        {
            get
            {
                var qg = DataProvider.ins.DB.DOIBONGs.Find(IDDOIBONG2);

                return qg == null ? " " : qg.TEN;
            }
            set
            {

            }

        }

        public byte[] HINHANHDOIBONG2
        {
            get
            {
                var qg = DataProvider.ins.DB.DOIBONGs.Find(IDDOIBONG2);
                return qg.HINHANH;

            }
            set { }

        }




    }
}
