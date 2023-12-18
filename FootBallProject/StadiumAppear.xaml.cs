using FootBallProject.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for StadiumAppear.xaml
    /// </summary>
    public partial class StadiumAppear : System.Windows.Window
    {
        public int id;
        string locationLocal;
        string nameLocal;
        int seatsLocal;
        string techLocal;
        string countryLocal;
        public StadiumAppear()
        {
            InitializeComponent();
            List<String> places = new List<String>();
            var list = DataProvider.ins.DB.QUOCTICHes.ToList();
            foreach (var item in list)
            {
                places.Add(item.ID.ToString() + ". " + item.TENQUOCGIA);
            }
            cbCountry.ItemsSource = places;
        }
        public void Init(string Location, string Name, int Seats, string Tech, byte[] img, string country)
        {
            tbLocation.Text = Location;
            tbCountry.Text = country;
            locationLocal = Location;
            tbname.Text = Name;
            nameLocal = Name;
            tbSeat.Text = Seats.ToString();
            seatsLocal = Seats;
            tbTech.Text = Tech.ToString();
            techLocal = Tech;
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

        private void Close_but_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            tbLocation.Visibility = Visibility.Hidden;
            cbLocation.Visibility = Visibility.Visible;
            cbCountry.Visibility = Visibility.Visible;
            tbCountry.Visibility = Visibility.Hidden;
            tbname.IsReadOnly = false;
            tbSeat.IsReadOnly = false;
            tbTech.IsReadOnly = false;
            Upload.Visibility = Visibility.Visible;
            Save.Visibility = Visibility.Visible;
            Cancel.Visibility = Visibility.Visible;
            Edit.Visibility = Visibility.Hidden;
            Delete.Visibility = Visibility.Hidden;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            tbLocation.Visibility = Visibility.Visible;
            tbCountry.Visibility = Visibility.Visible;
            cbLocation.Visibility = Visibility.Hidden;
            cbCountry.Visibility = Visibility.Hidden;
            tbname.IsReadOnly = true;
            tbSeat.IsReadOnly = true;
            tbTech.IsReadOnly = true;
            Upload.Visibility = Visibility.Hidden;
            Save.Visibility = Visibility.Hidden;
            Cancel.Visibility = Visibility.Hidden;
            Edit.Visibility = Visibility.Visible;
            Delete.Visibility = Visibility.Visible;
            tbLocation.Text = locationLocal;
            tbname.Text = nameLocal;
            tbSeat.Text = seatsLocal.ToString();
            tbTech.Text = techLocal;
        }

        private void Edit2_Click(object sender, RoutedEventArgs e)
        {
            //Save2.Visibility = Visibility.Visible;
            //Cancel2.Visibility = Visibility.Visible;
            //Edit2.Visibility = Visibility.Hidden;
            //cbLeague.Visibility = Visibility.Visible;
            //cbMatch.Visibility = Visibility.Visible;
            //cbRound.Visibility = Visibility.Visible;
            //dpntd.Visibility = Visibility.Visible;
            //PresetTimePicker.Visibility = Visibility.Visible;
            //tbTime.Visibility = Visibility.Hidden;
        }

        private void Cancel2_Click_1(object sender, RoutedEventArgs e)
        {
            //Save2.Visibility = Visibility.Hidden;
            //Cancel2.Visibility = Visibility.Hidden;
            //Edit2.Visibility = Visibility.Visible;
            //cbLeague.Visibility = Visibility.Hidden;
            //cbMatch.Visibility = Visibility.Hidden;
            //cbRound.Visibility = Visibility.Hidden;
            //dpntd.Visibility = Visibility.Hidden;
            //PresetTimePicker.Visibility = Visibility.Hidden;
            //tbTime.Visibility = Visibility.Visible;
        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                bldimg.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FIELD fIELD = DataProvider.ins.DB.FIELDs.Find(id);
                DataProvider.ins.DB.FIELDs.Remove(fIELD);
                DataProvider.ins.DB.SaveChanges();
                Success success = new Success();
                success.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                Error error = new Error("Đã xảy ra lỗi");
                error.ShowDialog();
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbLocation.Text == "" || tbname.Text == "" || tbSeat.Text == "" || tbTech.Text == "")
                {
                    Error error = new Error("Các trường không được bỏ trống");
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
                var bitmap = bldimg.Source as BitmapSource;
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    buffer = stream.ToArray();
                }
                string[] parts = cbLocation.Text.ToString().Split('.');
                string temp = parts[0].Trim();
                FIELD field = DataProvider.ins.DB.FIELDs.Find(id);
                if (field != null)
                {
                    field.idDiaDiem = Convert.ToInt32(temp);
                    field.fieldName = tbname.Text;
                    field.numOfSeats = Convert.ToInt32(tbSeat.Text);
                    field.technicalInformation = tbTech.Text;
                    field.status = 0;
                    field.images = buffer;
                    DataProvider.ins.DB.SaveChanges();
                    Success success = new Success();
                    success.ShowDialog();

                    int dotIndex1 = cbLocation.Text.IndexOf('.');
                    int dotIndex2 = cbCountry.Text.IndexOf('.');
                    tbLocation.Text = dotIndex1 != -1 ? cbLocation.Text.Substring(dotIndex1 + 2) : cbLocation.Text;
                    locationLocal = tbLocation.Text;
                    tbCountry.Text = dotIndex2 != -1 ? cbCountry.Text.Substring(dotIndex2 + 2) : cbCountry.Text;


                    nameLocal = tbname.Text;

                    seatsLocal = Convert.ToInt32(tbSeat.Text);

                    techLocal = tbTech.Text;
                    tbLocation.Visibility = Visibility.Visible;
                    tbCountry.Visibility = Visibility.Visible;
                    cbLocation.Visibility = Visibility.Hidden;
                    cbCountry.Visibility = Visibility.Hidden;
                    tbname.IsReadOnly = true;
                    tbSeat.IsReadOnly = true;
                    tbTech.IsReadOnly = true;
                    Upload.Visibility = Visibility.Hidden;
                    Save.Visibility = Visibility.Hidden;
                    Cancel.Visibility = Visibility.Hidden;
                    Edit.Visibility = Visibility.Visible;
                    Delete.Visibility = Visibility.Visible;
                    //tbLocation.Text = locationLocal;
                    tbname.Text = nameLocal;
                    tbSeat.Text = seatsLocal.ToString();
                    tbTech.Text = techLocal;
                }

            }
            catch
            {
                Error error = new Error("Có Lỗi Xảy Ra");
                error.ShowDialog();
            }
        }
    }
}
