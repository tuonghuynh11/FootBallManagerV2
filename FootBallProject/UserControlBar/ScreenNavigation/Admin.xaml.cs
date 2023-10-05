using FootBallProject.Class;
using FootBallProject.Model;
using FootBallProject.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FootBallProject.UserControlBar.ScreenNavigation
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : UserControl
    {
        int t1 = 1;
        int tm = 2;
        int t2 = 3;
        public Admin()
        {
            InitializeComponent();
            this.DataContext = new AdminScreenViewModel();
            //Timer 
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            if (t1 > 8)
            {
                t1 = t2 + 1;
            }
            if (tm > 8)
            {
                tm = t1;
            }
            if (t2 + 1 > 8)
            {
                t2 = 1;
            }

            Thumnail1.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/" + (t2 + 1) + ".jpg"));

            ThumnailMain.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/" + (t1) + ".jpg"));


            Thumnail2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/" + (tm) + ".jpg"));

            tm = t1;
            t1 = t2 + 1;
            t2 = t2 + 1;
        }
        private void bttFoward(object sender, RoutedEventArgs e)
        {
            if (t1 > 8)
            {
                t1 = t2 + 1;
            }
            if (tm > 8)
            {
                tm = t1;
            }
            if (t2 + 1 > 8)
            {
                t2 = 1;
            }

            Thumnail1.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/" + (t2 + 1) + ".jpg"));

            ThumnailMain.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/" + (t1) + ".jpg"));


            Thumnail2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/" + (tm) + ".jpg"));

            tm = t1;
            t1 = t2 + 1;
            t2 = t2 + 1;
        }

        private void bttBack(object sender, RoutedEventArgs e)
        {
            if (t1 > 8)
            {
                t1 = tm;
            }
            if (tm > 8)
            {
                tm = t2;
            }
            if (t2 + 1 > 8)
            {
                t2 = 1;
            }



            Thumnail1.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/" + (tm) + ".jpg"));

            ThumnailMain.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/" + (t2) + ".jpg"));


            Thumnail2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/" + (t2 + 1) + ".jpg"));



            t1 = tm;
            tm = t2;
            t2 = t2 + 1;
        }
        private void DsClb(object sender, RoutedEventArgs e)
        {
            DS_CLB dS_CLB = new DS_CLB();

            dS_CLB.Show();
        }

        private void DsGiaiDau(object sender, RoutedEventArgs e)
        {
            DS_GiaiDau dS_GiaiDau = new DS_GiaiDau();
            dS_GiaiDau.Show();
        }


        //Xem thông tin cầu thủ trong best players
        private void lvBestPlayers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView list = (ListView)sender;
            CAUTHU ct = list.SelectedItem as CAUTHU;
            PlayerProfile profile = new PlayerProfile(ct);
            profile.ShowDialog();
            list.UnselectAll();

        }

        //Chi tiết trận đấu
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var tt = lsTranDau.SelectedItem as FOOTBALLMATCH;
            if (tt != null)
            {
                DOIBONG team1 = DataProvider.ins.DB.DOIBONGs.Find(tt.IDDOI1);
                DOIBONG team2 = DataProvider.ins.DB.DOIBONGs.Find(tt.IDDOI2);
                MatchInfomation matchInfomation = new MatchInfomation();
                MatchInformationViewModel matchInformationViewModel = new MatchInformationViewModel(team1, team2);
                matchInfomation.DataContext = matchInformationViewModel;
                matchInfomation.Show();
            }

        }

        private void best_teams(object sender, MouseButtonEventArgs e)
        {
            ListView list = (ListView)sender;
            BestTeam teams = list.SelectedItem as BestTeam;
            ThongTinCLB thongTinCLB = new ThongTinCLB(teams.TEN);
            DOIBONG db = DataProvider.ins.DB.DOIBONGs.Find(teams.ID);
            ClubInfomationViewModel club = new ClubInfomationViewModel(db);
            thongTinCLB.DataContext = club;
            thongTinCLB.ShowDialog();
            list.UnselectAll();
        }
    }
}
