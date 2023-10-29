using DevExpress.Xpf.Grid;
using FootBallProject.Model;
using FootBallProject.PopUp;
using FootBallProject.Service;
using Microsoft.Office.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.ViewModel
{
    public class SupplierHomeScreenViewModel : BaseViewModel
    {
        private ObservableCollection<DOIBONGSUPPLIER> _FootBallTeamsCooperated;
        public ObservableCollection<DOIBONGSUPPLIER> FootBallTeamsCooperated { get => _FootBallTeamsCooperated; set {

                _FootBallTeamsCooperated = value; OnPropertyChanged(); } }


        private ObservableCollection<DOIBONG> _FootBallTeamsUnCooperate;
        public ObservableCollection<DOIBONG> FootBallTeamsUnCooperate { get => _FootBallTeamsUnCooperate; set { _FootBallTeamsUnCooperate = value; OnPropertyChanged(); } }

        private ObservableCollection<DOIBONGSUPPLIER> _FootBallTeamsWaitConfirm;
        public ObservableCollection<DOIBONGSUPPLIER> FootBallTeamsWaitConfirm { get => _FootBallTeamsWaitConfirm; set { _FootBallTeamsWaitConfirm = value; OnPropertyChanged(); } }


        public SupplierHomeScreenViewModel() {
         
             LoadingData();

        }

        public async Task LoadingData()
        {
            //Loading loading= new Loading();
            //loading.Show();
            //await Task.Delay(1000);
            SingleSupplierResponse res = (await Task.Run(()=> APIService.ins.getSuppliersById(1)));
            FootBallTeamsCooperated = new ObservableCollection<DOIBONGSUPPLIER>(res.data.DOIBONGSUPPLIERs);
            FootBallTeamsUnCooperate = new ObservableCollection<DOIBONG>(res.footBallTeamsUnCooperate);
            //List <DOIBONG> doibongs= await APIService.ins.ge
            //loading.Close();
        }
    }
}
