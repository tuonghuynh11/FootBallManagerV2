using FootBallProject.Model;
using FootBallProject.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Data.Entity.Migrations;
using DevExpress.Xpf.Core.ReflectionExtensions;
using FootBallProject.Component.RightBar;

namespace FootBallProject.ViewModel
{
    public class MatchResultViewModel : BaseViewModel
    {
        public MatchResultViewModel CurrentMatchResultViewModel;
        private FootballMatchCard _footballMatchCard;
        public FootballMatchCard FootballMatchCard
        {
            get => _footballMatchCard;
            set { _footballMatchCard = value; OnPropertyChanged(); }
        }

        private string _scoreTeamA;
        private string _scoreTeamB;
        private ObservableCollection<CAUTHU> _playerTeamAs;
        private ObservableCollection<CAUTHU> _playerTeamBs;
        private ObservableCollection<CardItem> _cardTeamAs;
        private THONGTINTRANDAU _matchTeamInfoTeamA;
        private THONGTINTRANDAU _matchTeamInfoTeamB;
        public THONGTINTRANDAU MatchTeamInfoTeamA
        {
            get => _matchTeamInfoTeamA;
            set { _matchTeamInfoTeamA = value; OnPropertyChanged(); }
        }
        public THONGTINTRANDAU MatchTeamInfoTeamB
        {
            get => _matchTeamInfoTeamB;
            set { _matchTeamInfoTeamB = value; OnPropertyChanged(); }
        }
        private bool _enable;
        public bool Enable
        {
            get => _enable;
            set { _enable = value; OnPropertyChanged(); }
        }
        public ObservableCollection<CardItem> CardTeamAs
        {
            get => _cardTeamAs;
            set { _cardTeamAs = value; OnPropertyChanged(); }
        }
        private ObservableCollection<CardItem> _cardTeamBs;
        public ObservableCollection<CardItem> CardTeamBs
        {
            get => _cardTeamBs;
            set { _cardTeamBs = value; OnPropertyChanged(); }
        }
        private ObservableCollection<CardItem> _cardTeamADisplays;
        private ObservableCollection<CardItem> _cardTeamBDisplays;
        public ObservableCollection<CardItem> CardTeamADisplays
        {
            get => _cardTeamADisplays;
            set { _cardTeamADisplays = value; OnPropertyChanged(); }
        }
        public ObservableCollection<CardItem> CardTeamBDisplays
        {
            get => _cardTeamBDisplays;
            set { _cardTeamBDisplays = value; OnPropertyChanged(); }
        }
        public ObservableCollection<CAUTHU> PlayerTeamAs
        {
            get => _playerTeamAs;
            set
            {
                _playerTeamAs = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<CAUTHU> PlayerTeamBs
        {
            get => _playerTeamBs;
            set
            {
                _playerTeamBs = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<int?> _cacTisoLuanLuuDoiA;
        public ObservableCollection<int?> CacTiSoLuanLuuDoiA
        {
            get => _cacTisoLuanLuuDoiA;
            set { _cacTisoLuanLuuDoiA = value; OnPropertyChanged(); }
        }
        private ObservableCollection<int?> _cacTisoLuanLuudoiB;
        public ObservableCollection<int?> CacTiSoLuanLuuDoiB
        {
            get => _cacTisoLuanLuudoiB;
            set { _cacTisoLuanLuudoiB = value; OnPropertyChanged(); }
        }

        private int? _tisoLuanLuuDoiA;
        private int? _tisoLuanLuuDoiB;
        public int? TiSoLuanLuuDoiA
        {
            get { return _tisoLuanLuuDoiA; }
            set
            {
                _tisoLuanLuuDoiA = value;
                OnPropertyChanged();
                RefreshTeamB();
            }
        }
        public int? TiSoLuanLuuDoiB
        {
            get { return _tisoLuanLuuDoiB; }
            set
            {
                _tisoLuanLuuDoiB = value; OnPropertyChanged();
                RefreshTeamA();
            }
        }

        public string ScoreTeamA
        {
            get => _scoreTeamA;
            set
            {
                _scoreTeamA = value;
                OnPropertyChanged();
            }
        }
        public string ScoreTeamB
        {
            get => _scoreTeamB; set
            {
                _scoreTeamB = value;
                OnPropertyChanged();
            }
        }
        public ICommand SettingDetail { get; set; }
        public ICommand CreateNewCard1 { get; set; }
        public ICommand CreateNewCard2 { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        private bool IsValid(string propertyName)
        {
            return !string.IsNullOrEmpty(propertyName) && !string.IsNullOrWhiteSpace(propertyName);
        }
        public MatchResultViewModel()
        {
            FootballMatchCard = null;
        }

        public MatchResultViewModel(UserControl p)
        {
            
                _errorBaseViewModel = new ErrorBaseViewModel();
                _errorBaseViewModel.ErrorsChanged += ErrorBaseViewModel_ErrorsChanged;
                FootballMatchCard card = p.DataContext as FootballMatchCard;
                FootballMatchCard = card;
                PlayerTeamAs = new ObservableCollection<CAUTHU>(DataProvider.Instance.Database.CAUTHUs.Where(x => x.IDDOIBONG == FootballMatchCard.TeamA.ID));
                PlayerTeamBs = new ObservableCollection<CAUTHU>(DataProvider.Instance.Database.CAUTHUs.Where(x => x.IDDOIBONG == FootballMatchCard.TeamB.ID));
                MatchTeamInfoTeamA = FootballMatchCard.InfoTeamA;
                MatchTeamInfoTeamB = FootballMatchCard.InfoTeamB;
                CardTeamAs = new ObservableCollection<CardItem>();
                var itemAs = DataProvider.Instance.Database.ITEMs.Where(x => x.IDTHONGTINTRANDAU == MatchTeamInfoTeamA.ID).ToList();
                foreach (var item in itemAs)
                {
                    CardTeamAs.Add(new CardItem(item, PlayerTeamAs, this));
                }
                CardTeamBs = new ObservableCollection<CardItem>();
                var itemBs = DataProvider.Instance.Database.ITEMs.Where(x => x.IDTHONGTINTRANDAU == MatchTeamInfoTeamB.ID).ToList();
                foreach (var item in itemBs)
                {
                    CardTeamBs.Add(new CardItem(item, PlayerTeamBs, this));
                }
                CardTeamADisplays = new ObservableCollection<CardItem>(CardTeamAs);
                CardTeamBDisplays = new ObservableCollection<CardItem>(CardTeamBs);
                if (MatchTeamInfoTeamA.DIEM == null)
                {
                    MatchTeamInfoTeamA.DIEM = 0;
                    DataProvider.ins.DB.THONGTINTRANDAUs.AddOrUpdate(MatchTeamInfoTeamA);
                    DataProvider.ins.DB.SaveChanges();
                }
                if (MatchTeamInfoTeamB.DIEM == null)
                {
                    MatchTeamInfoTeamB.DIEM = 0;
                    DataProvider.ins.DB.THONGTINTRANDAUs.AddOrUpdate(MatchTeamInfoTeamB);
                    DataProvider.ins.DB.SaveChanges();
                }

                ScoreTeamA = MatchTeamInfoTeamA.DIEM.ToString();
                ScoreTeamB = MatchTeamInfoTeamB.DIEM.ToString();
                CreateNewCard1 = new RelayCommand<object>((x) => { return true; }, (x) => { CreateNewItemCard1(); });
                CreateNewCard2 = new RelayCommand<object>((x) => { return true; }, (x) => { CreateNewItemCard2(); });
                SaveCommand = new RelayCommand<object>((x) => { return true; }, (x) => { SaveFuntion(); });
                CancelCommand = new RelayCommand<object>((x) => { return true; }, (x) => { CancelFuntion(); });
            //CurrentMatchResultViewModel = this;
                listTiSo = new ObservableCollection<int?>() { 0, 1, 2, 3, 4, 5 };
                TiSoLuanLuuDoiA = (int?) MatchTeamInfoTeamA.THEDO;
                TiSoLuanLuuDoiB =(int?) MatchTeamInfoTeamB.THEDO;
                IsEnable();
        }
        public ObservableCollection<int?> listTiSo;
        private void RefreshTeamA()
        {
            if (listTiSo == null) return;
            if (TiSoLuanLuuDoiB != null) CacTiSoLuanLuuDoiA = new ObservableCollection<int?>(listTiSo.Where(x => x != TiSoLuanLuuDoiB));
            else CacTiSoLuanLuuDoiA = new ObservableCollection<int?>(listTiSo);
        }
        private void RefreshTeamB()
        {
            if (listTiSo == null) return;
            if (TiSoLuanLuuDoiA != null) CacTiSoLuanLuuDoiB = new ObservableCollection<int?>(listTiSo.Where(x => x != TiSoLuanLuuDoiA));
            else CacTiSoLuanLuuDoiB = new ObservableCollection<int?>(listTiSo);

        }
        private void IsEnable()
        {
            if (ScoreTeamA == ScoreTeamB) Enable = true;
            if (CardTeamADisplays.Count == CardTeamBDisplays.Count) Enable = true;
            else Enable = false;
        }
        private void CancelFuntion()
        {
            CardTeamADisplays = new ObservableCollection<CardItem>(CardTeamAs);
            CardTeamBDisplays = new ObservableCollection<CardItem>(CardTeamBs);
            ScoreTeamA = MatchTeamInfoTeamA.DIEM.ToString();
            ScoreTeamB = MatchTeamInfoTeamB.DIEM.ToString();
        }
        private void SaveFuntion()
        {
            if (TiSoLuanLuuDoiA!= null && TiSoLuanLuuDoiB!=null && TiSoLuanLuuDoiA == TiSoLuanLuuDoiB) return;
            CardTeamAs = new ObservableCollection<CardItem>();
            var itemAs = DataProvider.Instance.Database.ITEMs.Where(x => x.IDTHONGTINTRANDAU == MatchTeamInfoTeamA.ID).ToList();
            foreach (var item in itemAs)
            {
                CardTeamAs.Add(new CardItem(item, PlayerTeamAs,this));
            }
            CardTeamBs = new ObservableCollection<CardItem>();
            var itemBs = DataProvider.Instance.Database.ITEMs.Where(x => x.IDTHONGTINTRANDAU == MatchTeamInfoTeamB.ID).ToList();
            foreach (var item in itemBs)
            {
                CardTeamBs.Add(new CardItem(item, PlayerTeamBs,this));
            }

            var oldlist = DataProvider.Instance.Database.ITEMs.Where(x => x.IDTHONGTINTRANDAU == MatchTeamInfoTeamA.ID).ToList();
            foreach (var item in oldlist)
            {
                DataProvider.Instance.Database.ITEMs.Remove(item);
            }
            var oldlist2 = DataProvider.Instance.Database.ITEMs.Where(x => x.IDTHONGTINTRANDAU == MatchTeamInfoTeamB.ID).ToList();
            foreach (var item in oldlist2)
            {
                DataProvider.Instance.Database.ITEMs.Remove(item);
            }
            Dictionary<int, int> sobanthang = new Dictionary<int, int>();
            foreach (var card in CardTeamADisplays)
            {
                {
                    ITEM temp = new ITEM
                    {
                        IDITEMTYPE = 1,
                        THOIGIAN = card.Time,
                        CAUTHU = card.Player,
                        THONGTINTRANDAU = MatchTeamInfoTeamA
                    };
                    DataProvider.Instance.Database.ITEMs.AddOrUpdate(temp);
                    if (!sobanthang.ContainsKey(card.Player.ID)) sobanthang.Add(card.Player.ID, 0);
                    sobanthang[card.Player.ID] =0+ sobanthang[card.Player.ID] + 1;
                    
                }
            }
            foreach (var card in CardTeamBDisplays)
            {
                {
                    ITEM temp = new ITEM
                    {
                        IDITEMTYPE = 1,
                        THOIGIAN = card.Time,
                        CAUTHU = card.Player,
                        THONGTINTRANDAU = MatchTeamInfoTeamB
                };
                  if(!sobanthang.ContainsKey(card.Player.ID))  sobanthang.Add(card.Player.ID, 0);
                    sobanthang[card.Player.ID] =0+ sobanthang[card.Player.ID] + 1;
                    DataProvider.Instance.Database.ITEMs.AddOrUpdate(temp);
                }
            }
            foreach (var banthang in sobanthang)
            {
                THAMGIA thamgia = new THAMGIA()
                {
                    IDCAUTHU = banthang.Key,
                    IDTRAN = (int)MatchTeamInfoTeamA.IDTRANDAU,
                    SOBANTHANG = banthang.Value
                };
                DataProvider.ins.DB.THAMGIAs.AddOrUpdate(thamgia);
                DataProvider.ins.DB.SaveChanges();
            }
            MatchTeamInfoTeamA.DIEM = Convert.ToInt16(ScoreTeamA);
            MatchTeamInfoTeamB.DIEM = Convert.ToInt16(ScoreTeamB);
            MatchTeamInfoTeamA.THEDO = TiSoLuanLuuDoiA;
            MatchTeamInfoTeamB.THEDO = TiSoLuanLuuDoiB;
            if (MatchTeamInfoTeamA.DIEM > MatchTeamInfoTeamB.DIEM || MatchTeamInfoTeamA.THEDO > MatchTeamInfoTeamB.THEDO )
            {
                MatchTeamInfoTeamA.KETQUA = 1;
                MatchTeamInfoTeamB.KETQUA = 0;
            }
            else if (MatchTeamInfoTeamA.DIEM < MatchTeamInfoTeamB.DIEM || MatchTeamInfoTeamA.THEDO < MatchTeamInfoTeamB.THEDO)
            {
                MatchTeamInfoTeamA.KETQUA = 0;
                MatchTeamInfoTeamB.KETQUA = 1;
            }
            DataProvider.Instance.Database.SaveChanges();
            LEAGUE thisLeague = FootballMatchCard.CurrentMatch.ROUND.LEAGUE;
            ROUND thisRound = FootballMatchCard.CurrentMatch.ROUND;
            ListMatchViewModel.Instance.SelectedRound = thisRound;
            ListMatchRightBarViewModel.Instance.RightSideBarItemViewModel = new ListMatchRightSideBarInfo2(FootballMatchCard);
        }
        private void CreateNewItemCard1()
        {
            ITEM item1 = new ITEM()
            {
                IDITEMTYPE = 1
            };
            CardItem item = new CardItem(item1, PlayerTeamAs, this);
            CardTeamADisplays.Add(item);
            ScoreTeamA = CardTeamADisplays.Count().ToString();
            IsEnable();
        }
        private void CreateNewItemCard2()
        {
            ITEM item1 = new ITEM()
            {
                IDITEMTYPE = 1
            };
            CardItem item = new CardItem(item1, PlayerTeamBs, this);
            CardTeamBDisplays.Add(item);
            ScoreTeamB = CardTeamBDisplays.Count().ToString();
            IsEnable();
        }
        public void DeleteItemCard(CardItem x)
        {
            if (CardTeamADisplays.Contains(x))
            {
                CardTeamADisplays.Remove(x);
                ScoreTeamA = CardTeamADisplays.Count().ToString();
               
            }
            else if (CardTeamBDisplays.Contains(x))
            {
                CardTeamBDisplays.Remove(x);
                ScoreTeamB = CardTeamBDisplays.Count().ToString();
            }
            IsEnable();
        }
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

        #endregion

    }

}
