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

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for PopUpCustom.xaml
    /// </summary>
    public partial class PopUpCustom : Window
    {
        public PopUpCustom()
        {
            InitializeComponent();
        }
        public PopUpCustom(string a)
        {
            InitializeComponent();
            content.Text = a;
        }
        public PopUpCustom(string title, string text)
        {
            InitializeComponent();
            content.Text = text;

            titletxbl.Text = title;
            titletxbl.Foreground = Brushes.Yellow;
            border.Background = Brushes.Red;
        }
        public PopUpCustom(string title, string text,int n)
        {
            InitializeComponent();
            content.Text = text;
            titletxbl.Text = title;
            border.Background = Brushes.LightSeaGreen;
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
