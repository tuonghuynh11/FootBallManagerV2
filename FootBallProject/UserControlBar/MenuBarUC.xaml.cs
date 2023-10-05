using FootBallProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace FootBallProject.UserControlBar
{
    /// <summary>
    /// Interaction logic for MenuBarUC.xaml
    /// </summary>
    
    public partial class MenuBarUC : UserControl
    {
        DispatcherTimer timer;
        double panelWidth;
        bool hiden = true;
        public MenuBarUC()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Tick += Timer_Tick;

            panelWidth = sidePanel.Width;

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (hiden)
            {
                sidePanel.Width += 4;
                if (sidePanel.Width >= 150)
                {
                    timer.Stop();
                    hiden = false;
                }
            }
            else
            {
                sidePanel.Width -= 4;
                if (sidePanel.Width <= panelWidth)
                {
                    timer.Stop();
                    hiden = true;
                }
            }
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
        
    }
}
