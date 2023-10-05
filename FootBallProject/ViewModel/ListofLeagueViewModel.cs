using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Editors.Helpers;
using FootBallProject.Class;
using FootBallProject.Model;
using FootBallProject.Object;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace FootBallProject.ViewModel
{
    public class ListofLeagueViewModel : BaseViewModel
    {
        private static ListofLeagueViewModel _instance;
        public static ListofLeagueViewModel Instance
        {
            get { return _instance; }
            set { _instance = value; }
        }
        private LeagueCardOb _currentleague;
        public LeagueCardOb Currentleague
        {
            get { return _currentleague; }
            set
            {
                _currentleague = value; OnPropertyChanged();
                LoadTeambyLeagueid();
            }
        }
        private ObservableCollection<LeagueCardOb> leagues;
        public ObservableCollection<LeagueCardOb> Leagues
        {
            get { return leagues; }
            set { leagues = value; OnPropertyChanged(); }
        }
        private ObservableCollection<TEAMOFLEAGUE> teams = new ObservableCollection<TEAMOFLEAGUE>();
        public ObservableCollection<TEAMOFLEAGUE> Teams
        {
            get { return teams; }
            set { teams = value; OnPropertyChanged(); }
        }
        private ObservableCollection<RoundObject> _roundlist = new ObservableCollection<RoundObject>();
        public ObservableCollection<RoundObject> RoundList
        {
            get { return _roundlist; }
            set
            {
                _roundlist = value;
                OnPropertyChanged();
            }
        }
        public ICommand AddLeague { get; set; }
        private void LoadTeambyLeagueid()
        {
            CurrentAhihi = configAutoViewModel;
            teams.Clear();
            List<TEAMOFLEAGUE> list = DataProvider.Instance.Database.TEAMOFLEAGUEs.Where(x => x.IDGIAIDAU == Currentleague.League.ID).ToList();
            foreach (var item in list)
            {
                teams.Add(item);
            }
            Teams = teams;
            var list1 = DataProvider.ins.DB.ROUNDs.Where(x => x.IDGIAIDAU == Currentleague.League.ID).ToList();
            _roundlist.Clear();
            foreach (var item in list1)
            {
                _roundlist.Add(new RoundObject(item));
            }
            RoundList = _roundlist;
            CurrentAhihi = new ConfigAutoViewModel(this);
        }
        private object currentAhihi;
        public object CurrentAhihi
        {
            get => currentAhihi;
            set { currentAhihi = value; OnPropertyChanged(); }
        }
        private bool _createLeagueButton;
        public bool CreateLeagueButton
        {
            get => _createLeagueButton;
            set { _createLeagueButton = value; OnPropertyChanged(); }
        }
        public ICommand AddtemofLeague { get; set; }
        public object configAutoViewModel;
        public object config1;
        public object config2;
        public object createnewleague;
        public LeagueCardOb first;
        public ListofLeagueViewModel()
        {
            Instance = this;
            ObservableCollection<LeagueCardOb> list3 = new ObservableCollection<LeagueCardOb>();
            List<LEAGUE> list1 = DataProvider.Instance.Database.LEAGUEs.Where(x => x.NGAYBATDAU!= null).ToList();
            foreach (var item in list1)
            {
                list3.Add(new LeagueCardOb(item));
            }
            Leagues = list3;
            Currentleague = Leagues.First();
            config1 = new ConfigVongLoai1ViewModel(this);
            createnewleague = new CreateNewLeague();
            CurrentAhihi = new ConfigAutoViewModel(this);
            AddLeague = new RelayCommand<object>((p) => { return true; }, (p) => { AddLeagueFuntion(); });
            CheckVisibility();
        }
        public void UpdateLeagues()
        {
            ObservableCollection<LeagueCardOb> list3 = new ObservableCollection<LeagueCardOb>();
            List<LEAGUE> list1 = DataProvider.Instance.Database.LEAGUEs.Where(x => x.NGAYBATDAU != null).ToList();
            foreach (var item in list1)
            {
                list3.Add(new LeagueCardOb(item));
            }
            Leagues = list3;
            Currentleague = Leagues.First();
            CurrentAhihi = new ConfigAutoViewModel(this);
        }
        private void CheckVisibility()
        {
            if (AccessUser.userLogin.USERROLE.ID == 2)
            {
                CreateLeagueButton = true;
            }
            else CreateLeagueButton = false;
        }
        public void Refresh()
        {
            UpdateLeagues();
        }
        public void AddLeagueFuntion()
        {
            createnewleague = new CreateNewLeague();
            config1 = new ConfigVongLoai1ViewModel(this);
            CurrentAhihi = createnewleague;
        }
        public void ContinueFuntion()
        {
            config2 = new ConfigVongLoai2ViewModel(this);
            CurrentAhihi = config2;
        }
        public void GoSuccess()
        {
            CurrentAhihi = new ConfigThanhCong("Thêm giải đấu thành công, vui lòng chọn thông tin để hiển thị");
        }
        public void GoNext()
        {
            config1 = new ConfigVongLoai1ViewModel(this);
            CurrentAhihi = config1;
        }
        public void Return()
        {
            CurrentAhihi = new ConfigAutoViewModel(this);
        }
        public void ReturnConfig1()
        {
            if (config1 == null)  config1 = new ConfigVongLoai1ViewModel(this);
            CurrentAhihi = config1;
        }
        public void ReturnCreate()
        {
            CurrentAhihi = createnewleague;
        }
    }
}
