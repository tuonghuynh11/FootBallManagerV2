using FootBallProject.Class;
using FootBallProject.Model;
using FootBallProject.PopUp;
using FootBallProject.Service;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FootBallProject.UserControlBar.ScreenNavigation
{
    /// <summary>
    /// Interaction logic for SupplierHomeScreen.xaml
    /// </summary>
    public partial class SupplierHomeScreen : UserControl
    {
        public SupplierHomeScreen()
        {
            InitializeComponent();



            //List<>

            //DTG_SupplierContract.ItemsSource = list;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            DTG_SupplierContracted.Items.Refresh();
        }

        private async void refreshBtn_Click(object sender, RoutedEventArgs e)
        {

            await (this.DataContext as SupplierHomeScreenViewModel).LoadingData();
            DTG_SupplierContracted.Items.Refresh();

        }




        private async void Send_Offer(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ListViewItem listViewItem = FindAncestor<ListViewItem>(button);

            // Check if a ListViewItem was found
            if (listViewItem != null)
            {
                // Select the ListViewItem
                lvUnCooperateFootBallTeams.SelectedItems.Clear();
                listViewItem.IsSelected = true;
            }

            DOIBONG dOIBONG = lvUnCooperateFootBallTeams.SelectedItem as DOIBONG;
            //Chỉnh idSupplier thành idSupplier đăng nhập
            DoiBongSupplierContractExtension db = new DoiBongSupplierContractExtension(idDoiBong: dOIBONG.ID, idSupplier: AccessUser.userLogin.IDSUPPLIER);
            db.ShowDialog();
            await (this.DataContext as SupplierHomeScreenViewModel).LoadingData();
            DTG_SupplierContracted.Items.Refresh();
        }

        private T FindAncestor<T>(DependencyObject current)
    where T : DependencyObject
        {
            do
            {
                if (current is T target)
                {
                    return target;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox != null)
            {
                if (textBox.Text == null || textBox.Text == "")
                {

                    DTG_SupplierContracted.ItemsSource = (this.DataContext as SupplierHomeScreenViewModel).FootBallTeamsCooperated;
                }
                List<DOIBONGSUPPLIER> temp = (this.DataContext as SupplierHomeScreenViewModel).FootBallTeamsCooperated.ToList();
                List<DOIBONGSUPPLIER> filter = new List<DOIBONGSUPPLIER>();
                foreach (DOIBONGSUPPLIER item in temp)
                {
                    if (item.DOIBONG.TEN.ToLower().StartsWith(textBox.Text.ToLower()))
                    {
                        filter.Add(item);
                    }
                }

                DTG_SupplierContracted.ItemsSource = filter;
            }
        }

        private void tb_UnCooperateTeams_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox != null)
            {
                if (textBox.Text == null || textBox.Text == "")
                {

                    lvUnCooperateFootBallTeams.ItemsSource = (this.DataContext as SupplierHomeScreenViewModel).FootBallTeamsUnCooperate;
                }
                List<DOIBONG> temp = (this.DataContext as SupplierHomeScreenViewModel).FootBallTeamsUnCooperate.ToList();
                List<DOIBONG> filter = new List<DOIBONG>();
                foreach (DOIBONG item in temp)
                {
                    if (item.TEN.ToLower().StartsWith(textBox.Text.ToLower()))
                    {
                        filter.Add(item);
                    }
                }

                lvUnCooperateFootBallTeams.ItemsSource = filter;
            }
        }

        private async void DTG_SupplierContracted_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid list = (DataGrid)sender;
            DOIBONGSUPPLIER dOIBONGSUPPLIER = list.SelectedItem as DOIBONGSUPPLIER;

            if (dOIBONGSUPPLIER != null)
            {
                if (list.CurrentCell.Column.DisplayIndex == 0)
                {
                    DOIBONG db = await APIService.ins.getDoiBongById(dOIBONGSUPPLIER.idDoiBong);
                    ThongTinCLB thongTinCLB = new ThongTinCLB(db.TEN);
                    ClubInfomationViewModel club = new ClubInfomationViewModel(db);

                    thongTinCLB.DataContext = club;
                    thongTinCLB.Show();
                }


                if (list.CurrentCell.Column.DisplayIndex == 4)
                {
                    TimeSpan difference = (TimeSpan)(dOIBONGSUPPLIER.endDate - DateTime.Now);

                    if (difference.Days < 0 || difference.Days < 10)
                    {
                        DoiBongSupplierContractExtension db = new DoiBongSupplierContractExtension(idDoiBong: dOIBONGSUPPLIER.idDoiBong, idSupplier: dOIBONGSUPPLIER.idSupplier, type: "Extend", doiBongSupplier: dOIBONGSUPPLIER);
                        db.ShowDialog();
                        await (this.DataContext as SupplierHomeScreenViewModel).LoadingData();
                        DTG_SupplierContracted.Items.Refresh();
                    }
                    //Gửi yêu cầu gia hạn hợp đồng
                }
            }



        }

        private async void DTG_wait_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid list = (DataGrid)sender;
            DOIBONGSUPPLIER dOIBONGSUPPLIER = list.SelectedItem as DOIBONGSUPPLIER;
            if (list.CurrentCell.Column.DisplayIndex == 0)
            {
                DOIBONG db = await APIService.ins.getDoiBongById(dOIBONGSUPPLIER.idDoiBong);
                ThongTinCLB thongTinCLB = new ThongTinCLB(db.TEN);
                ClubInfomationViewModel club = new ClubInfomationViewModel(db);

                thongTinCLB.DataContext = club;
                thongTinCLB.Show();
            }
        }

        private async void Focus_GotFocus(object sender, RoutedEventArgs e)
        {
            await (this.DataContext as SupplierHomeScreenViewModel).LoadFootBallTeamsWaitConfirm();
            DTG_wait.Items.Refresh();
        }

        private async void EditSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            EditSupplierInformation ed = new EditSupplierInformation(AccessUser.userLogin.IDSUPPLIER);
            ed.ShowDialog();
            if (ed.isChange == 1)
            {
                await (this.DataContext as SupplierHomeScreenViewModel).LoadingData();

            }
        }
    }
}
