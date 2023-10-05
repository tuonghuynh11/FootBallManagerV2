using FootBallProject.Class.Format_team;
using FootBallProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.ViewModel
{
    public class ClubInfomationViewModel:BaseViewModel
    {
        private ObservableCollection<CAUTHU> _PlayerList;
        public ObservableCollection<CAUTHU> PlayerList { get => _PlayerList; set { _PlayerList = value; OnPropertyChanged(); } }

        private ObservableCollection<HUANLUYENVIEN> _StaffList;
        public ObservableCollection<HUANLUYENVIEN> StaffList { get => _StaffList; set { _StaffList = value; OnPropertyChanged(); } }
        private DOIBONG _Team;
        public DOIBONG Team { get => _Team; set { _Team = value; OnPropertyChanged(); } }

        private List<CAUTHU> _MainPlayers;
        public List<CAUTHU> MainPlayers { get => _MainPlayers; set { _MainPlayers = value; OnPropertyChanged(); } }


        public BasicTeam Teamformat;
        public ClubInfomationViewModel(DOIBONG teams)
        {
            PlayerList = new ObservableCollection<CAUTHU>(DataProvider.ins.DB.CAUTHUs.Where(p => p.IDDOIBONG == teams.ID));
            StaffList = new ObservableCollection<HUANLUYENVIEN>(DataProvider.ins.DB.HUANLUYENVIENs.Where(p => p.IDDOIBONG == teams.ID));
            Team = teams;
            MainPlayers = (from a in DataProvider.ins.DB.CAUTHUs
                                join b in DataProvider.ins.DB.DOIHINHCHINHs on a.ID equals b.IDCAUTHU
                                where b.IDDOIBONG == teams.ID
                                select (a)).ToList<CAUTHU>();
            if (teams.SODOCHIENTHUAT=="4-3-3")
            {
                Teamformat = new Team_433(MainPlayers,1);
                MainPlayers = Teamformat.team;
            }
            else if(teams.SODOCHIENTHUAT == "4-4-2")
            {
                Teamformat = new Team_442(MainPlayers,1);
                MainPlayers = Teamformat.team;
            }
            else
            {
                Teamformat = new Team_4231(MainPlayers,1);
                MainPlayers = Teamformat.team;

            }
        }
        public ClubInfomationViewModel()
        {

        }

    }
}
