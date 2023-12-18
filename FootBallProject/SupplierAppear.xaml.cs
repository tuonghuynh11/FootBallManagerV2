using FootBallProject.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for SupplierAppear.xaml
    /// </summary>
    public partial class SupplierAppear : Window
    {
        public int id;
        private readonly SUPPLIER _supplier;
        ObservableCollection<SERVICE> services = new ObservableCollection<SERVICE>();
        public SupplierAppear(SUPPLIER sUPPLIER)
        {
            InitializeComponent();
            _supplier = sUPPLIER;
            if (_supplier != null)
            {

                var list = DataProvider.ins.DB.SUPPLIERSERVICEs.Where(s => s.idSupplier == _supplier.idSupplier).ToList();

                foreach (var item in list)
                {
                    var service = DataProvider.ins.DB.SERVICES.Find(item.idService);
                    if (service != null)
                    {
                        services.Add(service);
                    }
                }
                dtgSer.ItemsSource = services;
            }
        }

        private void Close_but_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void Init(string name, DateTime esDate, string address, string phone, string repname, byte[] img)
        {
            tbname.Text = name;
            tbadress.Text = address;
            tbphone.Text = phone;
            tbrep.Text = repname;
            string temp = esDate.ToString();
            temp = temp.Substring(0, temp.IndexOf(" "));
            tbdate.Text = temp;
            dpns.SelectedDate = esDate;
            byte[] pic;
            if (img == null)
            {
                pic = null;
            }
            else if (img.ToString() == "")
            {
                pic = null;
            }
            else
            {
                pic = (byte[])img;
            }
            if (pic != null)
            {
                using (var stream = new MemoryStream(pic))
                {
                    bldimg.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                }
            }
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            AddService addService = new AddService(services);
            addService.ShowDialog();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var supplier = DataProvider.ins.DB.SUPPLIERs.FirstOrDefault(s => s.idSupplier == _supplier.idSupplier);
                var listsupser = DataProvider.ins.DB.SUPPLIERSERVICEs.Where(s => s.idSupplier == _supplier.idSupplier).ToList();
                if (listsupser != null)
                {
                    List<SERVICE> list = new List<SERVICE>();
                    foreach (var s in listsupser)
                    {
                        var service = DataProvider.ins.DB.SERVICES.Find(s.idService);
                        if (service != null)
                        {
                            list.Add(service);
                        }
                    }
                    foreach (var sup in listsupser)
                    {
                        DataProvider.ins.DB.SUPPLIERSERVICEs.Remove(sup);
                    }
                    DataProvider.ins.DB.SaveChanges();
                    foreach (var service in list)
                    {
                        DataProvider.ins.DB.SERVICES.Remove(service);
                    }
                    DataProvider.ins.DB.SaveChanges();
                }
                if (supplier != null)
                {
                    DataProvider.ins.DB.SUPPLIERs.Remove(supplier);
                    DataProvider.ins.DB.SaveChanges();
                    Success success = new Success();
                    success.ShowDialog();
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                Error error = new Error("Đã có lỗi xảy ra");
                error.ShowDialog();
            }
        }

        private void editbtt_Click(object sender, RoutedEventArgs e)
        {
            editbtt.Visibility = Visibility.Hidden;
            delete.Visibility = Visibility.Hidden;
            tbadress.IsReadOnly = false;
            tbphone.IsReadOnly = false;
            tbname.IsReadOnly = false;
            tbrep.IsReadOnly = false;
            dpns.Visibility = Visibility.Visible;
            savebtt.Visibility = Visibility.Visible;
            cancelbtt.Visibility = Visibility.Visible;
            uploadbtt.Visibility = Visibility.Visible;
            AddService.Visibility = Visibility.Visible;
        }

        private void uploadbtt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                bldimg.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void savebtt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbadress.Text == "" || tbname.Text == "" || tbrep.Text == "" || tbphone.Text == "" || dpns.SelectedDate == null)
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
                var supplier = DataProvider.ins.DB.SUPPLIERs.FirstOrDefault(s => s.idSupplier == _supplier.idSupplier);
                var listsupser = DataProvider.ins.DB.SUPPLIERSERVICEs.Where(s => s.idSupplier == _supplier.idSupplier).ToList();
                if (listsupser != null)
                {
                    List<SERVICE> list = new List<SERVICE>();
                    foreach (var s in listsupser)
                    {
                        var service = DataProvider.ins.DB.SERVICES.Find(s.idService);
                        if (service != null)
                        {
                            list.Add(service);
                        }
                    }
                    foreach (var sup in listsupser)
                    {
                        DataProvider.ins.DB.SUPPLIERSERVICEs.Remove(sup);
                    }
                    DataProvider.ins.DB.SaveChanges();
                    foreach (var service in list)
                    {
                        DataProvider.ins.DB.SERVICES.Remove(service);
                    }
                    DataProvider.ins.DB.SaveChanges();
                }
                if (supplier != null)
                {
                    DataProvider.ins.DB.SERVICES.AddRange(services);
                    DataProvider.ins.DB.SaveChanges();
                    List<int> serviceIds = services.Select(s => s.idService).ToList();
                    foreach (int serviceId in serviceIds)
                    {
                        SUPPLIERSERVICE sUPPLIERSERVICE = new SUPPLIERSERVICE
                        {
                            idService = serviceId,
                            idSupplier = _supplier.idSupplier,
                        };

                        DataProvider.ins.DB.SUPPLIERSERVICEs.Add(sUPPLIERSERVICE);
                    }
                    DataProvider.ins.DB.SaveChanges();
                    supplier.representativeName = tbrep.Text;
                    supplier.addresss = tbadress.Text;
                    supplier.phoneNumber = tbphone.Text;
                    supplier.supplierName = tbname.Text;
                    supplier.establishDate = dpns.SelectedDate;
                    byte[] buffer;
                    var bitmap = bldimg.Source as BitmapSource;
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmap));

                    using (var stream = new MemoryStream())
                    {
                        encoder.Save(stream);
                        buffer = stream.ToArray();
                    }

                    supplier.images = buffer;
                    DataProvider.ins.DB.SaveChanges();
                    Success success = new Success();
                    success.ShowDialog();

                    editbtt.Visibility = Visibility.Visible;
                    delete.Visibility = Visibility.Visible;
                    tbadress.IsReadOnly = true;
                    tbphone.IsReadOnly = true;
                    tbname.IsReadOnly = true;
                    tbrep.IsReadOnly = true;
                    dpns.Visibility = Visibility.Hidden;
                    savebtt.Visibility = Visibility.Hidden;
                    cancelbtt.Visibility = Visibility.Hidden;
                    uploadbtt.Visibility = Visibility.Hidden;
                    AddService.Visibility = Visibility.Hidden;
                    string[] part = dpns.SelectedDate.ToString().Split(' ');
                    tbdate.Text = part[0];

                }
            }
            catch (Exception ex)
            {
                Error error = new Error("Đã có lỗi xảy ra");
                error.ShowDialog();
            }
        }

        private void cancelbtt_Click(object sender, RoutedEventArgs e)
        {
            editbtt.Visibility = Visibility.Visible;
            delete.Visibility = Visibility.Visible;
            tbadress.IsReadOnly = true;
            tbphone.IsReadOnly = true;
            tbname.IsReadOnly = true;
            tbrep.IsReadOnly = true;
            dpns.Visibility = Visibility.Hidden;
            savebtt.Visibility = Visibility.Hidden;
            cancelbtt.Visibility = Visibility.Hidden;
            uploadbtt.Visibility = Visibility.Hidden;
            AddService.Visibility = Visibility.Hidden;
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
            ServiceAppear serviceAppear = new ServiceAppear(services, ser);
            serviceAppear.Init(ser.serviceName, ser.detail);
            serviceAppear.ShowDialog();
        }
    }
}
