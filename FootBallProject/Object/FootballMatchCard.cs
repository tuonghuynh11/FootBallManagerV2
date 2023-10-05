using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Editors.Helpers;
using FootBallProject.Class;
using FootBallProject.Model;
using FootBallProject.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FootBallProject.Object
{
    public class FootballMatchCard : BaseViewModel, IBaseCard, INotifyDataErrorInfo
    {

        #region error
        public bool HasErrors => _errorBaseViewModel.HasErrors;
        private readonly ErrorBaseViewModel _errorBaseViewModel;
        private void ErrorBaseViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorBaseViewModel.GetErrors(propertyName);
        }
        private bool IsValid(string propertyName)
        {
            return !string.IsNullOrEmpty(propertyName) && !string.IsNullOrWhiteSpace(propertyName);
        }
        #endregion
        private DateTime? _displayDay;
        private DIADIEM _displayPlace;
        private string _scoreTeamA;
        private string _scoreTeamB;
        private string _nameTeamA;
        private string _nameTeamB;
        private int _id;
        private DOIBONG _teamA = new DOIBONG();
        private DOIBONG _teamB = new DOIBONG();
        private THONGTINTRANDAU _infoTeamA = new THONGTINTRANDAU();
        private THONGTINTRANDAU _infoTeamB = new THONGTINTRANDAU();
        private FOOTBALLMATCH _footballMatch = new FOOTBALLMATCH();
        private string _displayName;
        private ObservableCollection<DOIBONG> _teamList;
        private bool _enable;
        private bool _isFirstRoundofLeague;
        public bool IsFirstRoundofLeague
        {
            get { return _isFirstRoundofLeague; }
            set { _isFirstRoundofLeague = value; OnPropertyChanged(); }
        }
        public ObservableCollection<DOIBONG> TeamList
        {
            get => _teamList;
            set { _teamList = value; }
        }

        private ObservableCollection<DOIBONG> _teamListDisPlayA;
        public ObservableCollection<DOIBONG> TeamListDisPlayA
        {
            get => _teamListDisPlayA;
            set
            {
                _teamListDisPlayA = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<DOIBONG> _teamListDisPlayB;
        public ObservableCollection<DOIBONG> TeamListDisPlayB
        {
            get => _teamListDisPlayB;
            set { _teamListDisPlayB = value; OnPropertyChanged(); }
        }
        private ObservableCollection<LEAGUE> _leagues;
        public ObservableCollection<LEAGUE> Leagues
        {
            get => _leagues;
            set { _leagues = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ROUND> _rounds;
        public ObservableCollection<ROUND> Rounds
        {
            get => _rounds;
            set { _rounds = value; OnPropertyChanged(); }
        }
        private ROUND _selectedRound;
        public ROUND SelectedRound
        {
            get => _selectedRound;
            set { _selectedRound = value; OnPropertyChanged(); }
        }
        private DateTime? chanTime;
        public DateTime? ChanTime {
            get => chanTime;
            set { chanTime = value; OnPropertyChanged(); }
   
        }
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                _errorBaseViewModel.ClearErrors();
                if (!IsValid(DisplayName))
                {
                    _errorBaseViewModel.AddError(nameof(DisplayName), "Vui lòng nhập tên trận đấu!");
                }
                OnPropertyChanged();
            }
        }
        public string ScoreTeamA
        {
            get => _scoreTeamA; set
            {
                _scoreTeamA = value;


                _errorBaseViewModel.ClearErrors();

                if (!IsValid(_scoreTeamA.ToString()))
                {
                    _errorBaseViewModel.AddError(nameof(_scoreTeamA), "Vui lòng chọn tỉ số!");
                }
            }
        }
        public string ScoreTeamB
        {
            get => _scoreTeamB; set
            {
                _scoreTeamB = value;

                // Validation
                _errorBaseViewModel.ClearErrors();

                if (!IsValid(_scoreTeamB.ToString()))
                {
                    _errorBaseViewModel.AddError(nameof(_scoreTeamB), "Vui lòng chọn tỉ số!");
                }
            }
        }
        public bool Enable
        {
            get => _enable;
            set { _enable = value; OnPropertyChanged(); }
        }
        public DIADIEM DisplayPlace
        {
            get => _displayPlace; set
            {
                _displayPlace = value;
                _errorBaseViewModel.ClearErrors();
                if (DisplayPlace == null)
                {
                    _errorBaseViewModel.AddError(nameof(_displayPlace), "Vui lòng chọn địa điểm!");
                }
                OnPropertyChanged();
            }
        }
        private ObservableCollection<DIADIEM> _displayplaces;
        public ObservableCollection<DIADIEM> DisplayPlaces
        {
            get => _displayplaces;
            set { _displayplaces = value; OnPropertyChanged(); }
        }
        public DateTime? DisplayDay
        {

            get => _displayDay;
            set
            {
                _displayDay = value;
                _errorBaseViewModel.ClearErrors();

                if (DisplayDay == null)
                {
                    _errorBaseViewModel.AddError(nameof(DisplayDay), "Vui lòng chọn thời gian");
                }
                else if (CheckTime() == false)
                {
                    _errorBaseViewModel.AddError(nameof(DisplayDay), $"Ngày phải thuộc {CurrentMatch.ROUND.NGAYBATDAU.ToString() } và {ChanTime.ToString()}");
                }
                OnPropertyChanged();
            }
        }
        public int ID
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }
        public DOIBONG TeamA
        {
            get { return _teamA; }
            set
            {
                _teamA = value;
                OnPropertyChanged(); UpdateListTeamB();
            }
        }
        public DOIBONG TeamB
        {
            get { return _teamB; }
            set { _teamB = value; OnPropertyChanged(); UpdateListTeamA(); }
        }
        public FOOTBALLMATCH CurrentMatch
        {
            get { return _footballMatch; }
            set { _footballMatch = value; OnPropertyChanged(); }
        }
        public THONGTINTRANDAU InfoTeamA
        {
            get { return _infoTeamA; }
            set { _infoTeamA = value; OnPropertyChanged(); }
        }
        public THONGTINTRANDAU InfoTeamB
        {
            get { return _infoTeamB; }
            set { _infoTeamB = value; OnPropertyChanged(); }
        }
        private bool _checkTimeValue;
        public bool CheckTimeValue
        {
            get { return _checkTimeValue; }
            set { _checkTimeValue = value; OnPropertyChanged(); }
        }
        public FootballMatchCard(int id, string displayName, DIADIEM displayPlace, DateTime? displayTime, FOOTBALLMATCH currentMatch)
        {
            _errorBaseViewModel = new ErrorBaseViewModel();
            _errorBaseViewModel.ErrorsChanged += ErrorBaseViewModel_ErrorsChanged;
            ID = id;
            CurrentMatch = currentMatch;
            DisplayName = displayName;
            
            if (CurrentMatch.ROUND != null)
            {
                LEAGUE CurrentLeague = CurrentMatch.ROUND.LEAGUE;
                Rounds = new ObservableCollection<ROUND>(DataProvider.ins.DB.ROUNDs.Where(x => x.IDGIAIDAU == CurrentLeague.ID).ToList());
                DisplayPlaces = new ObservableCollection<DIADIEM>(DataProvider.ins.DB.DIADIEMs.Where(x => x.IDQUOCGIA == CurrentLeague.IDQUOCGIA));
                if (currentMatch.DIADIEM != null)
                {
                    int vari = (int)currentMatch.DIADIEM;
                    DisplayPlace = DisplayPlaces.Where(x=>x.ID == vari).FirstOrDefault();
                }
                InitTeamPlayerOfMatch();
                InitListTeam();
            }
            if (displayTime != null) DisplayDay = displayTime;
            IsEnable2();
        }
        public void IsEnable2() {
            if (AccessUser.userLogin.USERROLE.ID == 2 && CurrentMatch.VONGBANG != 1)
            {
                Enable = true;
            }
            else Enable = false;
        }
        public void InitListTeam()
        {
            if (CurrentMatch.ROUND == null)
            {
                return;
            }
            else {      
                if (CurrentMatch.ROUND.ID == Rounds[0].ID)
                {
                    List<DOIBONG> list1 = new List<DOIBONG>();
                        var list2 = DataProvider.Instance.Database.TEAMOFLEAGUEs.Where(x => x.IDGIAIDAU == CurrentMatch.ROUND.LEAGUE.ID).ToList();
                        foreach (var item in list2)
                        {
                            list1.Add(item.DOIBONG);
                        }
                    TeamList = new ObservableCollection<DOIBONG>(list1);
                    var list3 = DataProvider.ins.Database.FOOTBALLMATCHes.Where(x => x.IDVONG == CurrentMatch.ROUND.ID && x.ID != CurrentMatch.ID).ToList();
                    foreach (var item1 in list3)
                    {
                        var list4 = DataProvider.ins.Database.THONGTINTRANDAUs.Where(x => x.IDTRANDAU == item1.ID).ToList();
                        foreach (var item in list4)
                        {
                            if (item.DOIBONG != null)
                                TeamList.Remove(TeamList.Where(x=>x.ID == item.DOIBONG.ID).FirstOrDefault());
                        }
                    }
                    if (TeamA != null) TeamListDisPlayB = new ObservableCollection<DOIBONG>(TeamList.Where(x => x.ID != TeamA.ID));
                    else TeamListDisPlayB = new ObservableCollection<DOIBONG>(TeamList);
                    if (TeamB != null) TeamListDisPlayA = new ObservableCollection<DOIBONG>(TeamList.Where(x => x.ID != TeamB.ID));
                    else TeamListDisPlayA = new ObservableCollection<DOIBONG>(TeamList);
                }
                else
                {
                    List<DOIBONG> listphu = new List<DOIBONG>();
                    int idx = Rounds[ Convert.ToInt16(CurrentMatch.ROUND.IDDISPLAY) -2 ].ID;
                    var list11 = DataProvider.Instance.Database.FOOTBALLMATCHes.Where(x => x.IDVONG == idx).ToList();
                    foreach (var item1 in list11)
                    {
                        var list22 = DataProvider.ins.Database.THONGTINTRANDAUs.Where(x => x.IDTRANDAU == item1.ID && x.KETQUA == 1).ToList();
                        foreach (var item in list22)
                        {
                            if (item.DOIBONG != null)
                                listphu.Add(item.DOIBONG);
                        }
                    }
                    List<DOIBONG> list1 = new List<DOIBONG>();
                    var list2 = DataProvider.Instance.Database.TEAMOFLEAGUEs.Where(x => x.IDGIAIDAU == CurrentMatch.ROUND.LEAGUE.ID).ToList();
                    foreach (var item in list2)
                    {
                        if (item.DOIBONG != null && listphu.Where(x=>x.ID == item.DOIBONG.ID).FirstOrDefault() != null)
                        {
                            list1.Add(item.DOIBONG);
                        }
                    }
                    TeamList = new ObservableCollection<DOIBONG>(list1);
                    var list3 = DataProvider.ins.Database.FOOTBALLMATCHes.Where(x => x.IDVONG == CurrentMatch.ROUND.ID && x.ID != CurrentMatch.ID).ToList();
                    foreach (var item1 in list3)
                    {
                        var list4 = DataProvider.ins.Database.THONGTINTRANDAUs.Where(x => x.IDTRANDAU == item1.ID).ToList();
                        foreach (var item in list4)
                        {
                            if (item.DOIBONG != null)
                                TeamList.Remove(TeamList.Where(x => x.ID == item.DOIBONG.ID).FirstOrDefault());
                        }
                    }
                    if (TeamA != null) TeamListDisPlayB = new ObservableCollection<DOIBONG>(TeamList.Where(x => x.ID != TeamA.ID));
                    else TeamListDisPlayB = new ObservableCollection<DOIBONG>(TeamList);
                    if (TeamB != null) TeamListDisPlayA = new ObservableCollection<DOIBONG>(TeamList.Where(x => x.ID != TeamB.ID));
                    else TeamListDisPlayA = new ObservableCollection<DOIBONG>(TeamList);
                }
            }
        }
        private void UpdateListTeamA()
        {
            if (TeamB != null && TeamList != null) 
            {
                var temp = TeamList.Where(x => x != TeamB);
                TeamListDisPlayA = new ObservableCollection<DOIBONG>(temp);
            }
        }
        private void UpdateListTeamB()
        {
            if (TeamA != null && TeamList != null )
            {
                var temp1 = TeamList.Where(x => x != TeamA);
                TeamListDisPlayB = new ObservableCollection<DOIBONG>(temp1);
            }
        }
        public void InitTeamPlayerOfMatch()
        {
            
                List<THONGTINTRANDAU> list = DataProvider.Instance.Database.THONGTINTRANDAUs.Where(x => x.IDTRANDAU == ID).ToList();
                if (list.Count == 2)
                {
                    InfoTeamA = list[0];
                    TeamA = InfoTeamA.DOIBONG;
                    ScoreTeamA = InfoTeamA.DIEM.ToString();
                    InfoTeamB = list[1];
                    TeamB = InfoTeamB.DOIBONG;
                    ScoreTeamB = InfoTeamB.DIEM.ToString();
                }
                else { 
                    InfoTeamA = new THONGTINTRANDAU() { IDTRANDAU = ID  };
                    TeamA = null;
                    InfoTeamB = new THONGTINTRANDAU() { IDTRANDAU = ID };
                    TeamB = null; 
                }
                if(ScoreTeamA == ScoreTeamB && InfoTeamA != null && InfoTeamB != null)
            {
                EnableTiSo = true;
                TiSoTeamA = InfoTeamA.THEDO.ToString();
                TiSoTeamB = InfoTeamB.THEDO.ToString();
            }
                else { EnableTiSo = false; }
        }
        private string tisoTeamA;
        public string TiSoTeamA
        {
            get { return tisoTeamA; }
            set { tisoTeamA = value; OnPropertyChanged(); }
        }
        private string tisoTeamB;
        public string TiSoTeamB
        {
            get { return tisoTeamB; }
            set
            {
                tisoTeamB = value; OnPropertyChanged();
            }
        }
        private bool enableTiso;
        public bool EnableTiSo
        {
            get { return enableTiso; }
            set { enableTiso = value; OnPropertyChanged(); }
        }
        private int roundtruoc = -1;
        public bool CheckTime()
        {

            if (DisplayDay != null && Rounds != null)
            {
                if (CurrentMatch.ROUND.ID == Rounds.Last().ID)
                {
                    if (DateTime.Compare(CurrentMatch.ROUND.NGAYBATDAU.TryConvertToDateTime(), DisplayDay.TryConvertToDateTime()) <= 0)
                    {
                        if (CurrentMatch.ROUND.IDDISPLAY != "1") roundtruoc = Rounds[Convert.ToInt16(CurrentMatch.ROUND.IDDISPLAY) - 2].ID;
                        return true;
                    }
                    else
                    {
                        ChanTime = CurrentMatch.ROUND.LEAGUE.NGAYKETTHUC;
                        return false;
                    }
                }
                else if (DateTime.Compare(CurrentMatch.ROUND.NGAYBATDAU.TryConvertToDateTime(), DisplayDay.TryConvertToDateTime()) > 0 ||
                    DateTime.Compare(DisplayDay.TryConvertToDateTime(), Rounds[Convert.ToInt16(CurrentMatch.ROUND.IDDISPLAY)].NGAYBATDAU.TryConvertToDateTime()) > 0)
                {
                    ChanTime = Rounds[Convert.ToInt16(CurrentMatch.ROUND.IDDISPLAY)].NGAYBATDAU;
                    if (CurrentMatch.ROUND.IDDISPLAY != "1") roundtruoc = Rounds[Convert.ToInt16(CurrentMatch.ROUND.IDDISPLAY) - 2].ID;
                    return false;
                }
                else
                {
                    if (CurrentMatch.ROUND.IDDISPLAY != "1") roundtruoc = Rounds[Convert.ToInt16(CurrentMatch.ROUND.IDDISPLAY) - 2].ID;
                    return true;
                }
            }
            else return false;
        }
        
        public void UpdateFootballMatch()
        {
            if (CheckTime() == true && DisplayName != "" && DisplayPlace != null && TeamA != null && TeamB != null && DisplayDay != null)
            {
                List<HUANLUYENVIEN> noticehlvA = new List<HUANLUYENVIEN>();
                List<HUANLUYENVIEN> noticehlvB = new List<HUANLUYENVIEN>();
                noticehlvA = DataProvider.Instance.Database.HUANLUYENVIENs.Where(x => x.IDDOIBONG == TeamA.ID).ToList();
                noticehlvB = DataProvider.Instance.Database.HUANLUYENVIENs.Where(x => x.IDDOIBONG == TeamB.ID).ToList();
                foreach (var notice in noticehlvA)
                {
                    DataProvider.Instance.Database.Notifications.Add(new Notification() { IDHLV = notice.ID, NOTIFY = $"Bạn có lịch đấu với {TeamB.TEN} vào {DisplayDay}", CHECKED = "Chưa xem" });
                }
                foreach (var notice in noticehlvB)
                {
                    DataProvider.Instance.Database.Notifications.Add(new Notification() { IDHLV = notice.ID, NOTIFY = $"Bạn có lịch đấu với {TeamA.TEN} vào {DisplayDay}", CHECKED = "Chưa xem" });
                }
                if (roundtruoc != -1)
                {
                    var list = DataProvider.Instance.Database.FOOTBALLMATCHes.Where(x => x.IDVONG == roundtruoc).ToList();
                    int cou = 0;
                    foreach(var item in list)
                    {
                        var list2 = DataProvider.Instance.Database.THONGTINTRANDAUs.Where(x=>x.IDTRANDAU == item.ID).ToList();
                        foreach(var item1 in list2)
                        {
                            if(item1.DOIBONG.ID == TeamA.ID)
                            {
                                item.VONGBANG = 1;
                                DataProvider.Instance.Database.FOOTBALLMATCHes.AddOrUpdate(item);
                                DataProvider.Instance.Database.SaveChanges();
                                cou++;
                            }
                            if (item1.DOIBONG.ID == TeamB.ID)
                            {
                                item.VONGBANG = 1;
                                DataProvider.Instance.Database.FOOTBALLMATCHes.AddOrUpdate(item);
                                DataProvider.Instance.Database.SaveChanges();
                                cou++;
                            }
                        }
                        if (cou == 2) break;
                    }
                }
                CurrentMatch.THOIGIAN = DisplayDay;
                //CurrentMatch.DIADIEM1.TENDIADIEM = DisplayPlace;

                CurrentMatch.TENTRANDAU = DisplayName;
                CurrentMatch.DIADIEM = DisplayPlace.ID;
                DataProvider.Instance.Database.FOOTBALLMATCHes.AddOrUpdate(CurrentMatch);
                DataProvider.Instance.Database.SaveChanges();
                InfoTeamA.DIEM = Convert.ToInt16(ScoreTeamA);
                InfoTeamB.DIEM = Convert.ToInt16(ScoreTeamB);
                InfoTeamA.IDDOIBONG = TeamA.ID;
                InfoTeamB.IDDOIBONG = TeamB.ID;
                InfoTeamA.IDTRANDAU = CurrentMatch.ID;
                InfoTeamB.IDTRANDAU = CurrentMatch.ID;
                DataProvider.Instance.Database.THONGTINTRANDAUs.AddOrUpdate(InfoTeamA, InfoTeamB);
                DataProvider.Instance.Database.SaveChanges();
                LEAGUE thisLeague = CurrentMatch.ROUND.LEAGUE;
                ROUND thisRound = CurrentMatch.ROUND;
                ListMatchViewModel.Instance.SelectedRound = thisRound;
                //MainViewModel2.Instance.ContentViewModel = new ListMatchViewModel(thisLeague, thisRound);
            }
        }
    }
    
    public interface IBaseCard
    {
    }
    public static class BaseCardExtensions
    {
        public static void CopyCardInfo(this IBaseCard card, IBaseCard anotherCard)
        {
            foreach (PropertyInfo propertyInfo in card.GetType().GetProperties())
            {
                propertyInfo.SetValue(card, propertyInfo.GetValue(anotherCard));

            }
        }
    }
}
