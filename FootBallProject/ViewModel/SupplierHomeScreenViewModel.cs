using DevExpress.Xpf.Grid;
using FootBallProject.Class;
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

        private ObservableCollection<SERVICE> _supplierService;
        public ObservableCollection<SERVICE> supplierService
        {
            get => _supplierService; set
            {

                _supplierService = value; OnPropertyChanged();
            }
        }
        private SUPPLIER _supplierInfo;
        public SUPPLIER supplierInfo
        {
            get => _supplierInfo; set
            {

                _supplierInfo = value; OnPropertyChanged();
            }
        }
        public SupplierHomeScreenViewModel() {
         
             LoadingData();

        }

        public async Task LoadingData()
        {
            //Loading loading = new Loading();
            //loading.Show();
            //await Task.Delay(1000);
            SingleSupplierResponse res = (await Task.Run(()=> APIService.ins.getSuppliersById(AccessUser.userLogin.IDSUPPLIER)));
            FootBallTeamsCooperated = new ObservableCollection<DOIBONGSUPPLIER>(res.data.DOIBONGSUPPLIERs.Where(db=>db.status==2));
            FootBallTeamsUnCooperate = new ObservableCollection<DOIBONG>(res.footBallTeamsUnCooperate);


            List<DOIBONGSUPPLIER> allDoiBongSupplier = (await Task.Run(() => APIService.ins.getDoiBongSuppliers()));

            FootBallTeamsWaitConfirm = new ObservableCollection<DOIBONGSUPPLIER>(allDoiBongSupplier.Where(t => t.idSupplier == AccessUser.userLogin.IDSUPPLIER && t.status == 0));


            supplierInfo = res.data;
            supplierService = new ObservableCollection<SERVICE>();
            List<SUPPLIERSERVICE> supplierServicess = await APIService.ins.getAllSupplierServices();
            foreach (SUPPLIERSERVICE service in supplierServicess)
            {
                if (service.idSupplier == AccessUser.userLogin.IDSUPPLIER)
                {
                    supplierService.Add(service.SERVICE);
                }
            }


            //List <DOIBONG> doibongs= await APIService.ins.ge
            //loading.Close();
        }

        public async Task LoadFootBallTeamsWaitConfirm()
        {
            List<DOIBONGSUPPLIER> allDoiBongSupplier = (await Task.Run(() => APIService.ins.getDoiBongSuppliers()));
            if(allDoiBongSupplier != null) { 
             FootBallTeamsWaitConfirm = new ObservableCollection<DOIBONGSUPPLIER>(allDoiBongSupplier.Where(t => t.idSupplier == AccessUser.userLogin.IDSUPPLIER && t.status == 0));

            }
        }
    }
}
