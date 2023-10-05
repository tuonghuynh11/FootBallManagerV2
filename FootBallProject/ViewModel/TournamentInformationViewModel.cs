using FootBallProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.ViewModel
{
    public class TournamentInformationViewModel:BaseViewModel
    {

        private ObservableCollection<THONGTINGIAIDAU> _TournamentInformation;
        public ObservableCollection<THONGTINGIAIDAU> TournamentInformation { get => _TournamentInformation; set { _TournamentInformation = value; OnPropertyChanged(); } }

        public TournamentInformationViewModel() { }
        public TournamentInformationViewModel(int id) {
          TournamentInformation=new ObservableCollection<THONGTINGIAIDAU>(DataProvider.ins.DB.THONGTINGIAIDAUs.Where(p=>p.IDGIAIDAU == id));
        }

    }
}
