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
    /// Interaction logic for MatchInfomation.xaml
    /// </summary>
    public partial class MatchInfomation : Window
    {
        public MatchInformationViewModel matchInformation;
        public MatchInfomation()
        {
            InitializeComponent();
        }

        private void GTDHtbl_DOINHA_Loaded(object sender, RoutedEventArgs e)
        {
            var a = this.DataContext as MatchInformationViewModel;
            GTDHtbl_DOINHA.Text = String.Format("${0:n0}", a.Team1.GIATRI);
        }

        private void GTDHtbl_DOIKHACH_Loaded(object sender, RoutedEventArgs e)
        {
            var a = this.DataContext as MatchInformationViewModel;
            GTDHtbl_DOIKHACH.Text = String.Format("${0:n0}", a.Team2.GIATRI);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var a = this.DataContext as MatchInformationViewModel;
            //Đội nhà
            CAUTHU player = a.MainPlayers1[0];
           
            TenCauThutbl.Text = player.HOTEN;
            QuocTichtbl.Text = player.QUOCGIA;
            ChieuCaotbl.Text = player.CHIEUCAO;
            CanNangtbl.Text = player.CANNANG;
            TheTrangtbl.Text = player.THETRANG;
            ChanThuantbl.Text = player.CHANTHUAN;
            if (player.HINHANH.Length != 0)
            {
                AnhCauThutbl.ImageSource = LoadImage(player.HINHANH);

            }
            SoAotbl.Text = player.SOAO.ToString();
            //Đội khách
            CAUTHU player1 = a.MainPlayers2[0];
            TenCauThutbl_DOIKHACH.Text = player1.HOTEN;
            QuocTichtbl_DOIKHACH.Text = player1.QUOCGIA;
            ChieuCaotbl_DOIKHACH.Text = player1.CHIEUCAO;
            CanNangtbl_DOIKHACH.Text = player1.CANNANG;
            TheTrangtbl_DOIKHACH.Text = player1.THETRANG;
            ChanThuantbl_DOIKHACH.Text = player1.CHANTHUAN;
            if (player.HINHANH.Length != 0)
            {
                AnhCauThutbl_DOIKHACH.ImageSource = LoadImage(player1.HINHANH);

            }
            SoAotbl_DOIKHACH.Text = player1.SOAO.ToString();

            //Đội nhà
            if (a.Team1.SODOCHIENTHUAT == "4-3-3")
            {
                DoiHinhChienThuatUC433_DOINHA.Visibility = Visibility.Visible;
                DoiHinhChienThuatUC4231_DOINHA.Visibility = Visibility.Collapsed;
                DoiHinhChienThuatUC442_DOINHA.Visibility = Visibility.Collapsed;
                DoiHinhChienThuatUC433_DOINHA.DataContext = a.Teamformat1;
                ///Thêm datacontext cho DoiHinhChienThuatUC433
            }
            else if (a.Team1.SODOCHIENTHUAT == "4-4-2")
            {
                DoiHinhChienThuatUC433_DOINHA.Visibility = Visibility.Collapsed;
                DoiHinhChienThuatUC4231_DOINHA.Visibility = Visibility.Collapsed;
                DoiHinhChienThuatUC442_DOINHA.Visibility = Visibility.Visible;
                DoiHinhChienThuatUC442_DOINHA.DataContext = a.Teamformat1;
                ///Thêm datacontext cho DoiHinhChienThuatUC442
            }
            else
            {
                DoiHinhChienThuatUC433_DOINHA.Visibility = Visibility.Collapsed;
                DoiHinhChienThuatUC4231_DOINHA.Visibility = Visibility.Visible;
                DoiHinhChienThuatUC442_DOINHA.Visibility = Visibility.Collapsed;
                DoiHinhChienThuatUC4231_DOINHA.DataContext = a.Teamformat1;
                ///Thêm datacontext cho DoiHinhChienThuatUC4231
            }

            //Đội khách

            if (a.Team2.SODOCHIENTHUAT == "4-3-3")
            {
                DoiHinhChienThuatUC433_DOINHA.Visibility = Visibility.Visible;
                DoiHinhChienThuatUC4231_DOIKHACH.Visibility = Visibility.Collapsed;
                DoiHinhChienThuatUC442_DOIKHACH.Visibility = Visibility.Collapsed;
                DoiHinhChienThuatUC433_DOIKHACH.DataContext = a.Teamformat2;
                ///Thêm datacontext cho DoiHinhChienThuatUC433
            }
            else if (a.Team2.SODOCHIENTHUAT == "4-4-2")
            {
                DoiHinhChienThuatUC433_DOIKHACH.Visibility = Visibility.Collapsed;
                DoiHinhChienThuatUC4231_DOIKHACH.Visibility = Visibility.Collapsed;
                DoiHinhChienThuatUC442_DOIKHACH.Visibility = Visibility.Visible;
                DoiHinhChienThuatUC442_DOIKHACH.DataContext = a.Teamformat2;
                ///Thêm datacontext cho DoiHinhChienThuatUC442
            }
            else
            {
                DoiHinhChienThuatUC433_DOIKHACH.Visibility = Visibility.Collapsed;
                DoiHinhChienThuatUC4231_DOIKHACH.Visibility = Visibility.Visible;
                DoiHinhChienThuatUC442_DOIKHACH.Visibility = Visibility.Collapsed;
                DoiHinhChienThuatUC4231_DOIKHACH.DataContext = a.Teamformat2;
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

        private void bttPlayerInfo_click(object sender, RoutedEventArgs e)
        {
            CAUTHU player = dtgThongTinCLB.SelectedItem as CAUTHU;
            PlayerProfile playerProfile = new PlayerProfile(player);
            playerProfile.ShowDialog();
        }

        private void bttPlayerInfo_doikhach_click(object sender, RoutedEventArgs e)
        {
            CAUTHU player = dtgThongTinCLB_DOIKHACH.SelectedItem as CAUTHU;
            PlayerProfile playerProfile = new PlayerProfile(player);
            playerProfile.ShowDialog();
        }
    }
}
