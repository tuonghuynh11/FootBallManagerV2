using FootBallProject.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FootBallProject.ViewModel
{
    class ListMatchRightSideBarInfo2 : BaseViewModel
    {
        private FootballMatchCard _currentCard;
        public FootballMatchCard CurrentCard
        {
            get { return _currentCard; }
            set
            {
                _currentCard = value;
                OnPropertyChanged();
            }
        }
        private FootballMatchCard _actualCard;
        public FootballMatchCard ActualCard
        {
            get { return _actualCard; }
            set { _actualCard = value; }
        }
        #region command
        private ICommand _cancelEditMatchInfo;
        public ICommand DeleteCardInfo { get => _cancelEditMatchInfo; set => _cancelEditMatchInfo = value; }
        #endregion

        public ListMatchRightSideBarInfo2() { CurrentCard = null; }
        public ListMatchRightSideBarInfo2(FootballMatchCard card, bool isCreateNew = false)
        {
            card.InitListTeam();
            CurrentCard = new FootballMatchCard(card.ID, card.DisplayName, card.DisplayPlace, card.DisplayDay, card.CurrentMatch);
            CurrentCard.InitListTeam();
            //CurrentCard.InitListTeam();
            ActualCard = card;
            InitCommand();
        }
        private void InitCommand()
        {

            DeleteCardInfo = new RelayCommand<object>((p) => { return true; }, (p) => { DeleteFuntion(); /*MessageBox.Show("Vao command");*/ });
        }
        private void DeleteFuntion()
        {
            try
            {
                CurrentCard.UpdateFootballMatch();
            }
            catch { }
        }
    }

}
