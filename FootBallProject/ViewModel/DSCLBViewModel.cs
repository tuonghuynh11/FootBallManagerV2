using FootBallProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.ViewModel
{
    public class DSCLBViewModel:BaseViewModel
    {
        private ObservableCollection<DOIBONG> _DoiBongList;
        public ObservableCollection<DOIBONG> DoiBongList { get => _DoiBongList; set { _DoiBongList = value; OnPropertyChanged(); } }

        public DSCLBViewModel()
        {
            LoadDoiBong();
        }
        void LoadDoiBong()
        {
            DoiBongList = new ObservableCollection<DOIBONG>(DataProvider.ins.DB.DOIBONGs);
        }
    }
}
