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

namespace FootBallProject.Component.League
{
    /// <summary>
    /// Interaction logic for LeagueCard.xaml
    /// </summary>
    public partial class LeagueCard : UserControl
    {
        public LeagueCard()
        {
            InitializeComponent();
            if (AccessUser.userLogin.USERROLE.ID == 2)
            {
                btnEnable.Visibility = Visibility.Visible;
            }
            else btnEnable.Visibility = Visibility.Hidden;

        }
    }
}
