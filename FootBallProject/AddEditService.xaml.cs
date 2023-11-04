using FootBallProject.Model;
using FootBallProject.PopUp;
using FootBallProject.Service;
using FootBallProject.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for AddEditService.xaml
    /// </summary>
    public partial class AddEditService : Window
    {
        public SERVICE editService { get; set; }
        public SUPPLIER supplier { get; set; }
        public int isChange = 0;
        public string type = "create";
        public AddEditService()
        {
            InitializeComponent();
        }
        public AddEditService(SERVICE service,SUPPLIER supplier, string type = "create")
        {
            InitializeComponent();
            this.editService = service;
            this.type = type;
            this.supplier = supplier;
            ServiceNametbl.Text = service.serviceName;
            ServiceDetailtbl.Text = service.detail;
            if (service.images != null && service.images.Length != 0)
            {
                logoImage.ImageSource = LoadImage(service.images);

            }
            titletbl.Text = "Cập nhật thông tin dịch vụ";
            UpdateInformationBtn.Content = "Cập nhật";
        }
        public AddEditService(SUPPLIER supplier, string type = "create")
        {
            InitializeComponent();
            this.type = type;
            this.supplier = supplier;
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
            if (ServiceNametbl.Text == "" || ServiceDetailtbl.Text == "" )
            {
                Error error = new Error("Chưa đủ thông tin");
                error.ShowDialog();
            }
           
            else
            {
                //Update to API
                if(type == "create")
                {
                    //addd
                    Loading loading = new Loading();

                    byte[] image = convertBitMapToByeArray(logoImage.ImageSource);
                    SERVICE newService = new SERVICE() { serviceName = ServiceNametbl.Text, detail = ServiceDetailtbl.Text, images =image  };
                    //add by API
                    try
                    {
                        loading.Show();
                        SERVICE res = await APIService.ins.postNewServices(newService);
                        SUPPLIERSERVICE newSupplierService = new SUPPLIERSERVICE() { idService = res.idService, idSupplier = this.supplier.idSupplier, status = 0 };
                        await APIService.ins.postNewSupplierService(newSupplierService);
                        isChange = 1;
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Error");
                    }
                    loading.Close();

                }
                else
                {
                    //update
                    this.editService.serviceName = ServiceNametbl.Text; 
                    this.editService.detail= ServiceDetailtbl.Text;
                    this.editService.images = convertBitMapToByeArray(logoImage.ImageSource);
                    //uppdate API
                    await APIService.ins.updateService(this.editService.idService,this.editService);
                    isChange = 1;

                }
                this.Close();   
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
        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
