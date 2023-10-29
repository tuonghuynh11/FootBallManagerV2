using DevExpress.Data.Browsing;
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

        

     
        private void Send_Offer(object sender, RoutedEventArgs e)
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

            MessageBox.Show(dOIBONG.TEN);
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
                if(textBox.Text==null || textBox.Text == "")
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

                DTG_SupplierContracted.ItemsSource= filter;
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
    }
}
