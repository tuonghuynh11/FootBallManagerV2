using FootBallProject.Class.Format_team;
using FootBallProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.ViewModel
{
    public class MatchInformationViewModel:BaseViewModel
    {
        private DOIBONG _Team1;
        public DOIBONG Team1 { get => _Team1; set { _Team1 = value; OnPropertyChanged(); } }

        private List<CAUTHU> _MainPlayers1;
        public List<CAUTHU> MainPlayers1 { get => _MainPlayers1; set { _MainPlayers1 = value; OnPropertyChanged(); } }


        public BasicTeam Teamformat1;

        private DOIBONG _Team2;
        public DOIBONG Team2 { get => _Team2; set { _Team2 = value; OnPropertyChanged(); } }

        private List<CAUTHU> _MainPlayers2;
        public List<CAUTHU> MainPlayers2 { get => _MainPlayers2; set { _MainPlayers2 = value; OnPropertyChanged(); } }


        public BasicTeam Teamformat2;
        public MatchInformationViewModel() { }

        public MatchInformationViewModel(DOIBONG team1, DOIBONG team2)
        {
            Team1 = team1;
            MainPlayers1 = (from a in DataProvider.ins.DB.CAUTHUs
                           join b in DataProvider.ins.DB.DOIHINHCHINHs on a.ID equals b.IDCAUTHU
                           where b.IDDOIBONG == team1.ID
                           select (a)).ToList<CAUTHU>();
            if (team1.SODOCHIENTHUAT == "4-3-3")
            {
                Teamformat1 = new Team_433(MainPlayers1, 1);
                MainPlayers1 = Teamformat1.team;
            }
            else if (team1.SODOCHIENTHUAT == "4-4-2")
            {
                Teamformat1 = new Team_442(MainPlayers1, 1);
                MainPlayers1 = Teamformat1.team;
            }
            else
            {
                Teamformat1 = new Team_4231(MainPlayers1, 1);
                MainPlayers1 = Teamformat1.team;

            }

            Team2 = team2;
            MainPlayers2 = (from a in DataProvider.ins.DB.CAUTHUs
                            join b in DataProvider.ins.DB.DOIHINHCHINHs on a.ID equals b.IDCAUTHU
                            where b.IDDOIBONG == team2.ID
                            select (a)).ToList<CAUTHU>();
            if (team2.SODOCHIENTHUAT == "4-3-3")
            {
                Teamformat2 = new Team_433(MainPlayers2, 1);
                MainPlayers2 = Teamformat2.team;
            }
            else if (team2.SODOCHIENTHUAT == "4-4-2")
            {
                Teamformat2 = new Team_442(MainPlayers2, 1);
                MainPlayers2 = Teamformat2.team;
            }
            else
            {
                Teamformat2 = new Team_4231(MainPlayers2, 1);
                MainPlayers2 = Teamformat2.team;

            }

        }

    }
}
