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
using System.Windows.Shapes;

namespace FootBallProject.PopUp
{
    /// <summary>
    /// Interaction logic for OKCancelPopUp.xaml
    /// </summary>
    public partial class OKCancelPopUp : Window
    {
       public int Ok = 0;
        public OKCancelPopUp()
        {
            InitializeComponent();
        }

        private void Okbtt_Click(object sender, RoutedEventArgs e)
        {
            Ok = 1;
            this.Close();
        }

        private void Cancelbtt_Click(object sender, RoutedEventArgs e)
        {
            Ok = 0;
            this.Close();
        }
    }
}
