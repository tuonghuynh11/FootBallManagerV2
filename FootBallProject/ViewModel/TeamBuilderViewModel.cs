using FootBallProject.Class.Format_team;
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
    public class TeamBuilderViewModel:BaseViewModel
    {
        private List<CAUTHU> _MainTeamPlayers;
        public List<CAUTHU> MainTeamPlayers { get => _MainTeamPlayers; set { _MainTeamPlayers = value; OnPropertyChanged("MainTeamPlayers"); } }

        private List<CAUTHU> _SubTeamPlayers;
        public List<CAUTHU> SubTeamPlayers { get => _SubTeamPlayers; set { _SubTeamPlayers = value; OnPropertyChanged("SubTeamPlayers"); } }

        private BasicTeam _Teamformat;
        public BasicTeam Teamformat { get => _Teamformat; set { _Teamformat = value; OnPropertyChanged(); } }
        public HUANLUYENVIEN Coach { get; set; }

        private DOIBONG _Team;
        public DOIBONG Team { get => _Team; set { _Team = value; OnPropertyChanged(); } }
        public TeamBuilderViewModel()
        {
            //Load đội hình của đội có id cho trước, test trước
            //var temp = (from a in DataProvider.ins.DB.CAUTHUs
            //            join b in DataProvider.ins.DB.DOIHINHCHINHs on a.ID equals b.IDCAUTHU
            //            where b.IDDOIBONG == "mc"
            //            select (a)).ToList<CAUTHU>();
            //if (temp.Count!=0||temp!=null)
            //{
            //    LoadMainteam("mc");

            //}
          
         }

        //Load đội hình của đội có id cho trước
        public TeamBuilderViewModel(string id_doi)
        {
            //Load đội hình của đội có id cho trước
            var temp = (from a in DataProvider.ins.DB.CAUTHUs
                        join b in DataProvider.ins.DB.DOIHINHCHINHs on a.ID equals b.IDCAUTHU
                        where b.IDDOIBONG == id_doi
                        select (a)).ToList<CAUTHU>();
            Coach = DataProvider.ins.DB.HUANLUYENVIENs.Where(x => x.IDDOIBONG == id_doi && x.CHUCVU == "HLV Trưởng").FirstOrDefault();
            if (temp !=null)
            {
                if (temp.Count != 0)
                {
                    LoadMainteam(id_doi);

                }
                else
                {
                    MainTeamPlayers = DataProvider.ins.DB.CAUTHUs.Where(p=>p.IDDOIBONG==id_doi).ToList();
                }
            }
            

        }

        void LoadMainteam(string id_doi)
        {
            
            var mainteam = (from a in DataProvider.ins.DB.CAUTHUs
                     join b in DataProvider.ins.DB.DOIHINHCHINHs on a.ID equals b.IDCAUTHU
                     where b.IDDOIBONG == id_doi
                     select (a) ).ToList<CAUTHU>();

            var sub = DataProvider.ins.DB.Database.SqlQuery<CAUTHU>("SELECT * FROM CAUTHU WHERE IDDOIBONG=@ID1 AND ID NOT IN (SELECT IDCAUTHU FROM DOIHINHCHINH WHERE IDDOIBONG = @ID1 ) ", new SqlParameter("@ID1", id_doi)).ToList<CAUTHU>();
            SubTeamPlayers = sub;

            DOIBONG dOIBONG = DataProvider.ins.DB.DOIBONGs.Find(id_doi);
            Team = dOIBONG;
            if (dOIBONG.SODOCHIENTHUAT=="4-3-3")
            {
                Teamformat = new Team_433(mainteam,1);
                MainTeamPlayers = Teamformat.team;

            }
            else if (dOIBONG.SODOCHIENTHUAT == "4-4-2")
            {
                Teamformat = new Team_442(mainteam, 1);
                MainTeamPlayers = Teamformat.team;
            }
            else
            {
                Teamformat = new Team_4231(mainteam, 1);
                MainTeamPlayers = Teamformat.team;
            }

        }

      
    }
}
