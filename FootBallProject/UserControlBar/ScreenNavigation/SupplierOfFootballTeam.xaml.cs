using FootBallProject.Model;
using FootBallProject.Service;
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

namespace FootBallProject.UserControlBar.ScreenNavigation
{
    /// <summary>
    /// Interaction logic for SupplierOfFootballTeam.xaml
    /// </summary>
    public partial class SupplierOfFootballTeam : UserControl
    {
        public SupplierOfFootballTeam()
        {
            InitializeComponent();
        }

        private async void Cancel_Offer_Click(object sender, RoutedEventArgs e)
        {
            DOIBONGSUPPLIER ds = DTG_wait.SelectedItem as DOIBONGSUPPLIER;
            if (ds != null)
            {
                ds.status = 3;
                try
                {
                    await APIService.ins.UpdateStatusDoiBongSupplier(ds);
                    (this.DataContext as SupplierOfFootBallTeamViewModel).SuppliersWaitConfirm.Remove(ds);
                }
                catch (Exception)
                {

                    Error err = new Error("Đã xảy ra lỗi !");
                    err.ShowDialog();
                }
            }
        }

        private async void Confirm_Offer_Click(object sender, RoutedEventArgs e)
        {
            DOIBONGSUPPLIER ds = DTG_wait.SelectedItem as DOIBONGSUPPLIER;
            if (ds != null)
            {
                ds.status = 2;
                try
                {
                    await APIService.ins.UpdateStatusDoiBongSupplier(ds);
                    (this.DataContext as SupplierOfFootBallTeamViewModel).SuppliersWaitConfirm.Remove(ds);
                    DTG_wait.Items.Refresh();
                    (this.DataContext as SupplierOfFootBallTeamViewModel).SuppliersCooperated.Add(ds);
                }
                catch (Exception)
                {

                    Error err = new Error("Đã xảy ra lỗi !");
                    err.ShowDialog();
                }
            }
        }

        private async void Focus_GotFocus(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    await (this.DataContext as SupplierOfFootBallTeamViewModel).LoadSupplierWaitConfirm();
            //    DTG_wait.Items.Refresh();
            //}
            //catch (Exception ex)
            //{

            //    return;
            //}
          
        }

        private async void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            await(this.DataContext as SupplierOfFootBallTeamViewModel).LoadingData();
            DTG_SupplierContracted.Items.Refresh();
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox != null)
            {
                if (textBox.Text == null || textBox.Text == "")
                {

                    DTG_SupplierContracted.ItemsSource = (this.DataContext as SupplierOfFootBallTeamViewModel).SuppliersCooperated;
                }
                if((this.DataContext as SupplierOfFootBallTeamViewModel).SuppliersCooperated == null)
                {
                    return;
                }
                List<DOIBONGSUPPLIER> temp = (this.DataContext as SupplierOfFootBallTeamViewModel).SuppliersCooperated.ToList();
                List<DOIBONGSUPPLIER> filter = new List<DOIBONGSUPPLIER>();
                foreach (DOIBONGSUPPLIER item in temp)
                {
                    if (item.SUPPLIER.supplierName.ToLower().StartsWith(textBox.Text.ToLower()))
                    {
                        filter.Add(item);
                    }
                }

                DTG_SupplierContracted.ItemsSource = filter;
            }
        }

        private  void DTG_SupplierContracted_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dtg = (DataGrid)sender;
            DOIBONGSUPPLIER temp = dtg.SelectedItem as DOIBONGSUPPLIER;
            if (temp != null)
            {
                (this.DataContext as SupplierOfFootBallTeamViewModel).LoadSupplierInfo(temp.idSupplier);

            }
        }

        private void DTG_wait_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            DOIBONGSUPPLIER ds = dataGrid.SelectedItem as DOIBONGSUPPLIER;
            if (ds == null)
            {
                return;
            }
            else
            {
                if (dataGrid.CurrentCell.Column.DisplayIndex == 0)
                {
                    SupplierInformation info = new SupplierInformation(ds.idSupplier);
                    info.ShowDialog();
                    return;
                }
               

            }
        }

        private async void refreshWaitBtn_Click(object sender, RoutedEventArgs e)
        {
           await (this.DataContext as SupplierOfFootBallTeamViewModel).LoadSupplierWaitConfirm();
        }
    }
}
