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
using FootBallProject.Model;
using FootBallProject.ViewModel;

namespace FootBallProject.UserControlBar
{
    /// <summary>
    /// Interaction logic for TeamPlayersUC.xaml
    /// </summary>
    public partial class TeamPlayersUC : UserControl
    {
       
        public TeamPlayersUC()
        {
            InitializeComponent();
        }

        private void RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int x = Players_List.SelectedIndex;
            //object row = Players_List.SelectedItem;
            //string Name = (Players_List.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;
            //MessageBox.Show(Name);
            PlayerProfile pp = new PlayerProfile();
            pp.HorizontalAlignment = HorizontalAlignment.Right;

            //pp.DataContext = players[x];
            pp.Show();

        }

        private void teamPlayersUC1_Loaded(object sender, RoutedEventArgs e)
        {
            if (USER.ROLE != "Admin")
            {
                teamCMB.IsEditable = teamCMB.IsEnabled = teamCMB.IsReadOnly = false;
    
            }
        }
    }


}
