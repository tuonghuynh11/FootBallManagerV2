using FootBallProject.Class;
using FootBallProject.Model;
using FootBallProject.PopUp;
using FootBallProject.Service;
using FootBallProject.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for EditSupplierInformation.xaml
    /// </summary>
    public partial class EditSupplierInformation : Window
    {
        public int isChange = 0;
        public EditSupplierInformation()
        {
            InitializeComponent();
        }
        public EditSupplierInformation(int idSupplier)
        {
            InitializeComponent();
           
            (this.DataContext as SupplierInformationVIewModel).LoadData(idSupplier);
        }



        private T FindAncestor<T>(DependencyObject current)
  where T : DependencyObject
        {
            do
            {
                if (current is T target)
                {
                    return target;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private async void deleteServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ListViewItem listViewItem = FindAncestor<ListViewItem>(button);

            // Check if a ListViewItem was found
            if (listViewItem != null)
            {
                // Select the ListViewItem
                lsService.SelectedItems.Clear();
                listViewItem.IsSelected = true;
            }
            SERVICE service = lsService.SelectedItem as SERVICE;
            SUPPLIER supplier = (this.DataContext as SupplierInformationVIewModel).supplierInfo;
            OKCancelPopUp oKCancelPopUp = new OKCancelPopUp(contents:"Xóa dịch vụ");
            oKCancelPopUp.ShowDialog();
            if (oKCancelPopUp.Ok==0)
            {
                return;
            }
            else
            {
                //Delete API
                Loading loading = new Loading();
                try
                {
                    loading.Show();
                    await APIService.ins.deleteSupplerService(service.idService, supplier.idSupplier);
                    await APIService.ins.deleteServices(service.idService);
                }
                catch (Exception ex)
                {

                    Error error = new Error(ex.Message);
                    error.Show();
                }
                loading.Close();

                (this.DataContext as SupplierInformationVIewModel).supplierService.Remove(service);
                //isChange = 1;
            }

        }

        private async void editServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ListViewItem listViewItem = FindAncestor<ListViewItem>(button);

            // Check if a ListViewItem was found
            if (listViewItem != null)
            {
                // Select the ListViewItem
                lsService.SelectedItems.Clear();
                listViewItem.IsSelected = true;
            }
            SERVICE service = lsService.SelectedItem as SERVICE;
            AddEditService addEditService = new AddEditService(service, (this.DataContext as SupplierInformationVIewModel).supplierInfo, "edit");
            addEditService.ShowDialog();
            if (addEditService.isChange == 1)
            {
                Loading ld= new Loading();
                ld.Show();
                await (this.DataContext as SupplierInformationVIewModel).LoadData(AccessUser.userLogin.IDSUPPLIER);
                ld.Close();
                isChange = 1;

            }
        }

        private  async void addNewServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEditService addEditService = new AddEditService( (this.DataContext as SupplierInformationVIewModel).supplierInfo, "create");
            addEditService.ShowDialog();
            if (addEditService.isChange == 1)
            {
                Loading ld = new Loading();
                ld.Show();
                await (this.DataContext as SupplierInformationVIewModel).LoadData(AccessUser.userLogin.IDSUPPLIER);
                ld.Close();
                isChange = 1;


            }
        }

        private void uploadImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                logoImage.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void UpdateInformationBtn_Click(object sender, RoutedEventArgs e)
        {
            Regex phoneNumberReg = new Regex(@"^(0[1-9]|84[1-9])(\d{8,9})$");
            if(Nametbl.Text==""||Addresstbl.Text==""||PhoneNumbertbl.Text==""||etsdp.ToString()==""|| Representertbl.Text == "")
            {
                Error error = new Error("Chưa đủ thông tin");
                error.ShowDialog();
            }
            else if (!phoneNumberReg.IsMatch(PhoneNumbertbl.Text.Trim())){
                Error error = new Error("Số điện thoại không hợp lệ");
                error.ShowDialog();
            }
            else if (etsdp.SelectedDate > DateTime.Now)
            {
                Error error = new Error("Ngày thành lập không hợp lệ");
                error.ShowDialog();
            }
            else
            {
                //Update to API
                SUPPLIER updateSupplier = (this.DataContext as SupplierInformationVIewModel).supplierInfo;
                updateSupplier.supplierName= Nametbl.Text;
                updateSupplier.addresss = Addresstbl.Text;
                updateSupplier.phoneNumber = PhoneNumbertbl.Text;
                updateSupplier.representativeName = Representertbl.Text;
                updateSupplier.establishDate = etsdp.SelectedDate;
                updateSupplier.images = convertBitMapToByeArray(logoImage.ImageSource);
                try
                {
                    await APIService.ins.updateSupplier(updateSupplier.idSupplier, updateSupplier);
                    Success success = new Success();
                    success.Show();
                    this.Close();
                }
                catch (Exception ex)
                {

                    Error error = new Error(ex.Message);
                    error.Show();
                }

            }
        }

        public byte[] convertBitMapToByeArray(ImageSource source)
        {
            byte[] buffer;
            var bitmap = source as BitmapSource;
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (var stream = new MemoryStream())
            {
                encoder.Save(stream);
                buffer = stream.ToArray();
            }
            return buffer;
        }
    }
}
