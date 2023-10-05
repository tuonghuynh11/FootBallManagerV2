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
    public class Team_433 : BasicTeam
    {
        
        public Team_433() { }

        public Team_433(List<CAUTHU> players)
        {
            this.ST = new BitmapImage();
            this.LW = new BitmapImage();
            this.RW = new BitmapImage();
            this.LM = new BitmapImage();
            this.CM = new BitmapImage();
            this.RM = new BitmapImage();
            this.LB = new BitmapImage();
            this.CB1 = new BitmapImage();
            this.CB2 = new BitmapImage();
            this.RB = new BitmapImage();
            this.GK = new BitmapImage();
            this.team = new List<CAUTHU>();
            foreach (CAUTHU p in players)
            {
                if (p.VITRI == "ST")
                {
                    if (this.ST.CacheOption == BitmapCacheOption.Default)
                    {
                        this.ST = LoadImage(p.HINHANH);
                        p.VITRIAO = "ST";
                        p.STT = 1;
                        team.Add(p);

                    }
                }

                else if (p.VITRI == "RW")
                {
                    if (this.RW.CacheOption == BitmapCacheOption.Default)
                    {
                        this.RW = LoadImage(p.HINHANH);
                        p.VITRIAO = "RW";
                        p.STT = 2;
                        team.Add(p);

                    }

                }
                else if (p.VITRI == "LW")
                {
                    if (this.LW.CacheOption == BitmapCacheOption.Default)
                    {
                        this.LW = LoadImage(p.HINHANH);
                        p.VITRIAO = "LW";
                        p.STT = 3;
                        team.Add(p);

                    }

                }

                else if ((p.VITRI == "CM" || p.VITRI == "CDM" )&&(this.CM.CacheOption == BitmapCacheOption.Default))
                {
                    if (this.CM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.CM = LoadImage(p.HINHANH);
                        p.VITRIAO = "CM";
                        p.STT = 5;
                        team.Add(p);


                    }
                }
                else if ((p.VITRI == "RM"|| p.VITRI == "CM"|| p.VITRI == "CDM")&&(this.RM.CacheOption == BitmapCacheOption.Default))
                {
                    if (this.RM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.RM = LoadImage(p.HINHANH);
                        p.VITRIAO = "RM";
                        p.STT = 6;
                        team.Add(p);

                    }
                }
                else if (p.VITRI == "LM" || p.VITRI == "CM" || p.VITRI == "CDM")
                {
                    if (this.LM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.LM = LoadImage(p.HINHANH);
                        p.VITRIAO = "LM";
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
                else if (p.VITRI == "CB" || p.VITRI == "CB1" || p.VITRI == "CB2")
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
                        p.STT = 9;
                        this.CB2 = LoadImage(p.HINHANH);
                        p.VITRIAO = "CB";
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
        public string GETVITRIAO(int idcauthu)
        {
            
            DOIHINHCHINH qt = DataProvider.ins.DB.Database.SqlQuery<DOIHINHCHINH>("SELECT * FROM DOIHINHCHINH WHERE IDCAUTHU = @ID ", new SqlParameter("@ID", idcauthu)).FirstOrDefault<DOIHINHCHINH>();

            return qt.VITRI!=null?qt.VITRI:"";
        }
        public Team_433(List<CAUTHU> players,int i)
        {
            this.ST = new BitmapImage();
            this.LW = new BitmapImage();
            this.RW = new BitmapImage();
            this.LM = new BitmapImage();
            this.CM = new BitmapImage();
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
                    if (this.ST.CacheOption == BitmapCacheOption.Default)
                    {
                        this.ST = LoadImage(p.HINHANH);
                        p.VITRIAO = "ST";
                        p.STT = 1;
                        team.Add(p);

                    }
                }

                else if (GETVITRIAO(p.ID) == "RW" || GETVITRIAO(p.ID) == "RS")
                {
                    if (this.RW.CacheOption == BitmapCacheOption.Default)
                    {
                        this.RW = LoadImage(p.HINHANH);
                        p.VITRIAO = "RW";
                        p.STT = 2;
                        team.Add(p);

                    }

                }
                else if (GETVITRIAO(p.ID) == "LW" || GETVITRIAO(p.ID) == "RCM")
                {
                    if (this.LW.CacheOption == BitmapCacheOption.Default)
                    {
                        this.LW = LoadImage(p.HINHANH);
                        p.VITRIAO = "LW";
                        p.STT = 3;
                        team.Add(p);

                    }

                }

                else if ((GETVITRIAO(p.ID) == "CM" || GETVITRIAO(p.ID) == "CDM" || GETVITRIAO(p.ID) == "LCM")&&( (this.CM.CacheOption == BitmapCacheOption.Default)))
                {
                    if (this.CM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.CM = LoadImage(p.HINHANH);
                        p.VITRIAO = "CM";
                        p.STT = 5;
                        team.Add(p);


                    }
                }
                else if ((GETVITRIAO(p.ID) == "RM" || GETVITRIAO(p.ID) == "CM" || GETVITRIAO(p.ID) == "CDM")&& (this.RM.CacheOption == BitmapCacheOption.Default))
                {
                    if (this.RM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.RM = LoadImage(p.HINHANH);
                        p.VITRIAO = "RM";
                        p.STT = 6;
                        team.Add(p);

                    }
                }
                else if (GETVITRIAO(p.ID) == "LM" || GETVITRIAO(p.ID) == "CM" || GETVITRIAO(p.ID) == "CDM")
                {
                    if (this.LM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.LM = LoadImage(p.HINHANH);
                        p.VITRIAO = "LM";
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
                else if (GETVITRIAO(p.ID) == "CB" || GETVITRIAO(p.ID) == "CB1" || GETVITRIAO(p.ID) == "CB2")
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
                        p.STT = 9;
                        this.CB2 = LoadImage(p.HINHANH);
                        p.VITRIAO = "CB";
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

        /// <summary>
        /// Đội hình chiến thuật basic
        /// </summary>
        /// <param name="players"></param>
        /// <param name="format"></param>
        public Team_433(List<CAUTHU> players,string format)
        {
            this.ST = new BitmapImage();
            this.LW = new BitmapImage();
            this.RW = new BitmapImage();
            this.LM = new BitmapImage();
            this.CM = new BitmapImage();
            this.RM = new BitmapImage();
            this.LB = new BitmapImage();
            this.CB1 = new BitmapImage();
            this.CB2 = new BitmapImage();
            this.RB = new BitmapImage();
            this.GK = new BitmapImage();
            this.team = new List<CAUTHU>();
            foreach (CAUTHU p in players)
            {
                if (p.VITRIAO == "ST"||p.VITRIAO == "LS")
                {
                    if (this.ST.CacheOption == BitmapCacheOption.Default)
                    {
                        this.ST = LoadImage(p.HINHANH);
                        p.VITRIAO = "ST";
                        p.STT = 1;
                        team.Add(p);

                    }
                }

                else if (p.VITRIAO == "RW" || p.VITRIAO == "RS")
                {
                    if (this.RW.CacheOption == BitmapCacheOption.Default)
                    {
                        this.RW = LoadImage(p.HINHANH);
                        p.VITRIAO = "RW";
                        p.STT = 2;
                        team.Add(p);

                    }

                }
                else if (p.VITRIAO == "LW" || p.VITRIAO == "RCM")
                {
                    if (this.LW.CacheOption == BitmapCacheOption.Default)
                    {
                        this.LW = LoadImage(p.HINHANH);
                        p.VITRIAO = "LW";
                        p.STT = 3;
                        team.Add(p);

                    }

                }

                else if ((p.VITRIAO == "CM" || p.VITRIAO == "CDM" || p.VITRIAO == "LCM") && ((this.CM.CacheOption == BitmapCacheOption.Default)))
                {
                    if (this.CM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.CM = LoadImage(p.HINHANH);
                        p.VITRIAO = "CM";
                        p.STT = 5;
                        team.Add(p);


                    }
                }
                else if ((p.VITRIAO == "RM" || p.VITRIAO == "CM" || p.VITRIAO == "CDM") && (this.RM.CacheOption == BitmapCacheOption.Default))
                {
                    if (this.RM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.RM = LoadImage(p.HINHANH);
                        p.VITRIAO = "RM";
                        p.STT = 6;
                        team.Add(p);

                    }
                }
                else if (p.VITRIAO == "LM" || p.VITRIAO == "CM" || p.VITRIAO == "CDM")
                {
                    if (this.LM.CacheOption == BitmapCacheOption.Default)
                    {
                        this.LM = LoadImage(p.HINHANH);
                        p.VITRIAO = "LM";
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
                else if (p.VITRIAO == "CB" || p.VITRIAO == "CB1" || p.VITRIAO == "CB2")
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
                        p.STT = 9;
                        this.CB2 = LoadImage(p.HINHANH);
                        p.VITRIAO = "CB";
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
