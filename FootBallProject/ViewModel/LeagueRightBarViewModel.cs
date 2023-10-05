using FootBallProject.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using FootBallProject.Model;
using FootBallProject.Usercontrol;
using System.Data.Entity.Migrations;

namespace FootBallProject.ViewModel
{
    public class LeagueRightBarViewModel : BaseViewModel
    {
        private static LeagueRightBarViewModel _ins;
        public static LeagueRightBarViewModel Instance
        {
            get {  return _ins; }
            set { _ins = value;  }
        }
        private LeagueCardOb _selectedLeagueCard;
        private object _rightSideBarItemViewModel;
        private object _emptyStateRightBarViewModel;
        public LeagueCardOb SelectedLeagueCard
        {
            get { return _selectedLeagueCard; }
            set { _selectedLeagueCard = value; }
        }
        public object _leagueInfomationViewModel;
        public object RightSideBarItemViewModel
        {
            get => _rightSideBarItemViewModel;
            set
            {
                _rightSideBarItemViewModel = value;
                OnPropertyChanged();

            }
        }
        public ICommand ShowInfo { get; set; }
        public ICommand EditLeague { get; set; }
        public ICommand DeleteLeague { get; set; }
        public LeagueRightBarViewModel()
        {
            Instance = this;
            _leagueInfomationViewModel = new LeagueInfomationViewModel(null);
            _emptyStateRightBarViewModel = new EmptyRightSideBarViewModel();
            RightSideBarItemViewModel = _emptyStateRightBarViewModel;
            ShowInfo = new RelayCommand<UserControl>((p) => { return true; }, (p) => { ShowInfoFuntion(p); });
            EditLeague = new RelayCommand<UserControl>(p => { return true; }, p => { EditLeagueFunction(p); });
            DeleteLeague = new RelayCommand<UserControl>(p => { return true; }, p => { DeleteFuntion(p); });
        }
        private void EditLeagueFunction(UserControl p)
        {
            LeagueCardOb card = p.DataContext as LeagueCardOb;
            ListofLeagueViewModel.Instance.Currentleague = card;
            RightSideBarItemViewModel = new LeagueEditViewModel(card);
        }
        private void ShowInfoFuntion(UserControl p)
        {
            LeagueCardOb card = p.DataContext as LeagueCardOb;
            ListofLeagueViewModel.Instance.Currentleague = card;
            RightSideBarItemViewModel = new LeagueInfomationViewModel(card);
            //ListofLeagueViewModel.Instance.Refresh(card);
        }
        public void ShowInfoFuntion2(LeagueCardOb card)
        {
            ListofLeagueViewModel.Instance.Currentleague = card;
            RightSideBarItemViewModel = new LeagueInfomationViewModel(card);
        }
        public void DeleteFuntion(UserControl p)
        {
            LeagueCardOb card = p.DataContext as LeagueCardOb;
            card.League.NGAYBATDAU = null;
            DataProvider.ins.Database.LEAGUEs.AddOrUpdate(card.League);
            DataProvider.ins.Database.SaveChanges();
            ListofLeagueViewModel.Instance.UpdateLeagues();
            RightSideBarItemViewModel = _emptyStateRightBarViewModel;
        }
    }
}
