using DevExpress.Xpf.Grid;
using FootBallProject.Model;
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
using System.Windows.Shapes;

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for DS_GiaiDau.xaml
    /// </summary>
    public partial class DS_GiaiDau : Window
    {
        public DS_GiaiDauViewModel giaiDauViewModel;
        public DS_GiaiDau()
        {
            InitializeComponent();
            this.DataContext = giaiDauViewModel=new DS_GiaiDauViewModel();
         }

      
       

        private void tbviewGiaiDau_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CardView cardView = sender as CardView;
            var carditem = cardView.SelectedRows[0] as LEAGUE;
           // MessageBox.Show(carditem.Name, "Name leagues");
            ThongTinGiaiDau thongTinGiaiDau = new ThongTinGiaiDau(carditem.ID,carditem.TENGIAIDAU) ;
            thongTinGiaiDau.Show();
            
          
        }
    }
}
