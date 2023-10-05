using FootBallProject.Class;
using FootBallProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.ViewModel
{
    public class AdminScreenViewModel : BaseViewModel
    {
        private ObservableCollection<DOIBONG> _Teams;
        public ObservableCollection<DOIBONG> Teams { get => _Teams; set { _Teams = value; OnPropertyChanged(); } }


        private ObservableCollection<BestTeam> _BestTeams;
        public ObservableCollection<BestTeam> BestTeams { get => _BestTeams; set { _BestTeams = value; OnPropertyChanged(); } }

        private ObservableCollection<CAUTHU> _BestPlayers;
        public ObservableCollection<CAUTHU> BestPlayers { get => _BestPlayers; set { _BestPlayers = value; OnPropertyChanged(); } }

        private ObservableCollection<THONGTINGIAIDAU> _TournamentInformation;
        public ObservableCollection<THONGTINGIAIDAU> TournamentInformation { get => _TournamentInformation; set { _TournamentInformation = value; OnPropertyChanged(); } }


        private ObservableCollection<FOOTBALLMATCH> _MatchInformation;
        public ObservableCollection<FOOTBALLMATCH> MatchInformation { get => _MatchInformation; set { _MatchInformation = value; OnPropertyChanged(); } }

        public LEAGUE league { get; set; }
        public AdminScreenViewModel()
        {

            Teams = new ObservableCollection<DOIBONG>(DataProvider.ins.DB.Database.SqlQuery<DOIBONG>("SELECT TOP(4) * FROM DOIBONG"));
            BestPlayers = new ObservableCollection<CAUTHU>(DataProvider.ins.DB.Database.SqlQuery<CAUTHU>("SELECT TOP(5) * FROM CAUTHU ORDER BY SOBANTHANG DESC"));

            LEAGUE id_giaidau = DataProvider.ins.DB.Database.SqlQuery<LEAGUE>("SELECT TOP(1) * FROM LEAGUE ORDER BY ID ASC").FirstOrDefault<LEAGUE>();
            league = DataProvider.ins.Database.LEAGUEs.Find(id_giaidau.ID);
            TournamentInformation = new ObservableCollection<THONGTINGIAIDAU>(DataProvider.ins.DB.Database.SqlQuery<THONGTINGIAIDAU>("SELECT * FROM THONGTINGIAIDAU WHERE IDGIAIDAU =@ID ", new SqlParameter("@ID", id_giaidau.ID)));

            MatchInformation = new ObservableCollection<FOOTBALLMATCH>(DataProvider.ins.DB.Database.SqlQuery<FOOTBALLMATCH>("SELECT TOP(4) * FROM FOOTBALLMATCH ORDER BY THOIGIAN DESC"));



            var bt = (from a in DataProvider.ins.DB.THONGTINGIAIDAUs
                      join b in DataProvider.ins.DB.DOIBONGs on a.IDDOIBONG equals b.ID
                      orderby a.POINTS descending
                      select (a)).ToList<object>();
            var c = DataProvider.ins.DB.Database.SqlQuery<BestTeam>(" SELECT TOP(5) b.ID, b.IDQUOCTICH, b.THANHPHO, b.HINHANH, b.TEN,  b.SOLUONGTHANHVIEN,  b.NGAYTHANHLAP, b.SANNHA,  b.SODOCHIENTHUAT, b.GIATRI, a.POINTS FROM dbo.THONGTINGIAIDAU a JOIN dbo.DOIBONG b ON a.IDDOIBONG=b.ID ORDER BY a.POINTS DESC");
            BestTeams = new ObservableCollection<BestTeam>(c);

        }

    }
}
