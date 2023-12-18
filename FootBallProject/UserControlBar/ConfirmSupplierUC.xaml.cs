using FootBallProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FootBallProject.UserControlBar
{
    /// <summary>
    /// Interaction logic for ConfirmSupplierUC.xaml
    /// </summary>
    public partial class ConfirmSupplierUC : UserControl
    {
        public ConfirmSupplierUC()
        {
            InitializeComponent();


            Load();
        }

        public void Load()
        {
            var list = DataProvider.ins.DB.DOIBONGSUPPLIERs.ToList();
            List<ClubSupplier> clubSuppliers = new List<ClubSupplier>();
            foreach (var item in list)
            {
                var club = DataProvider.ins.DB.DOIBONGs.FirstOrDefault(x => x.ID.Equals(item.idDoiBong));
                var sup = DataProvider.ins.DB.SUPPLIERs.Find(item.idSupplier);
                if (club != null && sup != null && item.status == 1)
                {

                    clubSuppliers.Add(new ClubSupplier(club.TEN, sup.supplierName, (DateTime)item.startDate, (DateTime)item.endDate, (int)item.duration, item.idDoiBong, item.idSupplier, (int)item.status));
                }
            }
            fromSupDTG.ItemsSource = clubSuppliers;
        }

        private void fromClubDTG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DataGrid dg = sender as DataGrid;
            //ClubSupplier clubSupplier = (ClubSupplier)dg.SelectedItem;
            //if (clubSupplier == null)
            //{
            //    Error error = new Error("");
            //    error.ShowDialog();
            //    return;
            //}
            //SupplierConfirm supplierConfirm = new SupplierConfirm();
            //supplierConfirm.Init(clubSupplier.Club, clubSupplier.Supplier, clubSupplier.startDate, clubSupplier.endDate, clubSupplier.duration);
            //supplierConfirm.idDoibong = clubSupplier.idDoibong;
            //supplierConfirm.idSup = clubSupplier.idSup;
            //supplierConfirm.ShowDialog();
        }

        private void fromSupDTG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            ClubSupplier clubSupplier = (ClubSupplier)dg.SelectedItem;
            if (clubSupplier == null)
            {
                //Error error = new Error("");
                //error.ShowDialog();
                return;
            }
            SupplierConfirm supplierConfirm = new SupplierConfirm();
            supplierConfirm.Init(clubSupplier.Club, clubSupplier.Supplier, clubSupplier.startDate, clubSupplier.endDate, clubSupplier.duration, clubSupplier.idDoibong, clubSupplier.idSup, clubSupplier.status);
            supplierConfirm.idDoibong = clubSupplier.idDoibong;
            supplierConfirm.idSup = clubSupplier.idSup;
            supplierConfirm.ShowDialog();
            Load();
        }
    }
    public class ClubSupplier
    {
        public string idDoibong { get; set; }
        public int idSup { get; set; }
        public string Club { get; set; }
        public string Supplier { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int duration { get; set; }
        public int status { get; set; }
        public ClubSupplier(string CLub, string Supplier, DateTime startDate, DateTime endDate, int duration, string idDoibong, int idSup, int status)
        {
            this.Club = CLub;
            this.Supplier = Supplier;
            this.startDate = startDate;
            this.endDate = endDate;
            this.duration = duration;
            this.idDoibong = idDoibong;
            this.idSup = idSup;
            this.status = status;
        }
    }
}
