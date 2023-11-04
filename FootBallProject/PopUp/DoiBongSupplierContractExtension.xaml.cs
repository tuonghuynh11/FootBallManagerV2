using FootBallProject.Model;
using FootBallProject.RequestModel;
using FootBallProject.Service;
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

namespace FootBallProject.PopUp
{
    /// <summary>
    /// Interaction logic for DoiBongSupplierContractExtension.xaml
    /// </summary>
    public partial class DoiBongSupplierContractExtension : Window
    {
        List<String> times = new List<String>() {"1 năm", "2 năm","3 năm","4 năm","5 năm" };
        public string idDoiBong { get; set; }
        public int idSupplier { get; set; }
        public string type { get; set; }
        public DOIBONGSUPPLIER doiBongSupplier { get; set; }
        public DoiBongSupplierContractExtension()
        {
            InitializeComponent();
            cbTimeExtension.ItemsSource = times;
            cbTimeExtension.SelectedItem= times[0];
        }

        public DoiBongSupplierContractExtension(string idDoiBong, int idSupplier, string type = "create", DOIBONGSUPPLIER doiBongSupplier = null)
        {
            InitializeComponent();
            cbTimeExtension.ItemsSource = times;
            cbTimeExtension.SelectedItem = times[0];
            this.idDoiBong = idDoiBong;
            this.idSupplier = idSupplier;
            this.type = type;
            this.doiBongSupplier = doiBongSupplier;
            if(doiBongSupplier != null)
            {
                content.Text = "Thời hạn đến " + ((DateTime)doiBongSupplier.startDate).AddYears((int)(1 +doiBongSupplier.duration)).ToString("MM/dd/yyyy");
            }
            if(type == "create")
            {
                titletxbl.Text = "Gửi yêu cầu hợp tác";
            }
        }

        private async void Okbtt_Click(object sender, RoutedEventArgs e)
        {
            int _duration = int.Parse(cbTimeExtension.SelectedItem.ToString().Substring(0, 1));
            DoiBongSupplierRequest ds = new DoiBongSupplierRequest() { idDoiBong = this.idDoiBong, idSupplier = this.idSupplier, startDate = DateTime.Now, endDate = DateTime.Now.AddYears(_duration), duration = _duration, status = 0 };
            Loading ld = new Loading();
            Success success = new Success();

            if (this.type != "create")
            {
                try
                {
                    int moreYears = (int)(_duration + (this.doiBongSupplier.duration));

                    DateTime newDate = this.doiBongSupplier.startDate.Value.AddYears(moreYears);
                    DoiBongSupplierRequest ds1 = new DoiBongSupplierRequest() { idDoiBong = this.idDoiBong, idSupplier = this.idSupplier, startDate = this.doiBongSupplier.startDate, endDate = newDate, duration = _duration + this.doiBongSupplier.duration, status = 4 };
                    await APIService.ins.patchDoiBongSupplier(ds1);
                    ld.Close();
                    this.Close();
                    success.ShowDialog();

                }
                catch (Exception ex)
                {

                   Error error = new Error(ex.Message);
                    error.Show();
                }
             
                return;
            }
        
            ld.Show();
            await APIService.ins.postNewDoiBongSupplier(ds);
            ld.Close();
            this.Close();
            success.ShowDialog();
        }

        private void Cancelbtt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbTimeExtension_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem!=null)
            {
                DateTime originalDate = DateTime.Now;
                if(this.type != "create" && this.doiBongSupplier!=null)
                {
                    DateTime originalDate1 = (DateTime)this.doiBongSupplier.startDate;

                    content.Text = "Thời hạn đến " + originalDate1.AddYears((int)(this.doiBongSupplier.duration+int.Parse(comboBox.SelectedItem.ToString().Substring(0, 1)))).ToString("MM/dd/yyyy");

                    return; 
                }
                content.Text = "Thời hạn đến " + originalDate.AddYears(int.Parse(comboBox.SelectedItem.ToString().Substring(0, 1))).ToString("MM/dd/yyyy");
            }
         
        }
    }
}
