using FootBallProject.Model;
using FootBallProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace FootBallProject.Object
{
    public class CardItem : BaseViewModel
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }
        private THONGTINTRANDAU _currnetMatchTeamInfo;
        public THONGTINTRANDAU CurrentnetMatchTeamInfo
        {
            get => _currnetMatchTeamInfo;
            set { _currnetMatchTeamInfo = value; OnPropertyChanged(); }
        }
        private CAUTHU _player;
        public CAUTHU Player
        {
            get { return _player; }
            set { _player = value; OnPropertyChanged(); }
        }
        private string _time;
        public string Time
        {
            get { return _time; }
            set { _time = value; OnPropertyChanged(); }
        }
        private ITEMTYPE _type;
        public ITEMTYPE Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged(); }
        }
        private ObservableCollection<CAUTHU> _listPlayers;
        public ObservableCollection<CAUTHU> ListPlayers
        {
            get { return _listPlayers; }
            set { _listPlayers = value; OnPropertyChanged(); }
        }
        public ICommand DeleteCard { get; set; }
        public CardItem()
        {
        }
        private MatchResultViewModel matchResultViewModel;
        public CardItem(ITEM item, ObservableCollection<CAUTHU> list, MatchResultViewModel matchresultviewmodel)
        {
            Id = item.ID;
            Player = item.CAUTHU;
            Time = item.THOIGIAN;
            Type = item.ITEMTYPE;
            ListPlayers = list;
            matchResultViewModel = matchresultviewmodel;
            DeleteCard = new RelayCommand<object>((x) => { return true; }, (x) => { DeleteMySelfFuntion(); });
        }
        public void SaveCardItem()
        {
            ITEM item = new ITEM();
            if (Id > 0) item.ID = Id;
            item.CAUTHU = Player;
            item.THOIGIAN = Time;
            item.ITEMTYPE = Type;
            item.THONGTINTRANDAU = CurrentnetMatchTeamInfo;
            DataProvider.Instance.Database.ITEMs.AddOrUpdate(item);
            DataProvider.Instance.Database.SaveChanges();
        }
        private void DeleteMySelfFuntion()
        {
            matchResultViewModel.DeleteItemCard(this);
        }
    }
}
