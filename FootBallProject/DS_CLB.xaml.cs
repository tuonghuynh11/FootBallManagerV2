using DevExpress.Xpf.Grid;
using FootBallProject.Model;
using FootBallProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for DS_CLB.xaml
    /// </summary>
    public partial class DS_CLB : Window
    {
      
        public DS_CLB()
        {
            InitializeComponent();
            //Tùy chọn tìm kiếm comboBox
            this.DataContext = new DSCLBViewModel();
            List<string> header = new List<String>();
            header.Add("ID đội bóng");
            header.Add("Tên đội bóng");
            header.Add("Huấn luyện viên");
            cbSearchColumn.ItemsSource = header;

            ///////////////////
        
        }

        private void cbSearchColumn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combobox = sender as ComboBox;
            if (combobox.SelectedItem.ToString() == "ID đội bóng")
            {
                tbviewCLB.SearchColumns = "ID";
            }
            else if (combobox.SelectedItem.ToString() == "Tên đội bóng")
            {
                tbviewCLB.SearchColumns = "TEN";
            }

            else if (combobox.SelectedItem.ToString() == "Huấn luyện viên")

            {
                tbviewCLB.SearchColumns = "HLV";

            }
        }

        private void bttClubInfo_Click(object sender, RoutedEventArgs e)
        {
            DOIBONG teams = grdcontrolCLB.SelectedItems[0] as DOIBONG;
            ThongTinCLB thongTinCLB = new ThongTinCLB(teams.TEN);
            ClubInfomationViewModel club = new ClubInfomationViewModel(teams);
          
            thongTinCLB.DataContext = club;
            thongTinCLB.Show();
        }
    }
}
