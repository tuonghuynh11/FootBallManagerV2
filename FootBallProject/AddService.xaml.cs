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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for AddService.xaml
    /// </summary>
    public partial class AddService : Window
    {
        private readonly ObservableCollection<SERVICE> _service;
        public AddService(ObservableCollection<SERVICE> sERVICEs)
        {
            InitializeComponent();
            _service = sERVICEs;
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

        private void addStadium_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                byte[] buffer;
                var bitmap = stadiumImage.Source as BitmapSource;
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    buffer = stream.ToArray();
                }
                _service.Add(new SERVICE { serviceName = tbName.Text, detail = tbDetail.Text, images = buffer });
                Success success = new Success();
                success.ShowDialog();
                this.Close();

            }
            catch (Exception ex)
            {
                Error error = new Error("Đã có lỗi xảy ra");
                error.ShowDialog();
            }
        }
    }
}
