using DevExpress.Xpf.Charts;
using FootBallProject.Component.League;
using FootBallProject.Model;
using FootBallProject.Object;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace FootBallProject.ViewModel
{
    public class ConfigAutoViewModel : BaseViewModel
    {
        private static ConfigAutoViewModel ins;
        public static ConfigAutoViewModel Instance
        {
            get { return ins; }
            set { ins = value; }
        }
        private LeagueCardOb _currentleague;
        public LeagueCardOb Currentleague
        {
            get { return _currentleague; }
            set
            {
                _currentleague = value; OnPropertyChanged();
            }
        }
        private ObservableCollection<RoundObject> _roundlist;
        public ObservableCollection<RoundObject> RoundList
        {
            get { return _roundlist; }
            set
            {
                _roundlist = value;
                OnPropertyChanged();
            }
        }

        public ListofLeagueViewModel Ins;
        private ObservableCollection<TEAMOFLEAGUE> teams = new ObservableCollection<TEAMOFLEAGUE>();
        public ObservableCollection<TEAMOFLEAGUE> Teams
        {
            get { return teams; }
            set { teams = value; OnPropertyChanged(); }
        }
        public ConfigAutoViewModel(ListofLeagueViewModel ins)
        {
            Instance = this;
            Currentleague = new LeagueCardOb(ins.Currentleague.League);
            Ins = ListofLeagueViewModel.Instance;
            Teams = new ObservableCollection<TEAMOFLEAGUE>( ins.Teams);
            RoundList = new ObservableCollection<RoundObject>( ins.RoundList);
        }
        public void Update()
        {
            Currentleague = new LeagueCardOb(Ins.Currentleague.League);
            Teams = new ObservableCollection<TEAMOFLEAGUE>(Ins.Teams);
            RoundList = new ObservableCollection<RoundObject>(Ins.RoundList);
        }
    }
}
