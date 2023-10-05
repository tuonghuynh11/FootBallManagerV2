using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.Model
{
    public class DataProvider
    {
        private static DataProvider _ins;
        public static DataProvider ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new DataProvider();
                }
                return _ins;

            }
            set { _ins = value; }
        }
        private static DataProvider _Instance;
        public static DataProvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DataProvider();
                }
                return _Instance;

            }
            set { _Instance = value; }
        }
        public officialleagueEntities1 DB { get; set; }
        public officialleagueEntities1 Database { get; set; }
        private DataProvider()
        {
            DB = new officialleagueEntities1();
            Database = new officialleagueEntities1();

        }
    }
}
