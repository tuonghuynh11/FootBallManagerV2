using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FootBallProject
{
    public class Teams
    {
        public string ID { get; set; }

        public string Name { get; set; }
        public string logo { get; set; }
        public string Coach { get; set; }
        public string Founded { get; set; }
        public string Stadium { get; set; }
        public string Nation { get; set; }
        public string City { get; set; }




        public int Win { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public int Ga { get; set; }
        public int Gd { get; set; }
        public int Pts { get; set; }
        public List<Players> playersOfTeam { get; set; }
        public Brush brush { get; set; }
    }
}
