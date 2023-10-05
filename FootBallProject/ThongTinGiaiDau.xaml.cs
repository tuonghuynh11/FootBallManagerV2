using FootBallProject.Class;
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
    /// Interaction logic for ThongTinGiaiDau.xaml
    /// </summary>
    public partial class ThongTinGiaiDau : Window
    {
     
        public string LeagueName { get; set; }
        public static int leagueid;
        public TournamentInformationViewModel tournamentInformation { get; set; }
        public ThongTinGiaiDau()
        {
            InitializeComponent();
        
           
                  
        }
        public ThongTinGiaiDau(int id, string leagueName)
        {
            InitializeComponent();
            leagueid = id;
            this.DataContext = tournamentInformation= new TournamentInformationViewModel(id);
            this.LeagueName = leagueName;
            this.titlebar.Tag = LeagueName;
        
        }

       

        private void dtgThongTinGiaDau_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            THONGTINGIAIDAU teams = dataGrid.SelectedItem as THONGTINGIAIDAU;

            ObservableCollection<MatchesPlayed> matched= new ObservableCollection<MatchesPlayed>(DataProvider.ins.DB.Database.SqlQuery<MatchesPlayed>(
                $"select  T1.IDDOIBONG IDDOIBONG1, T5.IDDOIBONG IDDOIBONG2, t1.diem DIEM1,t5.diem DIEM2 from THONGTINTRANDAU T1 JOIN THONGTINTRANDAU T5 ON T1.IDTRANDAU = T5.IDTRANDAU JOIN FOOTBALLMATCH T2 ON T1.IDTRANDAU = T2.ID JOIN ROUND T3 ON T2.IDVONG = T3.ID JOIN LEAGUE T4 ON T3.IDGIAIDAU = T4.ID where T1.IDDOIBONG <> T5.IDDOIBONG AND T1.IDDOIBONG = '{teams.IDDOIBONG}' AND T4.ID={leagueid}"));
       
            lsThongTinTranDau.ItemsSource=matched;

        }
    }
}
