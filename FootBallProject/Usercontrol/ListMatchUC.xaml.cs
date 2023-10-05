using FootBallProject.ViewModel;
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
    /// Interaction logic for ListMatchUC.xaml
    /// </summary>
    public partial class ListMatchUC : UserControl
    {
        public ListMatchUC()
        {
            InitializeComponent();
            this.DataContext = new ListMatchViewModel();
        }
    }
}
