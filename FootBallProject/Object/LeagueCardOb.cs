using FootBallProject.Class;
using FootBallProject.Model;
using FootBallProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.Object
{
    public class LeagueCardOb : BaseViewModel
    {
        private string _displayName;
        private string _startTime;
        private string _endTime;
        private int _soDoi;
        private LEAGUE league;
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; OnPropertyChanged(); }
        }
        public LEAGUE League
        {
            get { return league; }
            set { league = value; OnPropertyChanged();}
        }
        public string StartTime
        {
            get { return _startTime; }
            set { _startTime = value; OnPropertyChanged(); }
        }
        public string EndTime
        {
            get { return _endTime; }
            set { _endTime = value; OnPropertyChanged(); }
        }
        public int SoDoi
        {
            get { return _soDoi; }
            set { _soDoi = value; OnPropertyChanged(); }
        }
        private QUOCTICH quocTich;
        public QUOCTICH QuocTich
        {
            get { return quocTich; }
            set { quocTich = value; OnPropertyChanged(); }
        }
        private DIADIEM diadiem;
        public DIADIEM Diadiem
        {
            get { return diadiem; }
            set { diadiem = value; OnPropertyChanged(); }
        }
        public ObservableCollection<string> soluongdois = new ObservableCollection<string>() { "4", "8", "16" };
        private string selectedSoluong;
        public string SelectedSoluong
        {
            get { return selectedSoluong; }
            set { selectedSoluong = value; OnPropertyChanged(); }
        }
        private bool enable;
        public bool Enable
        {
            get { return enable; }
            set { enable = value; OnPropertyChanged(); }
        }
        public LeagueCardOb(LEAGUE p)
        {
            DisplayName = p.TENGIAIDAU;
            QuocTich = p.QUOCTICH;
            StartTime = p.NGAYBATDAU.ToString().Split(' ')[0];
            EndTime = p.NGAYKETTHUC.ToString().Split(' ')[0];
            SoDoi = DataProvider.Instance.Database.TEAMOFLEAGUEs.Where(x => x.IDGIAIDAU == p.ID).Count();
            League = p;
            //if (AccessUser.userLogin.USERROLE.ID == 2)
            //{
            //    Enable = true;
            //}
            //else Enable = false;
        }
        public void SaveLeague()
        {
            if (DisplayName != "" && QuocTich != null)
            {
                League.TENGIAIDAU = DisplayName;
                League.QUOCTICH = QuocTich;
                DataProvider.ins.Database.LEAGUEs.AddOrUpdate(League);
                DataProvider.ins.Database.SaveChanges();
            }
        }
    }

}
