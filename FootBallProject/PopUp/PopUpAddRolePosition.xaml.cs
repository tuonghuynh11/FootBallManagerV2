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
    /// Interaction logic for PopUpAddRolePosition.xaml
    /// </summary>
    public partial class PopUpAddRolePosition : Window
    {
        public string role = "";
        public int ok = 0;
        public int cancel = 0;
        public PopUpAddRolePosition()
        {
            InitializeComponent();
        }
        public PopUpAddRolePosition(string rl)
        {
            InitializeComponent();
            if (rl==null||rl=="")
            {
                Vaitro.Text = "";
            }
            else
            {
                Vaitro.Text = rl;
            }
        }

        private void Vaitro_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            this.role = textBox.Text;
        }
        public string GetRole()
        {
            return this.role;
        }

        private void Okbtt_Click(object sender, RoutedEventArgs e)
        {
            ok = 1;
            cancel = 0;
            this.Close();
        }

        private void Cancelbtt_Click(object sender, RoutedEventArgs e)
        {
            cancel = 1;
            ok = 0;
            this.Close();
        }
    }
}
