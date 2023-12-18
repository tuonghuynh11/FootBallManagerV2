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
using System.Windows.Shapes;

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for SupplierConfirm.xaml
    /// </summary>
    public partial class SupplierConfirm : Window
    {
        public string idDoibong;
        public int idSup;
        public SupplierConfirm()
        {
            InitializeComponent();
        }
        public void Init(string CLub, string Supplier, DateTime startDate, DateTime endDate, int duration, string idDoibong, int idSup, int status)
        {
            tbClub.Text = CLub;
            tbSup.Text = Supplier;
            tbstart.Text = startDate.ToString();
            tbend.Text = endDate.ToString();
            tbduraion.Text = duration.ToString();
        }

        private void Close_but_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void yesBtt_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var item = DataProvider.ins.DB.DOIBONGSUPPLIERs.FirstOrDefault(x => x.idSupplier == idSup && x.idDoiBong.Equals(idDoibong));
                if (item != null)
                {
                    item.status = 2;
                    DataProvider.ins.DB.SaveChanges();
                    Success success = new Success();
                    success.ShowDialog();
                    this.Close();
                }
            }
            catch
            {
                Error error = new Error("Đã có lỗi xảy ra");
                error.ShowDialog();
            }
        }

        private void NoBtt_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var item = DataProvider.ins.DB.DOIBONGSUPPLIERs.FirstOrDefault(x => x.idSupplier == idSup && x.idDoiBong.Equals(idDoibong));
                if (item != null)
                {
                    item.status = 6;
                    DataProvider.ins.DB.SaveChanges();
                    Success success = new Success();
                    success.ShowDialog();
                    this.Close();
                }
            }
            catch
            {
                Error error = new Error("Đã có lỗi xảy ra");
                error.ShowDialog();
            }
        }
    }
}
