using FootBallProject.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FootBallProject.Class.Format_team
{
    abstract public class BasicTeam
    {
        public List<CAUTHU> team;
        public BitmapImage ST { get; set; }
        public BitmapImage LW { get; set; }
        public BitmapImage RW { get; set; }

        public BitmapImage LS { get; set; }
        public BitmapImage RS { get; set; }

        public BitmapImage CAM { get; set; }
        public BitmapImage CM1 { get; set; }
        public BitmapImage CM2 { get; set; }
        public BitmapImage LM { get; set; }
        public BitmapImage CM { get; set; }
        public BitmapImage RM { get; set; }
        public BitmapImage LCM { get; set; }
        public BitmapImage RCM { get; set; }

        public BitmapImage LB { get; set; }
        public BitmapImage CB1 { get; set; }
        public BitmapImage CB2 { get; set; }
        public BitmapImage RB { get; set; }
        public BitmapImage GK { get; set; }

        public static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
