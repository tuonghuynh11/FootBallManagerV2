using FootBallProject.Class;
using FootBallProject.Model;
using FootBallProject.ViewModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Diagnostics;
using System.IO;

namespace FootBallProject.UserControlBar
{
    /// <summary>
    /// Interaction logic for ControlBarUC.xaml
    /// </summary>
    public partial class ControlBarUC : UserControl
    {
        //So sánh sự thay đổi của database
        List<Notification> data1;
        List<Notification> data2;
        int flag = 0;
        int newnotifies = 0;

        Queue<string> FamousFootBallQuotes;
        public static byte[] Avatar;
        public   byte[] Avatar1;
        //So sánh sự thay đổi của database
        public ObservableCollection<Notification> Notifies { get; set; }
        public ControlBarViewModel ViewModel { get; set; }
        public ControlBarUC()
        {
            InitializeComponent();
            this.DataContext = ViewModel = new ControlBarViewModel();

            //FamousFootBallQuotes
            FamousFootBallQuotes = new Queue<string>();

            FamousFootBallQuotes.Enqueue("\"If you do not believe you can do it then you have no chance at all\" – Arsene Wenger\"");
            FamousFootBallQuotes.Enqueue("\"I learned all about life with a ball at my feet\" – Ronaldinho\"");
            FamousFootBallQuotes.Enqueue("\"The more difficult the victory, the greater the happiness in winning.\" – Pelé\"");
            FamousFootBallQuotes.Enqueue("\"You have to fight to reach your dream. You have to sacrifice and work hard for it.\" – Lionel Messi\"");
            FamousFootBallQuotes.Enqueue("\"I don’t have time for hobbies. At the end of the day, I treat my job as a hobby. It’s something I love doing.\" – David Beckham\"");
            FamousFootBallQuotes.Enqueue("\"When people succeed, it is because of hard work. Luck has nothing to do with success.\" –Diego Maradona\"");
            FamousFootBallQuotes.Enqueue("\"I once cried because I had no shoes to play soccer, but one day, I met a man who had no feet.\" – Zinedine Zidane\"");
            FamousFootBallQuotes.Enqueue("\"A penalty is a cowardly way to score.\" – Pelé\"");
            FamousFootBallQuotes.Enqueue("\"I don’t believe in superstitions. I just do certain things because I’m scared in case something will happen if I don’t do them.\" – Michael Owen\"");
            FamousFootBallQuotes.Enqueue("\"Playing football with your feet is one thing, but playing football with your heart is another.\" – Francesco Totti\"");
            FamousFootBallQuotes.Enqueue("\"They’re the second-best team in the world, and there’s no higher praise than that.\" – Kevin Keegan\"");
            FamousFootballQuotelb.Content = "\"They’re the second-best team in the world, and there’s no higher praise than that.\" – Kevin Keegan\"";
            //FamousFootBallQuotes

            UserNamelb.Content = USER.USERN;
            Avatar1 = DataProvider.ins.DB.USERS.Find(AccessUser.userLogin.ID).AVATAR;
            if (Avatar1 != null && Avatar1.Length != 0)
            {
                avatar.ImageSource = LoadImage(Avatar1);
            }

            int uncheck = DataProvider.ins.DB.Notifications.Where(p => p.IDHLV == AccessUser.userLogin.IDNHANSU && p.CHECKED == "Chưa xem").ToList().Count();
            numberofnotifies.Badge = uncheck;
            notifipopup.ToolTip = $"Bạn có {uncheck} thông báo mới";


            Notifies = new ObservableCollection<Notification>(DataProvider.ins.DB.Notifications.Where(p => p.IDHLV == AccessUser.userLogin.IDNHANSU).OrderByDescending(p => p.CHECKED).OrderByDescending(p => p.ID));

            //Notifies = new ObservableCollection<Notification>(DataProvider.ins.DB.Database.SqlQuery<Notification>($"SELECT  * FROM Notification WHERE IDHLV = {AccessUser.userLogin.IDNHANSU} order by  checked asc,id desc"));

            lvUsers.ItemsSource = Notifies;
            //Group thông báo
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("CHECKED");
            view.GroupDescriptions.Add(groupDescription);
            //Timer 
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3.5);
            timer.Tick += timer_Tick;
            timer.Start();

            //Timer for FamousFootBallQuotes
            DispatcherTimer FamousFootBallQuotesTimer = new DispatcherTimer();
            FamousFootBallQuotesTimer.Interval = TimeSpan.FromSeconds(60);
            FamousFootBallQuotesTimer.Tick += timer_Tick1;
            FamousFootBallQuotesTimer.Start();

            //Timer for updateavatar
            DispatcherTimer avatartimer = new DispatcherTimer();
            avatartimer.Interval = TimeSpan.FromSeconds(3.0);
            avatartimer.Tick += timer_Tick_avatartimer;
            avatartimer.Start();
        }
        public void setNewavatar(byte[] a){

            Avatar = a;
         }

        //Timer for updateavatar
        void timer_Tick_avatartimer(object sender, EventArgs e)
        {
           
            if (Avatar != null && Avatar.Length != 0)
            {
                if (Avatar1 != Avatar)
                {
                    avatar.ImageSource = LoadImage(Avatar);

                }
            }
      

        }

            //Timer for FamousFootBallQuotes
            void timer_Tick1(object sender, EventArgs e)
        {
            Random r = new Random();
            Random fontran = new Random();
            string quote = FamousFootBallQuotes.Dequeue();
            string nextquote = FamousFootBallQuotes.Peek();
            FamousFootBallQuotes.Enqueue(quote);
            FamousFootballQuotelb.Content = nextquote;
            switch (fontran.Next(1, 4))
            {
                case 1:
                    FamousFootballQuotelb.Foreground = new SolidColorBrush() {Color=Colors.Red};
                    break;
                case 2:
                    FamousFootballQuotelb.Foreground = new SolidColorBrush() { Color = Colors.Yellow };
                    break;
                case 3:
                    FamousFootballQuotelb.Foreground = new SolidColorBrush() { Color = Colors.White };
                    break;

            }
            switch (fontran.Next(1, 4))
            {
                case 1:
                    FamousFootballQuotelb.FontFamily = new FontFamily("Helvetica");
                    break;
                case 2:
                    FamousFootballQuotelb.FontFamily = new FontFamily("Garamond Pro");
                    break;
                case 3:
                    FamousFootballQuotelb.FontFamily = new FontFamily("VNI-Commerce");
                    break;

            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Catch();
        }
        public void Catch()
        {

            data1 = DataProvider.ins.Database.Notifications.ToList();
            if (flag == 0)
            {
                flag = 1;
                data2 = data1;
                return;
            }
            else
            {
                if (data1.Count() != data2.Count())
                {
                    List<Notification> list = new List<Notification>();
                    for (int i = data2.Count(); i < data1.Count(); i++)
                    {
                        list.Add(data1[i]);
                    }
                    //Thông báo mới
                    newnotifies = list.Where(p => p.IDHLV == AccessUser.userLogin.IDNHANSU).Count();
                    //Thông báo mới
                    if (newnotifies > 0)
                    {
                        int uncheck = DataProvider.ins.DB.Notifications.Where(p => p.IDHLV == AccessUser.userLogin.IDNHANSU && p.CHECKED == "Chưa xem").ToList().Count();
                        numberofnotifies.Badge = uncheck;
                        notifipopup.ToolTip = $"Bạn có {newnotifies} thông báo mới";
                        Notifies = new ObservableCollection<Notification>(DataProvider.ins.DB.Notifications.Where(p => p.IDHLV == AccessUser.userLogin.IDNHANSU).OrderByDescending(p => p.CHECKED).OrderByDescending(p => p.ID));
                        lvUsers.ItemsSource = Notifies;
                        //Group thông báo
                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
                        PropertyGroupDescription groupDescription = new PropertyGroupDescription("CHECKED");
                        view.GroupDescriptions.Add(groupDescription);
                    }

                }
            }
            data2 = data1;

        }

        private void Accountcbb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            ComboBoxItem select = (ComboBoxItem)combo.SelectedItem;
            if (select != null)
            {
                if (select.Content.ToString() == "Thông tin tài khoản")
                {
                    UserAccount userAccount = new UserAccount();
                    userAccount.ShowDialog();
                    combo.SelectedIndex = -1;
                }
                else if (select.Content.ToString() == "Đăng xuất")
                {

                    Window window = Application.Current.MainWindow as Window;

                    if (window != null)
                    {
                        LoginWindow mainWindow = new LoginWindow();
                        Application.Current.MainWindow = mainWindow;
                        Application.Current.MainWindow.Show();
                        window.Close();
                    }



                }

            }

        }


        private void lvUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = (ListView)sender;
            Notification notification = listView.SelectedItem as Notification;
            Notification update = DataProvider.ins.DB.Notifications.Find(notification.ID);
            try
            {
                update.CHECKED = "Đã xem";
                DataProvider.ins.DB.SaveChanges();
            }
            catch (Exception)
            {

                return;
            }
            
        }

        private void notifipopup_Opened(object sender, RoutedEventArgs e)
        {
            int uncheck = DataProvider.ins.DB.Notifications.Where(p => p.IDHLV == AccessUser.userLogin.IDNHANSU && p.CHECKED == "Chưa xem").ToList().Count();
            numberofnotifies.Badge = uncheck;
            //Notifies = new ObservableCollection<Notification>(DataProvider.ins.DB.Notifications.Where(p => p.IDHLV == AccessUser.userLogin.IDNHANSU).OrderByDescending(p => p.CHECKED).OrderByDescending(p => p.ID));
            Notifies = new ObservableCollection<Notification>(DataProvider.ins.DB.Database.SqlQuery<Notification>($"SELECT  * FROM Notification WHERE IDHLV = {AccessUser.userLogin.IDNHANSU} order by  checked asc,id desc"));

            lvUsers.ItemsSource = Notifies;
            //Group thông báo
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("CHECKED");
            view.GroupDescriptions.Add(groupDescription);
        }


        private void notifipopup_Closed(object sender, RoutedEventArgs e)
        {
            int uncheck = DataProvider.ins.DB.Notifications.Where(p => p.IDHLV == AccessUser.userLogin.IDNHANSU && p.CHECKED == "Chưa xem").ToList().Count();
            numberofnotifies.Badge = uncheck;
            notifipopup.ToolTip = $"Bạn có {uncheck} thông báo mới";

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
