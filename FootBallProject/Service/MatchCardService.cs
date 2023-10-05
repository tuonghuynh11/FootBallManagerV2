using FootBallProject.Model;
using FootBallProject.Object;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.Service
{
    public class MatchCardService
    {
        private static MatchCardService s_instance;
        public static MatchCardService Instance => s_instance ?? (s_instance = new MatchCardService());
        public MatchCardService() { }
        public DbSet<FOOTBALLMATCH> LoadFootballMatch()
        {
            return DataProvider.Instance.Database.FOOTBALLMATCHes;
        }
        public void ConvertFootballMatchCardToFootballMatch(FootballMatchCard p)
        {
            try
            {
                FOOTBALLMATCH footballMatch = new FOOTBALLMATCH()
                {
                    ID = p.ID,
                    THOIGIAN = p.DisplayDay,
                    TENTRANDAU = p.DisplayName,
                };
                DataProvider.Instance.Database.FOOTBALLMATCHes.AddOrUpdate();
                DataProvider.Instance.Database.SaveChanges();
            }
            catch { }

        }
        public void AddMatchTeamInfo1(FootballMatchCard p)
        {
            int x = Convert.ToInt16(p.ScoreTeamA) - Convert.ToInt16(p.ScoreTeamB);
            int ketquaA, ketquaB;
            if (x == 0)
            {
                ketquaA = ketquaB = 2;
            }
            else if (x > 0)
            {
                ketquaA = 1;
                ketquaB = 0;
            }
            else
            {
                ketquaA = 0;
                ketquaB = 1;
            }

            THONGTINTRANDAU teamA = new THONGTINTRANDAU()
            {
                IDTRANDAU = p.ID,
                IDDOIBONG = p.TeamA.ID,
                DIEM = Convert.ToInt32(p.ScoreTeamA),
                KETQUA = ketquaA
            };
            THONGTINTRANDAU teamB = new THONGTINTRANDAU()
            {
                IDTRANDAU = p.ID,
                IDDOIBONG = p.TeamB.ID,
                DIEM = Convert.ToInt32(p.ScoreTeamB),
                KETQUA = ketquaB
            };
            DataProvider.Instance.Database.THONGTINTRANDAUs.AddOrUpdate(teamA, teamB);
            DataProvider.Instance.Database.SaveChanges();
        }
    }

}
