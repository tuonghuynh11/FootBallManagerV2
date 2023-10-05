using FootBallProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.ViewModel
{
    public class TransferConfirmViewModel:BaseViewModel
    {
        private ObservableCollection<CHUYENNHUONG> _TransferPlayers;
        public ObservableCollection<CHUYENNHUONG> TransferPlayers { get => _TransferPlayers; set { _TransferPlayers = value; OnPropertyChanged(); } }
        public TransferConfirmViewModel() {
            TransferPlayers = new ObservableCollection<CHUYENNHUONG>(DataProvider.ins.DB.CHUYENNHUONGs.Where(p=>p.IDDOIMUA!=null));
        }
    }
}
