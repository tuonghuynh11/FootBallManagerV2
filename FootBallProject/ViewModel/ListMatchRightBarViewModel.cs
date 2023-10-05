using DevExpress.Xpf.Editors.Helpers;
using FootBallProject.Object;
using FootBallProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace FootBallProject.ViewModel
{
    public class ListMatchRightBarViewModel : BaseViewModel
    {
        private static ListMatchRightBarViewModel s_instance;
        private object _emptyStateRightBarViewModel;
        private object _adminListMatchSideBarInfo;
        private object _rightSideBarItemViewModel;
        private FootballMatchCard _selectedMatchCard;
        private bool _enable;
        public bool Enable
        {
            get { return _enable; }
            set
            {
                _enable = value; OnPropertyChanged(nameof(Enable));
            }
        }
        public static ListMatchRightBarViewModel Instance
        {
            get => s_instance ?? (s_instance = new ListMatchRightBarViewModel());
            private set => s_instance = value;
        }
        public object RightSideBarItemViewModel
        {
            get => _rightSideBarItemViewModel;
            set
            {
                _rightSideBarItemViewModel = value;
                OnPropertyChanged();

            }
        }
        public object AdminListMatchSideBarInfo
        {
            get => _adminListMatchSideBarInfo;
            set { _adminListMatchSideBarInfo = value; OnPropertyChanged(); }
        }
        public object EmptyStateRightBarViewModel
        {
            get => _emptyStateRightBarViewModel;
            set { _emptyStateRightBarViewModel = value; OnPropertyChanged(); }
        }
        public FootballMatchCard SelectedMatchCard
        {
            get => _selectedMatchCard;
            set
            {
                _selectedMatchCard = value;
                OnPropertyChanged();
                _adminListMatchSideBarInfo = new ListMatchRightSideBarInfo(_selectedMatchCard);
                _rightSideBarItemViewModel = _adminListMatchSideBarInfo;
            }
        }
        #region command
        private ICommand _showItemofMatchInfo;
        private ICommand _editMatchInfo;
        public ICommand SettingResult { get; set; }
        private ICommand _deleteItemofMatchInfo;
        private ICommand _showMatchInfo;
        private ICommand _createNewMatch;
        private ICommand _settingDetail;
        public ICommand SettingDetail { get; set; }
        public ICommand ShowInfo2 { get; set; }
        public ICommand CreateNewMatch
        {
            get => _createNewMatch;
            set { _createNewMatch = value; OnPropertyChanged(); }
        }
        public ICommand ShowMatchInfo
        {
            get => _showMatchInfo; set { _showMatchInfo = value; OnPropertyChanged(); }

        }

        public ICommand EditMatchInfo
        {
            get => _editMatchInfo;
            set => _editMatchInfo = value;
        }
        public ICommand DeleteItemofMatchInfo
        {
            get => _deleteItemofMatchInfo; set => _deleteItemofMatchInfo = value;
        }

        public ICommand ShowItemofMatchInfo
        {
            get => _showItemofMatchInfo; set => _showItemofMatchInfo = value;
        }
        #endregion
        public ListMatchRightBarViewModel()
        {
            RightSideBarItemViewModel = new EmptyRightSideBarViewModel();
            //InitRightBarViewModel();
            //InitRightBarCommand();
            //Instance = this;
            Instance = this;
            // EditMatchInfo1(null);
            CreateNewMatch = new RelayCommand<object>((p) => true, (p) => { CreateNewMatch1(); });
            ShowMatchInfo = new RelayCommand<UserControl>((p) => true, (p) => { EditMatchInfo1(p); });
            ShowInfo2 = new RelayCommand<UserControl>((p) => true, (p) => { ShowInfomation(p); });
            SettingDetail = new RelayCommand<UserControl>((p) => true, (p) => { ShowDetail(p); });
            SettingResult = new RelayCommand<UserControl>((p) => true, (p) => { SetResult(p); });
            Enable = true;
        }
        public void Refresh()
        {
            RightSideBarItemViewModel = new EmptyRightSideBarViewModel();
        }
        private bool Check(FootballMatchCard x)
        {
            if (x.TeamA == null || x.TeamB == null && x.InfoTeamA == null
                || x.InfoTeamB == null) return false;
            return true;
        }
        private void SetResult(UserControl userControl)
        {
            var x = userControl.DataContext as FootballMatchCard;

            if (Check(x) == true && x.DisplayDay != null && DateTime.Compare(x.TryConvertToDateTime(), DateTime.Now) < 0)
            {

                Enable = true;
                RightSideBarItemViewModel = new MatchResultViewModel(userControl);
            }
            else
            {
                RightSideBarItemViewModel = new MatchNoDisPlayViewModel();
            }
        }
        private void ShowDetail(UserControl p)
        {
        }
        private void InitRightBarViewModel()
        {
            EmptyStateRightBarViewModel = new EmptyRightSideBarViewModel();
            AdminListMatchSideBarInfo = new ListMatchRightSideBarInfo();
            RightSideBarItemViewModel = EmptyStateRightBarViewModel;
        }
        private void ShowInfomation(UserControl p)
        {
            FootballMatchCard card = p.DataContext as FootballMatchCard;
            RightSideBarItemViewModel = new ListMatchRightSideBarInfo2(card);
        }
        public void ShowInfomation(FootballMatchCard p)
        {
            
            RightSideBarItemViewModel = new ListMatchRightSideBarInfo2(p);
        }
        private void CreateNewMatch1()
        {
            RightSideBarItemViewModel = new CreateNewMatchViewModel();
        }
        private void InitRightBarCommand()
        {
            ShowMatchInfo = new RelayCommand<UserControl>((p) => { return true; }, (p) => EditMatchInfo1(p));
        }
        private void EditMatchInfo1(UserControl p)
        {
            //FootballMatch cardd = DataProvider.Instance.Database.FootballMatches.FirstOrDefault();
            FootballMatchCard card = p.DataContext as FootballMatchCard;
            //FootballMatchCard card = new FootballMatchCard(cardd.id, cardd.name, cardd.diadiem, cardd.thoigian);
            RightSideBarItemViewModel = new ListMatchRightSideBarInfo(card);
        }
    }

}
