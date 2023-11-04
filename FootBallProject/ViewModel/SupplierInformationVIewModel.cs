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
    public class SupplierInformationVIewModel:BaseViewModel
    {
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
        public SupplierInformationVIewModel()
        {

            //LoadingData();

        }

        public async Task LoadData(int idSupplier)
        {
            Loading ld = new Loading();
            ld.Show();
            SingleSupplierResponse res = (await APIService.ins.getSuppliersById(idSupplier));
            supplierInfo = res.data;
            supplierService = new ObservableCollection<SERVICE>();
            List<SUPPLIERSERVICE> supplierServicess = await APIService.ins.getAllSupplierServices();
            foreach (SUPPLIERSERVICE service in supplierServicess)
            {
                if (service.idSupplier == idSupplier)
                {
                    supplierService.Add(service.SERVICE);
                }
            }
            ld.Close();

        }
    }
}
