using FootBallProject.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
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
using static FootBallProject.UserControlBar.UserControl_DS_BLD;

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for AddStadium.xaml
    /// </summary>
    public partial class AddStadium : Window
    {
        public AddStadium()
        {
            InitializeComponent();
            List<String> places = new List<String>();
            var list = DataProvider.ins.DB.QUOCTICHes.ToList();
            foreach (var item in list)
            {
                places.Add(item.ID.ToString() + ". " + item.TENQUOCGIA);
            }
            cbCountry.ItemsSource = places;

            //var tst = DataProvider.ins.DB.FIELDs.Find(2);
            //byte[] pic;
            //if (tst.images.ToString() == "")
            //{
            //    pic = null;
            //}
            //else
            //{
            //    pic = (byte[])tst.images;
            //}
            //if (pic != null)
            //{
            //    using (var stream = new MemoryStream(pic))
            //    {
            //        stadiumImage.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            //    }
            //}

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

        private void cbCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCountry.SelectedItem != null)
            {
                string[] parts = cbCountry.SelectedItem.ToString().Split('.');
                string temp = parts[0].Trim();
                int res = Convert.ToInt32(temp);
                var list = DataProvider.ins.DB.DIADIEMs.Where(d => d.IDQUOCGIA == res).ToList();
                List<String> places = new List<String>();
                foreach (var item in list)
                {
                    places.Add(item.ID + ". " + item.TENDIADIEM);
                }
                cbLocation.ItemsSource = places;
            }
        }

        private void addStadium_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbLocation.Text == "" || tbName.Text == "" || tbSeat.Text == "" || tbTech.Text == "")
                {
                    Error error = new Error("Các trường không được bỏ trống!");
                    error.ShowDialog();
                    return;
                }
                if (!int.TryParse(tbSeat.Text, out int result))
                {
                    Error error = new Error("Số ghế không hợp lệ!");
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
                string[] parts = cbLocation.Text.ToString().Split('.');
                string temp = parts[0].Trim();
                FIELD fIELD = new FIELD
                {
                    idDiaDiem = Convert.ToInt32(temp),
                    fieldName = tbName.Text,
                    numOfSeats = Convert.ToInt32(tbSeat.Text),
                    technicalInformation = tbTech.Text,
                    status = 0,
                    images = buffer
                };
                DataProvider.ins.DB.FIELDs.Add(fIELD);
                DataProvider.ins.DB.SaveChanges();
                Success success = new Success();
                success.ShowDialog();
                this.Close();
            }
            catch
            {
                Error error = new Error("Đã xảy ra lỗi !");
                error.ShowDialog();
            }
        }
    }
}
