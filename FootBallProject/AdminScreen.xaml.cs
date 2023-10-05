
using FootBallProject.Class;
using FootBallProject.Model;
using FootBallProject.ViewModel;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for AdminScreen.xaml
    /// </summary>
    public partial class AdminScreen : Window
    {
        //So sánh sự thay đổi của database
        List<Notification> data1;
        List<Notification> data2;
        int flag = 0;
        int newnotifies = 0;
        //So sánh sự thay đổi của database
        public ObservableCollection<Notification> Notifies { get; set; }
        public AdminScreen()
        {
            InitializeComponent();
           

            this.DataContext = new MenuBarViewModel();
            //Timer 
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(4);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            Catch();
        }
        public void Catch()
        {
            SnackbarNotify.IsActive = false;
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
                        ShowSnackBar();
                    }

                }
            }
            data2 = data1;

        }
        void ShowSnackBar()
        {
            SnackbarNotify.MessageQueue?.Enqueue($"Bạn có {newnotifies} thông báo mới", null, null, null, false, true, TimeSpan.FromSeconds(4));

        }
    }
}

