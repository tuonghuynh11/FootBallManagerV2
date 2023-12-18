using FootBallProject.Model;
using FootBallProject.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FootBallProject.UserControlBar
{
    /// <summary>
    /// Interaction logic for ManageFieldUC.xaml
    /// </summary>
    public partial class ManageFieldUC : UserControl
    {
        List<Stadium> sta = new List<Stadium>();

        public ManageFieldUC()
        {
            InitializeComponent();
            List<Stadium> fakeUsed = new List<Stadium>
            {


            };

            //fieldUsedDTG.ItemsSource = fakeUsed;
            //fieldAvailableDTG.ItemsSource = fakeAvai;
            Load();


        }

        public void Load()
        {
            var list = DataProvider.ins.DB.FIELDs.ToList();
            List<Stadium> fIELDs = new List<Stadium>();
            foreach (var field in list)
            {
                //fIELDs.Add(new FIELD { idField = field.idField, idDiaDiem = field.idDiaDiem, fieldName = field.fieldName, technicalInformation = field.technicalInformation, numOfSeats = field.numOfSeats, status = field.status});
                var location = DataProvider.ins.DB.DIADIEMs.Find(field.idDiaDiem);
                string loc = "";
                if (location != null)
                {
                    loc = location.TENDIADIEM;
                }
                byte[] img = null;
                if (field.images != null)
                {
                    img = field.images;
                }
                DIADIEM dIADIEM = DataProvider.ins.DB.DIADIEMs.Find(field.idDiaDiem);
                string country = "";
                if (dIADIEM != null)
                {
                    QUOCTICH qUOCTICH = DataProvider.ins.DB.QUOCTICHes.Find(dIADIEM.IDQUOCGIA);
                    if (qUOCTICH != null)
                    {
                        country = qUOCTICH.TENQUOCGIA.ToString();
                    }
                }
                if ((int)field.status == 0)
                {

                    fIELDs.Add(new Stadium(field.idField, field.fieldName, loc, field.technicalInformation, (int)field.numOfSeats, (int)field.status, img, country));
                }
            }
            fieldAvailableDTG.ItemsSource = fIELDs;
        }

        private void fieldUsedDTG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DataGrid dg = sender as DataGrid;
            //Stadium stadium = dg.SelectedItem as Stadium;
            //if(stadium == null)
            //{
            //    Error error = new Error("");
            //    error.ShowDialog();
            //    return;
            //}
            //StadiumAppear stadiumAppear = new StadiumAppear();
            //stadiumAppear.id = stadium.ID;
            //stadiumAppear.Init(stadium.Location, stadium.Name, stadium.seatNum, stadium.Technical);
            //stadiumAppear.ShowDialog();
        }

        private void fieldAvailableDTG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            Stadium stadium = dg.SelectedItem as Stadium;
            if (stadium == null)
            {
                //Error error = new Error("");
                //error.ShowDialog();
                return;
            }
            StadiumAppear stadiumAppear = new StadiumAppear();
            stadiumAppear.id = stadium.idField;
            stadiumAppear.Init(stadium.Location, stadium.fieldName, stadium.numOfSeats, stadium.technicalInformation, stadium.images, stadium.Country);
            stadiumAppear.ShowDialog();
            Load();
            fieldAvailableDTG.Items.Refresh();
        }

        private void NewStadium_Click(object sender, RoutedEventArgs e)
        {
            AddStadium addStadium = new AddStadium();
            addStadium.ShowDialog();
            Load();
            fieldAvailableDTG.Items.Refresh();
        }

        private void NewSchedule_Click(object sender, RoutedEventArgs e)
        {
            AddScheduleStadium add = new AddScheduleStadium();
            add.ShowDialog();
        }
    }
    public class Stadium
    {
        public int idField { get; set; }
        public string fieldName { get; set; }
        public string Location { get; set; }
        public string technicalInformation { get; set; }
        public int numOfSeats { get; set; }
        public int status { get; set; }
        public byte[] images { get; set; }
        public string Country { get; set; }
        public Stadium(int idField, string fieldName, string Location, string technicalInformation, int numOfSeats, int status, byte[] images, string country)
        {
            this.idField = idField;
            this.fieldName = fieldName;
            this.Location = Location;
            this.technicalInformation = technicalInformation;
            this.numOfSeats = numOfSeats;
            this.status = status;
            this.images = images;
            Country = country;
        }
    }
}
