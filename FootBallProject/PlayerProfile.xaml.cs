using FootBallProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for PlayerProfile.xaml
    /// </summary>
    public partial class PlayerProfile : Window
    {
        public PlayerProfile()
        {
            InitializeComponent();
            
           
        }
        public PlayerProfile(CAUTHU player)
        {
            InitializeComponent();
            if (player.HINHANH != null&&player.HINHANH.Length != 0)
            {
                image.ImageSource = LoadImage(player.HINHANH);

            }
            Nametbl.Text = player.HOTEN;
            Agetbl.Text = player.TUOI.ToString();
            Clubtbl.Text = player.TENDOIBONG;
            Heighttbl.Text = player.CHIEUCAO;
            Weighttbl.Text = player.CANNANG;
            KitNumbertbl.Text = player.SOAO.ToString();
            Positiontbl.Text = player.VITRI;
            Goalstbl.Text = player.SOBANTHANG.ToString();
            LeagueSumtbl.Text = player.SOGIAI.ToString();
            Nationlb.Text = player.QUOCGIA;
            Physiclb.Text = player.THETRANG;
            Pricelb.Text= String.Format("${0:n0}", player.GIATRICAUTHU);
            Footlb.Text = player.CHANTHUAN;
        }
        private static BitmapImage LoadImage(byte[] imageData)
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
    public class Statistics
    {
        int appearances;
        int goals;
        int assists;
        int left_foot;
        int right_foot;
        int head;
        public Statistics(int appearances = 0, int goals = 0, int assists = 0, int lfoot = 0, int rfoot = 0, int head = 0)
        {
            this.appearances = appearances;
            this.goals = goals;
            this.assists = assists;
            left_foot = lfoot;
            right_foot = rfoot;
            this.head = head;
        }
        public int Appearances { get { return appearances; } }
        public int Goals { get { return goals; } }
        public int Assists { get { return assists; } }
        public int Left_foot { get { return left_foot; } }
        public int Right_foot { get { return right_foot; } } 
        public int Head { get { return head; } }
    }
}
