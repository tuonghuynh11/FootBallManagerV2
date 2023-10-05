using FootBallProject.Model;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FootBallProject
{
    public class Players
    {
        public string name;
        public string Position { get; set ; }
        public string Name { get; set; }
           
        public string Role { get; set; }
        public string Image { get; set; }
        public int SoThuTu { get; set; }    
    
        public Brush brush { get; set; }


        public void Exchange(Players a)
        {
            string term1 = a.Name;
            a.Name = this.Name;
            this.Name = term1;

            string term2 = a.Image;
            a.Image = this.Image;
            this.Image = term2;
        }
        public Players PlayerCopy(Players pl, string pos, int stt)
        {
            this.Name = pl.Name;
            this.Role = pl.Role;
            this.Position = pos;
            this.Image = pl.Image;
            this.SoThuTu = stt;
            return this;
        }

        public Players PlayerCopy(CAUTHU pl, string pos, int stt)
        {
            this.Name = pl.HOTEN;
            this.Role = pl.VITRI;
            this.Position = pos;
           // this.Image = pl.HINHANH;
            this.SoThuTu = stt;
            return this;
        }
    }
}
