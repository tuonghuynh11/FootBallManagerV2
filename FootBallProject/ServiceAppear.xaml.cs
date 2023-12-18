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
    /// Interaction logic for ServiceAppear.xaml
    /// </summary>
    public partial class ServiceAppear : Window
    {
        private readonly ObservableCollection<SERVICE> _service;
        private readonly SERVICE _selectedService;
        public ServiceAppear(ObservableCollection<SERVICE> sERVICEs, SERVICE sERVICE)
        {
            InitializeComponent();
            _service = sERVICEs;
            _selectedService = sERVICE;
            if (_selectedService != null)
            {

                byte[] pic;
                if (_selectedService.images == null)
                {
                    pic = null;
                }
                else if (_selectedService.images.ToString() == "")
                {
                    pic = null;
                }
                else
                {
                    pic = (byte[])_selectedService.images;
                }
                if (pic != null)
                {
                    using (var stream = new MemoryStream(pic))
                    {
                        stadiumImage.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    }
                }
            }

        }

        public void Init(string name, string detail)
        {
            tbName.Text = name;
            tbDetail.Text = detail;
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

        private void Close_but_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void editStadium_Click(object sender, RoutedEventArgs e)
        {
            Upload.Visibility = Visibility.Visible;
            cancelStadium.Visibility = Visibility.Visible;
            saveStadium.Visibility = Visibility.Visible;
            editStadium.Visibility = Visibility.Hidden;
            tbDetail.IsReadOnly = false;
            tbName.IsReadOnly = false;
        }

        private void cancelStadium_Click(object sender, RoutedEventArgs e)
        {
            Upload.Visibility = Visibility.Hidden;
            cancelStadium.Visibility = Visibility.Hidden;
            saveStadium.Visibility = Visibility.Hidden;
            editStadium.Visibility = Visibility.Visible;
            tbDetail.IsReadOnly = true;
            tbName.IsReadOnly = true;
        }

        private void saveStadium_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedService != null)
            {
                _selectedService.detail = tbDetail.Text;
                _selectedService.serviceName = tbName.Text;
                Success success = new Success();
                success.ShowDialog();
            }
        }

        private void deleteStadium_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedService != null)
            {
                _service.Remove(_selectedService);
                Success success = new Success();
                success.ShowDialog();
                this.Close();
            }
        }
    }
}
