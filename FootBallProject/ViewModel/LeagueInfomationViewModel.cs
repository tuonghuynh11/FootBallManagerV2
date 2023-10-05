using FootBallProject.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.ViewModel
{
    public class LeagueInfomationViewModel
    {
        private LeagueCardOb _selectedLeague;
        public LeagueCardOb SelectedLeague
        {
            get => _selectedLeague;
            set { _selectedLeague = value; }
        }
        public LeagueInfomationViewModel(LeagueCardOb card)
        {
            SelectedLeague = card;
        }
    }
}
