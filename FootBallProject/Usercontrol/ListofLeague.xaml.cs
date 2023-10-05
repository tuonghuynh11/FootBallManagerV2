using FootBallProject.Class;
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

namespace FootBallProject.Usercontrol
{
    /// <summary>
    /// Interaction logic for ListofLeague.xaml
    /// </summary>
    public partial class ListofLeague : UserControl
    {
        public ListofLeague()
        {
            InitializeComponent();
            if (AccessUser.userLogin.USERROLE.ID == 2)
            {
                btnEnable.Visibility= Visibility.Visible;
            }
            else btnEnable.Visibility = Visibility.Hidden;
        }
    }
}
