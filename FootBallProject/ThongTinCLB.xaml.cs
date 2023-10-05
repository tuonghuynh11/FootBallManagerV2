using FootBallProject.Class.Format_team;
using FootBallProject.Model;
using FootBallProject.ViewModel;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for ThongTinCLB.xaml
    /// </summary>
    public partial class ThongTinCLB : Window
    {
        
        public ClubInfomationViewModel ClubInfomation;
        public ThongTinCLB()
        {
            InitializeComponent();
            this.DataContext = new ClubInfomationViewModel();
          
        }

        public ThongTinCLB(string TeamName)
        {
            InitializeComponent();
            this.DataContext = new ClubInfomationViewModel();
            var team = this.DataContext as ClubInfomationViewModel;
            titlebar.Tag = TeamName;
          
        }

      
        private void bttPlayerInfo_Click(object sender, RoutedEventArgs e)
        {
            CAUTHU player= dtgThongTinCLB.SelectedItem as CAUTHU;
            PlayerProfile playerProfile = new PlayerProfile(player);
            playerProfile.ShowDialog();
        }

     
      
        //Convert String to currency
        private void GTDHtbl_Loaded(object sender, RoutedEventArgs e)
        {
            var a = this.DataContext as ClubInfomationViewModel;
            GTDHtbl.Text = String.Format("${0:n0}", a.Team.GIATRI);
        }

        //Set cầu thủ mặc định khi xem đội bóng
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var a = this.DataContext as ClubInfomationViewModel;
            CAUTHU player = a.MainPlayers[0];
            TenCauThutbl.Text = player.HOTEN;
            QuocTichtbl.Text = player.QUOCGIA;
            ChieuCaotbl.Text = player.CHIEUCAO;
            CanNangtbl.Text = player.CANNANG;
            TheTrangtbl.Text = player.THETRANG;
            ChanThuantbl.Text = player.CHANTHUAN;
            if (player.HINHANH.Length!=0)
            {
                AnhCauThutbl.ImageSource = LoadImage(player.HINHANH);

            }
            SoAotbl.Text = player.SOAO.ToString();


            if (a.Team.SODOCHIENTHUAT=="4-3-3")
            {
                DoiHinhChienThuatUC433.Visibility = Visibility.Visible;
                DoiHinhChienThuatUC4231.Visibility = Visibility.Collapsed;
                DoiHinhChienThuatUC442.Visibility = Visibility.Collapsed;
                DoiHinhChienThuatUC433.DataContext = a.Teamformat;
                ///Thêm datacontext cho DoiHinhChienThuatUC433
            }
            else if (a.Team.SODOCHIENTHUAT == "4-4-2")
            {
                DoiHinhChienThuatUC433.Visibility = Visibility.Collapsed;
                DoiHinhChienThuatUC4231.Visibility = Visibility.Collapsed;
                DoiHinhChienThuatUC442.Visibility = Visibility.Visible;
                DoiHinhChienThuatUC442.DataContext = a.Teamformat;
                ///Thêm datacontext cho DoiHinhChienThuatUC442
            }
            else
            {
                DoiHinhChienThuatUC433.Visibility = Visibility.Collapsed;
                DoiHinhChienThuatUC4231.Visibility = Visibility.Visible;
                DoiHinhChienThuatUC442.Visibility = Visibility.Collapsed;
                DoiHinhChienThuatUC4231.DataContext = a.Teamformat;
                ///Thêm datacontext cho DoiHinhChienThuatUC4231
            }


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
}
