using FootBallProject.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FootBallProject.Class.Format_team
{
    public  class Team_442:BasicTeam
    {
       
        public Team_442() { }
        public string GETVITRIAO(int idcauthu)
        {

            DOIHINHCHINH qt = DataProvider.ins.DB.Database.SqlQuery<DOIHINHCHINH>("SELECT * FROM DOIHINHCHINH WHERE IDCAUTHU = @ID ", new SqlParameter("@ID", idcauthu)).FirstOrDefault<DOIHINHCHINH>();

            return qt.VITRI;
        }
        public Team_442(List<CAUTHU> players, int i)
        {
            this.LS = new BitmapImage();
            this.RS = new BitmapImage();
            this.LM = new BitmapImage();
            this.LCM = new BitmapImage();
            this.RCM = new BitmapImage();
            this.RM = new BitmapImage();
            this.LB = new BitmapImage();
            this.CB1 = new BitmapImage();
            this.CB2 = new BitmapImage();
            this.RB = new BitmapImage();
            this.GK = new BitmapImage();
            this.team = new List<CAUTHU>();
            foreach (CAUTHU p in players)
            {
                if (GETVITRIAO(p.ID) == "ST" || GETVITRIAO(p.ID) == "LS")
                {
                    if (this.LS.CacheOption == BitmapCacheOption.Default)
                    {
                        this.LS = LoadImage(p.HINHANH);
                        p.VITRIAO = "LS";
                        p.STT = 1;
                        team.Add(p);

                    }
                }

                else if (GETVITRIAO(p.ID) == "RW" || GETVITRIAO(p.ID) == "RS")
                {
                    if (this.RS.CacheOption == BitmapCacheOption.Default)
                    {
                        this.RS = LoadImage(p.HINHANH);
                        p.VITRIAO = "RS";
                        p.STT = 2;
                        team.Add(p);

                    }

                }
                else if ((GETVITRIAO(p.ID) == "LW" || GETVITRIAO(p.ID) == "LM") && this.LM.CacheOption == BitmapCacheOption.Default)
                {
                    if (this.LM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.LM = LoadImage(p.HINHANH);
                        p.VITRIAO = "LM";
                        p.STT = 3;
                        team.Add(p);

                    }

                }

                else if (GETVITRIAO(p.ID) == "CM" || GETVITRIAO(p.ID) == "CDM" || GETVITRIAO(p.ID) == "LCM")
                {
                    if (this.LCM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.LCM = LoadImage(p.HINHANH);
                        p.VITRIAO = "LCM";
                        p.STT = 5;
                        team.Add(p);


                    }
                }
                else if (GETVITRIAO(p.ID) == "RM")
                {
                    if (this.RM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.RM = LoadImage(p.HINHANH);
                        p.VITRIAO = "RM";
                        p.STT = 6;
                        team.Add(p);

                    }
                }
                else if (GETVITRIAO(p.ID) == "LM" || GETVITRIAO(p.ID) == "RCM" || GETVITRIAO(p.ID) == "LW")
                {
                    if (this.RCM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.RCM = LoadImage(p.HINHANH);
                        p.VITRIAO = "RCM";
                        p.STT = 4;
                        team.Add(p);

                    }
                }
                else if (GETVITRIAO(p.ID) == "LB")
                {
                    if (this.LB.CacheOption == BitmapCacheOption.Default)
                    {
                        this.LB = LoadImage(p.HINHANH);
                        p.VITRIAO = "LB";
                        p.STT = 7;
                        team.Add(p);

                    }
                }
                else if (GETVITRIAO(p.ID) == "CB")
                {
                    if (this.CB1.CacheOption == BitmapCacheOption.Default)
                    {
                        this.CB1 = LoadImage(p.HINHANH);
                        p.VITRIAO = "CB";
                        p.STT = 8;
                        team.Add(p);

                    }
                    else
                    {
                        this.CB2 = LoadImage(p.HINHANH);
                        p.VITRIAO = "CB";
                        p.STT = 9;
                        team.Add(p);

                    }
                }
                else if (GETVITRIAO(p.ID) == "RB")
                {
                    if (this.RB.CacheOption == BitmapCacheOption.Default)
                    {
                        this.RB = LoadImage(p.HINHANH);
                        p.VITRIAO = "RB";
                        p.STT = 10;
                        team.Add(p);

                    }
                }
                else if (GETVITRIAO(p.ID) == "GK")
                {
                    if (this.GK.CacheOption == BitmapCacheOption.Default)
                    {
                        this.GK = LoadImage(p.HINHANH);
                        p.VITRIAO = "GK";
                        p.STT = 11;
                        team.Add(p);

                    }
                }

            }
            team.Sort((a, b) => a.STT.CompareTo(b.STT));
        }

        public Team_442(List<CAUTHU> players)
        {
            this.LS = new BitmapImage();
            this.RS = new BitmapImage();
            this.LM = new BitmapImage();
            this.LCM = new BitmapImage();
            this.RCM = new BitmapImage();
            this.RM = new BitmapImage();
            this.LB = new BitmapImage();
            this.CB1 = new BitmapImage();
            this.CB2 = new BitmapImage();
            this.RB = new BitmapImage();
            this.GK = new BitmapImage();
            this.team = new List<CAUTHU>();
            foreach (CAUTHU p in players)
            {
                if (p.VITRI == "ST" || p.VITRI == "LS")
                {
                    if (this.LS.CacheOption == BitmapCacheOption.Default)
                    {
                        this.LS = LoadImage(p.HINHANH);
                        p.VITRIAO = "LS";
                        p.STT = 1;
                        team.Add(p);

                    }
                }

                else if (p.VITRI == "RW" || p.VITRI == "RS")
                {
                    if (this.RS.CacheOption == BitmapCacheOption.Default)
                    {
                        this.RS = LoadImage(p.HINHANH);
                        p.VITRIAO = "RS";
                        p.STT = 2;
                        team.Add(p);

                    }

                }
                else if ((p.VITRI == "LW" || p.VITRI == "LM") && this.LM.CacheOption == BitmapCacheOption.Default)
                {
                    if (this.LM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.LM = LoadImage(p.HINHANH);
                        p.VITRIAO = "LM";
                        p.STT = 3;
                        team.Add(p);

                    }

                }

                else if ((p.VITRI == "CM" || p.VITRI == "CDM" || p.VITRI == "LCM")&&(this.LCM.CacheOption == BitmapCacheOption.Default))
                {
                    if (this.LCM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.LCM = LoadImage(p.HINHANH);
                        p.VITRIAO = "LCM";
                        p.STT = 5;
                        team.Add(p);


                    }
                }
                else if ((p.VITRI == "RM"||p.VITRI == "CM" || p.VITRI == "CDM" || p.VITRI == "LCM")&& (this.RM.CacheOption == BitmapCacheOption.Default))
                {
                    if (this.RM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.RM = LoadImage(p.HINHANH);
                        p.VITRIAO = "RM";
                        p.STT = 6;
                        team.Add(p);

                    }
                }
                else if (p.VITRI == "LM" || p.VITRI == "RCM" || p.VITRI == "LW")
                {
                    if (this.RCM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.RCM = LoadImage(p.HINHANH);
                        p.VITRIAO = "RCM";
                        p.STT = 4;
                        team.Add(p);

                    }
                }
                else if (p.VITRI == "LB")
                {
                    if (this.LB.CacheOption == BitmapCacheOption.Default)
                    {
                        this.LB = LoadImage(p.HINHANH);
                        p.VITRIAO = "LB";
                        p.STT = 7;
                        team.Add(p);

                    }
                }
                else if (p.VITRI == "CB")
                {
                    if (this.CB1.CacheOption == BitmapCacheOption.Default)
                    {
                        this.CB1 = LoadImage(p.HINHANH);
                        p.VITRIAO = "CB";
                        p.STT = 8;
                        team.Add(p);

                    }
                    else
                    {
                        this.CB2 = LoadImage(p.HINHANH);
                        p.VITRIAO = "CB";
                        p.STT = 9;
                        team.Add(p);

                    }
                }
                else if (p.VITRI == "RB")
                {
                    if (this.RB.CacheOption == BitmapCacheOption.Default)
                    {
                        this.RB = LoadImage(p.HINHANH);
                        p.VITRIAO = "RB";
                        p.STT = 10;
                        team.Add(p);

                    }
                }
                else if (p.VITRI == "GK")
                {
                    if (this.GK.CacheOption == BitmapCacheOption.Default)
                    {
                        this.GK = LoadImage(p.HINHANH);
                        p.VITRIAO = "GK";
                        p.STT = 11;
                        team.Add(p);

                    }
                }

            }
            team.Sort((a, b) => a.STT.CompareTo(b.STT));
        }


        /// <summary>
        /// Đội hình chiến thuật basic
        /// </summary>
        /// <param name="players"></param>
        /// <param name="format"></param>
        public Team_442(List<CAUTHU> players, string format)
        {
            this.LS = new BitmapImage();
            this.RS = new BitmapImage();
            this.LM = new BitmapImage();
            this.LCM = new BitmapImage();
            this.RCM = new BitmapImage();
            this.RM = new BitmapImage();
            this.LB = new BitmapImage();
            this.CB1 = new BitmapImage();
            this.CB2 = new BitmapImage();
            this.RB = new BitmapImage();
            this.GK = new BitmapImage();
            this.team = new List<CAUTHU>();
            foreach (CAUTHU p in players)
            {
                if (p.VITRIAO == "ST" || p.VITRIAO == "LS")
                {
                    if (this.LS.CacheOption == BitmapCacheOption.Default)
                    {
                        this.LS = LoadImage(p.HINHANH);
                        p.VITRIAO = "LS";
                        p.STT = 1;
                        team.Add(p);

                    }
                }

                else if (p.VITRIAO == "RW" || p.VITRIAO == "RS")
                {
                    if (this.RS.CacheOption == BitmapCacheOption.Default)
                    {
                        this.RS = LoadImage(p.HINHANH);
                        p.VITRIAO = "RS";
                        p.STT = 2;
                        team.Add(p);

                    }

                }
                else if ((p.VITRIAO == "LW" || p.VITRIAO == "LM") && this.LM.CacheOption == BitmapCacheOption.Default)
                {
                    if (this.LM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.LM = LoadImage(p.HINHANH);
                        p.VITRIAO = "LM";
                        p.STT = 3;
                        team.Add(p);

                    }

                }

                else if (p.VITRIAO == "CM" || p.VITRIAO == "CDM" || p.VITRIAO == "LCM")
                {
                    if (this.LCM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.LCM = LoadImage(p.HINHANH);
                        p.VITRIAO = "LCM";
                        p.STT = 5;
                        team.Add(p);


                    }
                }
                else if (p.VITRIAO == "RM")
                {
                    if (this.RM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.RM = LoadImage(p.HINHANH);
                        p.VITRIAO = "RM";
                        p.STT = 6;
                        team.Add(p);

                    }
                }
                else if (p.VITRIAO == "LM" || p.VITRIAO == "RCM" || p.VITRIAO == "LW")
                {
                    if (this.RCM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.RCM = LoadImage(p.HINHANH);
                        p.VITRIAO = "RCM";
                        p.STT = 4;
                        team.Add(p);

                    }
                }
                else if (p.VITRIAO == "LB")
                {
                    if (this.LB.CacheOption == BitmapCacheOption.Default)
                    {
                        this.LB = LoadImage(p.HINHANH);
                        p.VITRIAO = "LB";
                        p.STT = 7;
                        team.Add(p);

                    }
                }
                else if (p.VITRIAO == "CB")
                {
                    if (this.CB1.CacheOption == BitmapCacheOption.Default)
                    {
                        this.CB1 = LoadImage(p.HINHANH);
                        p.VITRIAO = "CB";
                        p.STT = 8;
                        team.Add(p);

                    }
                    else
                    {
                        this.CB2 = LoadImage(p.HINHANH);
                        p.VITRIAO = "CB";
                        p.STT = 9;
                        team.Add(p);

                    }
                }
                else if (p.VITRIAO == "RB")
                {
                    if (this.RB.CacheOption == BitmapCacheOption.Default)
                    {
                        this.RB = LoadImage(p.HINHANH);
                        p.VITRIAO = "RB";
                        p.STT = 10;
                        team.Add(p);

                    }
                }
                else if (p.VITRIAO == "GK")
                {
                    if (this.GK.CacheOption == BitmapCacheOption.Default)
                    {
                        this.GK = LoadImage(p.HINHANH);
                        p.VITRIAO = "GK";
                        p.STT = 11;
                        team.Add(p);

                    }
                }

            }
            team.Sort((a, b) => a.STT.CompareTo(b.STT));
        }
    }
}
