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
using DevExpress.Xpf.Core.Native;
using FootBallProject.Model;

namespace FootBallProject.UserControlBar
{
    /// <summary>
    /// Interaction logic for TransferWindowUC.xaml
    /// </summary>
    public partial class TransferWindowUC : UserControl
    {
        public TransferWindowUC()
        {
            InitializeComponent();

        }

        private void transferWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            if (USER.ROLE == "Admin" || USER.ROLE == "Assistant")
            {
                if (USER.ROLE == "Admin")
                {
                    txbDS.Visibility = Visibility.Collapsed;
                    border1.Visibility = Visibility.Collapsed;
                    border2.Margin = new Thickness(0, 50, 10, 0);
                    txblTransfer.Margin = txbDS.Margin;
                    dgrid2.Height = border2.Height = 600;
                    Reload.Margin= new Thickness(718, 25, 10, 0);
                }
                //var i1 = dgrid1.Columns.Single(c => c.Header.ToString() == "Button").DisplayIndex;
                //var i2 = dgrid2.Columns.Single(c => c.Header.ToString() == "Button").DisplayIndex;
                //var i3 = dgrid3.Columns.Single(c => c.Header.ToString() == "Button").DisplayIndex;

                var i1 = 4;
                var i2 = 5;
                var i3 = -1;
                dgrid1.Columns[i1].Visibility = dgrid2.Columns[i2].Visibility = dgrid3.Columns[i1].Visibility = Visibility.Hidden;

            }
        }
    }
}
