using FootBallProject.Model;
using FootBallProject.UserControlBar;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for AddSupplier.xaml
    /// </summary>
    public partial class AddSupplier : Window
    {
        public ObservableCollection<SERVICE> sERVICEs = new ObservableCollection<SERVICE>();
        public AddSupplier()
        {
            InitializeComponent();
            dtgSer.ItemsSource = sERVICEs;
        }

        private void Close_but_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                stadiumImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {

            AddService addService = new AddService(sERVICEs);
            addService.ShowDialog();

        }

        private void dtgSer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            SERVICE ser = dg.SelectedItem as SERVICE;
            if (ser == null)
            {
                //Error error = new Error("");
                //error.ShowDialog();
                return;
            }
            ServiceAppear serviceAppear = new ServiceAppear(sERVICEs, ser);
            serviceAppear.Init(ser.serviceName, ser.detail);
            serviceAppear.ShowDialog();
        }

        private void addStadium_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbAddress.Text == "" || tbName.Text == "" || tbNameDD.Text == "" || tbphone.Text == "" || dpntl.SelectedDate == null)
                {
                    Error error = new Error("Các trường không được bỏ trống");
                    error.ShowDialog();
                    return;
                }
                string phoneNumber = tbphone.Text;

                // Define the regex pattern for a Vietnamese phone number
                string pattern = @"(84|0[3|5|7|8|9])+([0-9]{8})\b";

                // Create a regex object and check if the input matches the pattern
                Regex regex = new Regex(pattern);
                bool isMatch = regex.IsMatch(phoneNumber);

                if (!isMatch)
                {
                    Error error = new Error("Số điện thoại không hợp lệ");
                    error.ShowDialog();
                    return;
                }

                byte[] buffer;
                var bitmap = stadiumImage.Source as BitmapSource;
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    buffer = stream.ToArray();
                }
                SUPPLIER sUPPLIER = new SUPPLIER
                {
                    supplierName = tbName.Text,
                    addresss = tbAddress.Text,
                    phoneNumber = tbphone.Text,
                    representativeName = tbNameDD.Text,
                    establishDate = dpntl.SelectedDate,
                    images = buffer
                };
                DataProvider.ins.DB.SUPPLIERs.Add(sUPPLIER);
                DataProvider.ins.DB.SaveChanges();
                int supplierId = sUPPLIER.idSupplier;
                DataProvider.ins.DB.SERVICES.AddRange(sERVICEs);
                DataProvider.ins.DB.SaveChanges();
                List<int> serviceIds = sERVICEs.Select(s => s.idService).ToList();
                foreach (int serviceId in serviceIds)
                {
                    SUPPLIERSERVICE sUPPLIERSERVICE = new SUPPLIERSERVICE
                    {
                        idService = serviceId,
                        idSupplier = supplierId,
                    };

                    DataProvider.ins.DB.SUPPLIERSERVICEs.Add(sUPPLIERSERVICE);
                }
                DataProvider.ins.DB.SaveChanges();
                Success success = new Success();
                success.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                Error error = new Error("Đã có lỗi xảy ra!");
                error.ShowDialog();
            }
        }
    }
}
