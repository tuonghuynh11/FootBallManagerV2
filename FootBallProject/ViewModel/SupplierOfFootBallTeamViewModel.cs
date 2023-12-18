using FootBallProject.Class;
using FootBallProject.Model;
using FootBallProject.PopUp;
using FootBallProject.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.ViewModel
{
    public class SupplierOfFootBallTeamViewModel:BaseViewModel
    {
        private ObservableCollection<DOIBONGSUPPLIER> _SuppliersCooperated;
        public ObservableCollection<DOIBONGSUPPLIER> SuppliersCooperated
        {
            get => _SuppliersCooperated; set
            {

                _SuppliersCooperated = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<DOIBONGSUPPLIER> _SuppliersWaitConfirm;
        public ObservableCollection<DOIBONGSUPPLIER> SuppliersWaitConfirm { get => _SuppliersWaitConfirm; set { _SuppliersWaitConfirm = value; OnPropertyChanged(); } }

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
        private ObservableCollection<DOIBONGSUPPLIER> _allDoiBongSupplier;
        public ObservableCollection<DOIBONGSUPPLIER> allDoiBongSupplier
        {
            get => _allDoiBongSupplier; set
            {

                _allDoiBongSupplier = value; OnPropertyChanged();
            }
        }

        private ObservableCollection<SUPPLIERSERVICE> _allServices;
        public ObservableCollection<SUPPLIERSERVICE> allServices
        {
            get => _allServices; set
            {

                _allServices = value; OnPropertyChanged();
            }
        }
        public SupplierOfFootBallTeamViewModel()
        {
            try {
                 LoadingData();
            }
            catch (Exception ex){
                return;
            }
        }

        public async Task LoadingData()
        {
            //Loading loading = new Loading();
            //loading.Show();
            //await Task.Delay(1000);

            List<DOIBONGSUPPLIER> allDoiBongSupplier = (await Task.Run(() => APIService.ins.getDoiBongSuppliers()));
            if (allDoiBongSupplier == null)
            {
                return;
            }

            this.allDoiBongSupplier = new ObservableCollection<DOIBONGSUPPLIER>(allDoiBongSupplier);
            SuppliersCooperated = new ObservableCollection<DOIBONGSUPPLIER>(allDoiBongSupplier.Where(db =>db.idDoiBong== AccessUser.userLogin.IDDOIBONG&& db.status == 2));

            SuppliersWaitConfirm = new ObservableCollection<DOIBONGSUPPLIER>(allDoiBongSupplier.Where(t => t.idDoiBong == AccessUser.userLogin.IDDOIBONG && t.status == 0));
            if (SuppliersCooperated == null)
            {
                SuppliersCooperated = new ObservableCollection<DOIBONGSUPPLIER>(new List<DOIBONGSUPPLIER>());
            }
            if (SuppliersWaitConfirm == null)
            {
                SuppliersWaitConfirm = new ObservableCollection<DOIBONGSUPPLIER>(new List<DOIBONGSUPPLIER>());
            }
            supplierInfo = SuppliersCooperated[0].SUPPLIER;
            supplierService = new ObservableCollection<SERVICE>();
            List<SUPPLIERSERVICE> supplierServicess = await APIService.ins.getAllSupplierServices();
            allServices = new ObservableCollection<SUPPLIERSERVICE>(supplierServicess);
            foreach (SUPPLIERSERVICE service in supplierServicess)
            {
                if (service.idSupplier == supplierInfo.idSupplier)
                {
                    supplierService.Add(service.SERVICE);
                }
            }

            //List <DOIBONG> doibongs= await APIService.ins.ge
            //loading.Close();
        }

        public void LoadSupplierInfo(int idSupplier)
        {
            supplierInfo = SuppliersCooperated.Where(s=>s.idSupplier==idSupplier).FirstOrDefault().SUPPLIER;
            supplierService = new ObservableCollection<SERVICE>();
            if (allServices != null)
            {
                foreach (SUPPLIERSERVICE service in allServices)
                {
                    if (service.idSupplier == idSupplier)
                    {
                        supplierService.Add(service.SERVICE);
                    }
                }
            }
           
        }
        public async Task LoadSupplierWaitConfirm()
        {
            List<DOIBONGSUPPLIER> allDoiBongSupplier = (await Task.Run(() => APIService.ins.getDoiBongSuppliers()));
            if (allDoiBongSupplier != null)
            {
                SuppliersWaitConfirm = new ObservableCollection<DOIBONGSUPPLIER>(allDoiBongSupplier.Where(t => t.idDoiBong == AccessUser.userLogin.IDDOIBONG && t.status == 0));

            }
        }
    }
}
