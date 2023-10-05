using FootBallProject.Model;
using FootBallProject.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.Object
{
    public class TeamItem : BaseViewModel
    {
        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set { selected = value; OnPropertyChanged();
                ConfigVongLoai1ViewModel.Instance.CountTotalTeam();
            }
        }
        private DOIBONG team;
        public  DOIBONG Team{
            get { return team; }
            set { team = value;}
        }
        private string displayName;
        public string DisplayName        {
            get { return displayName; }
            set
            {
                displayName = value;
            }   
        }
        private string hlvName;
        public string HlvName
        {
            get { return hlvName; }
            set
            {
                hlvName = value;
            }   
        }
        private string soThanhVien;
        public string SoThanhVien
        {
            get { return soThanhVien; }
            set { soThanhVien= value;
                 } 
        }
        private string soGiaiDau;
        public string SoGiaiDau
        {
            get { return soGiaiDau;}
            set { soGiaiDau= value;}
        }
        public TeamItem(DOIBONG item)
        {
            Selected = false;
            Team = item;
            DisplayName = item.TEN;
            HlvName = item.HUANLUYENVIENs.ToList().FirstOrDefault().HOTEN;
            SoThanhVien = DataProvider.Instance.Database.CAUTHUs.Where(x=>x.IDDOIBONG == item.ID).ToList().Count.ToString();
            SoGiaiDau = DataProvider.Instance.Database.LEAGUEs.ToList().Count.ToString();
        }
    }
}
