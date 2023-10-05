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
using FootBallProject.PopUp;
namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for EditPlayerForm.xaml
    /// </summary>
    public partial class EditPlayerForm : Window
    {
        public EditPlayerForm()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Close_but_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void editPlayerForm1_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Chân thuận đang trống!!\n Nếu cẩn thận, bạn nhớ check chân thuận trên profile trước");
           
        }

       
    }
}
