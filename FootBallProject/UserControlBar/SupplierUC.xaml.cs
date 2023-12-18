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
    /// Interaction logic for SupplierUC.xaml
    /// </summary>
    public partial class SupplierUC : UserControl
    {
        public SupplierUC()
        {
            InitializeComponent();
            //List<supplier> suppliers = new List<supplier> { 
            //    new supplier(1, "abc", new DateTime(2023,1,1), "agh", "01111111", "ABC"),
            //    new supplier(1, "abc", new DateTime(2023,1,1), "agh", "01111111", "ABC"),
            //    new supplier(1, "abc", new DateTime(2023,1,1), "agh", "01111111", "ABC"),
            //    new supplier(1, "abc", new DateTime(2023,1,1), "agh", "01111111", "ABC"),
            //    new supplier(1, "abc", new DateTime(2023,1,1), "agh", "01111111", "ABC"),
            //    new supplier(1, "abc", new DateTime(2023,1,1), "agh", "01111111", "ABC"),
            //};
            //DTG.ItemsSource = suppliers;
            Load();
        }

        public void Load()
        {
            var list = DataProvider.ins.DB.SUPPLIERs.ToList();
            foreach (var item in list)
            {

            }
            DTG.ItemsSource = list;
        }

        private void AddNewUser_Click(object sender, RoutedEventArgs e)
        {
            AddSupAccount addSupplierAccount = new AddSupAccount();
            addSupplierAccount.ShowDialog();
        }

        private void AddNewPerson_Click(object sender, RoutedEventArgs e)
        {
            AddSupplier addSupplier = new AddSupplier();
            addSupplier.ShowDialog();
        }

        private void DTG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            SUPPLIER sup = dg.SelectedItem as SUPPLIER;
            if (sup == null)
            {
                //Error error = new Error("");
                //error.ShowDialog();
                return;
            }
            SupplierAppear supplierAppear = new SupplierAppear(sup);
            supplierAppear.id = sup.idSupplier;
            supplierAppear.Init(sup.supplierName, (DateTime)sup.establishDate, sup.addresss, sup.phoneNumber, sup.representativeName, sup.images);
            supplierAppear.ShowDialog();
            Load();
        }
    }
    public class supplier
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime EstablishDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string RepName { get; set; }
        public ImageSource img { get; set; }
        public supplier(int iD, string name, DateTime establishDate, string address, string phone, string repName)
        {
            ID = iD;
            Name = name;
            EstablishDate = establishDate;
            Address = address;
            Phone = phone;
            RepName = repName;
        }
    }
}
