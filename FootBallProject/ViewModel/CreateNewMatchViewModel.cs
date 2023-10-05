using FootBallProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FootBallProject.ViewModel
{
    public class CreateNewMatchViewModel : BaseViewModel
    {
        private ObservableCollection<LEAGUE> _leagues;
        public ObservableCollection<LEAGUE> Leagues
        {
            get { return _leagues; }
            set { _leagues = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ROUND> _rounds;
        public ObservableCollection<ROUND> Rounds
        {
            get { return _rounds; }
            set { _rounds = value; OnPropertyChanged(); }
        }
        private LEAGUE _selectedLeague;
        public LEAGUE SelectedLeague
        {
            get { return _selectedLeague; }
            set { _selectedLeague = value; OnPropertyChanged(); LoadRoundByLeague(); }
        }
        private ROUND _selectedRound;
        public ROUND SelectedRound
        {
            get { return _selectedRound; }
            set { _selectedRound = value; OnPropertyChanged(); }
        }
        private DOIBONG _teamA;
        private DOIBONG _teamB;
        public DOIBONG TeamA
        {
            get { return _teamA; }
            set
            {
                _teamA = value;
                OnPropertyChanged(); UpdateListTeamA();
            }
        }
        public DOIBONG TeamB
        {
            get { return _teamB; }
            set { _teamB = value; OnPropertyChanged(); UpdateListTeamB(); }
        }
        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;

                OnPropertyChanged();
            }
        }
        private void UpdateListTeamA()
        {
            var temp = TeamList.Where(x => x != TeamA);
            TeamListDisPlayB = new ObservableCollection<DOIBONG>(temp);
        }
        private void UpdateListTeamB()
        {
            var temp1 = TeamList.Where(x => x != TeamB);
            TeamListDisPlayA = new ObservableCollection<DOIBONG>(temp1);
        }
        private ObservableCollection<DOIBONG> _teamList;
        public ObservableCollection<DOIBONG> TeamList
        {
            get => _teamList;
            set { _teamList = value; }
        }

        private ObservableCollection<DOIBONG> _teamListDisPlayA;
        public ObservableCollection<DOIBONG> TeamListDisPlayA
        {
            get => _teamListDisPlayA;
            set { _teamListDisPlayA = value; OnPropertyChanged(); }
        }
        private ObservableCollection<DOIBONG> _teamListDisPlayB;
        public ObservableCollection<DOIBONG> TeamListDisPlayB
        {
            get => _teamListDisPlayB;
            set { _teamListDisPlayB = value; OnPropertyChanged(); }
        }
        public void InitListTeam()
        {
            var list1 = DataProvider.Instance.Database.DOIBONGs.ToList();
            TeamList = new ObservableCollection<DOIBONG>(list1);
            TeamListDisPlayB = new ObservableCollection<DOIBONG>(list1.Where(x => x != TeamA));
            TeamListDisPlayA = new ObservableCollection<DOIBONG>(list1.Where(x => x != TeamB));
        }
        private ICommand _createNewMatch;
        public ICommand CreateNewMatch
        {
            get => _createNewMatch;
            set => _createNewMatch = value;
        }
        public CreateNewMatchViewModel()
        {
            var list = DataProvider.Instance.Database.LEAGUEs.ToList();
            Leagues = new ObservableCollection<LEAGUE>(list);
            SelectedLeague = list.FirstOrDefault();
            LoadRoundByLeague();
            InitListTeam();
            CreateNewMatch = new RelayCommand<object>((p) => { return CanCreateNewFootballMatch(); }, (p) => { CreateNewFootballMatch(); });
        }
        private bool CanCreateNewFootballMatch()
        {
            if (SelectedRound == null) return false;
            if (TeamA == null) return false;
            if (TeamB == null) return false;
            if (DisplayName == null) return false;
            return true;
        }
        private void CreateNewFootballMatch()
        {
            FOOTBALLMATCH foot = new FOOTBALLMATCH()
            {
                IDVONG = SelectedRound.ID,
                TENTRANDAU = DisplayName,
            };
            DataProvider.Instance.Database.FOOTBALLMATCHes.Add(foot);
            DataProvider.Instance.Database.SaveChanges();
            THONGTINTRANDAU match1 = new THONGTINTRANDAU()
            {
                IDTRANDAU = foot.ID,
                IDDOIBONG = TeamA.ID
            };

            DataProvider.Instance.Database.THONGTINTRANDAUs.Add(match1);
            DataProvider.Instance.Database.SaveChanges();
            THONGTINTRANDAU match2 = new THONGTINTRANDAU()
            {
                IDTRANDAU = foot.ID,
                IDDOIBONG = TeamB.ID
            };
            DataProvider.Instance.Database.THONGTINTRANDAUs.Add(match2);
            DataProvider.Instance.Database.SaveChanges();
        }
        private void LoadRoundByLeague()
        {
            if (SelectedLeague != null)
                Rounds = new ObservableCollection<ROUND>(DataProvider.Instance.Database.ROUNDs.Where(x => x.IDGIAIDAU == SelectedLeague.ID));
        }
    }

}
