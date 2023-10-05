using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Configuration;
using FootBallProject.ViewModel;
using System.IO;
using FootBallProject.Model;

namespace FootBallProject.UserControlBar
{
    /// <summary>
    /// Interaction logic for UserControl_DS_BLD.xaml
    /// </summary>
    public partial class UserControl_DS_BLD : UserControl
    {
        public string connectstr = ConfigurationManager.ConnectionStrings["connectstr"].ConnectionString;
        public List<BLD> bLDs = new List<BLD>();
        public string usr = USER.USERN;
        public UserControl_DS_BLD()
        {
            InitializeComponent();
            ReadOrderData2(connectstr);
            Int32 role = -1;
            Int32 ID = -1;
            string queryString = "SELECT * FROM dbo.USERS WHERE USERNAME = '" + usr + "'";
            using (SqlConnection connection = new SqlConnection(connectstr))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    role = reader.GetInt32(1);
                    if (role != 2)
                    {
                        ID = reader.GetInt32(8);
                    }
                }
                reader.Close();
            }
            
            if (role != 2)
            {
                AddNewUser.Visibility = Visibility.Hidden;
                AddNewPerson.Visibility = Visibility.Hidden;
                string tendoibong = "";
                string queryString2 = "SELECT * FROM dbo.DOIBONG db JOIN dbo.HUANLUYENVIEN hlv ON db.ID = hlv.IDDOIBONG WHERE hlv.ID = " + ID.ToString();
                using (SqlConnection connection = new SqlConnection(connectstr))
                {
                    SqlCommand command = new SqlCommand(queryString2, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        tendoibong = reader.GetString(0) + ". " + reader.GetString(4);

                    }
                    reader.Close();
                }
                int index = cbIDdoibong.Items.IndexOf(tendoibong);
                cbIDdoibong.SelectedItem = cbIDdoibong.Items[index];
                cbIDdoibong.IsHitTestVisible = false;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (cbIDdoibong.Text != "")
            {
                ReadOrderData(connectstr);
                DTG.ItemsSource = bLDs;
                DTG.Items.Refresh();
            }
        }

        private void cbIDdoibong_DropDownClosed(object sender, EventArgs e)
        {
            if (cbIDdoibong.Text != "")
            {
                ReadOrderData(connectstr);
                DTG.ItemsSource = bLDs;
                DTG.Items.Refresh();
            }
        }


        public class BLD
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public DateTime Dateofbirth { get; set; }
            public string Position { get; set; }
            public string Mailcontact { get; set; }
            public string Nationality { get; set; }
            public string IDdoibong { get; set; }
            public ImageSource BLDimage { get; set; }
            public BLD(string iD, string name, DateTime dateofbirth, string position, string mailcontact, string nationality)
            {
                ID = iD;
                Name = name;
                Dateofbirth = dateofbirth;
                Position = position;
                Mailcontact = mailcontact;
                Nationality = nationality;
            }
        }

        private void ReadOrderData(string connectionString)
        {
            if(cbIDdoibong.Text == "")
            {
                Error error = new Error("Chưa chọn đội bóng");
                error.ShowDialog();
                return;
            }
            string GetIDDOIBONG = cbIDdoibong.Text;
            GetIDDOIBONG = GetIDDOIBONG.Substring(0, GetIDDOIBONG.IndexOf('.'));
            bLDs.Clear();
            string queryString = "SELECT * FROM dbo.HUANLUYENVIEN hlv JOIN dbo.QUOCTICH qt ON hlv.IDQUOCTICH = qt.ID" + " WHERE hlv.IDDOIBONG = " + "'" + GetIDDOIBONG + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ReadSingleRow((IDataRecord)reader);
                }
                reader.Close();
            }
        }

        private void ReadSingleRow(IDataRecord dataRecord)
        {
            BLD bLD = new BLD(dataRecord[0].ToString(), (string)dataRecord[3], (DateTime)dataRecord[6], (string)dataRecord[7], (string)dataRecord[5], dataRecord[10].ToString());
            bLD.IDdoibong = (string)dataRecord[1];
            byte[] pic;
            if (dataRecord[8].ToString() == "")
            {
                pic = null;
            }
            else
            {
                pic = (byte[])dataRecord[8];
            }
            if (pic != null)
            {
                using (var stream = new MemoryStream(pic))
                {
                    bLD.BLDimage = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                }
            }
            bLDs.Add(bLD);
        }

        //private void Button_Click_ExportExcel(object sender, RoutedEventArgs e)
        //{

        //}

        private void AddNewPerson_Click(object sender, RoutedEventArgs e)
        {
            if (cbIDdoibong.Text == "")
            {
                Error error = new Error("Chưa chọn đội bóng");
                error.ShowDialog();
                return;
            }
            DS_BLD_ADD dsadd = new DS_BLD_ADD();
            dsadd.GetIdDB(cbIDdoibong.Text);
            dsadd.ShowDialog();
            ReadOrderData(connectstr);
            DTG.ItemsSource = bLDs;
            DTG.Items.Refresh();
        }



        private void DTG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            BLD bLD = dg.SelectedItem as BLD;
            if(bLD == null)
            {
                return;
            }
            string tmp = cbIDdoibong.Text;
            string tmp1 = tmp.Substring(tmp.IndexOf(' '));
            string tmp2 = tmp.Substring(0, tmp.IndexOf('.'));
            if(tmp2.ToLower() != bLD.IDdoibong.ToLower())
            {
                Error error = new Error("");
                error.ShowDialog();
                return;
            }
            BLD_Appear bLD_Appear = new BLD_Appear();
            bLD_Appear.id = bLD.ID;
            bLD_Appear.Init(tmp1, bLD.Name, bLD.Dateofbirth, bLD.Position, bLD.Mailcontact, bLD.Nationality, bLD.BLDimage);
            bLD_Appear.ShowDialog();
            ReadOrderData(connectstr);
            DTG.ItemsSource = bLDs;
            DTG.Items.Refresh();
        }

        private void _Load_Click(object sender, RoutedEventArgs e)
        {
            ReadOrderData(connectstr);
            DTG.ItemsSource = bLDs;
            DTG.Items.Refresh();
        }

        private void ReadOrderData2(string connectionString)
        {
            string queryString = "SELECT * FROM dbo.DOIBONG";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string tmp = reader.GetString(0) + ". " + reader.GetString(4);
                    cbIDdoibong.Items.Add(tmp);
                }
                reader.Close();
            }
        }
        private void AddNewUser_Click(object sender, RoutedEventArgs e)
        {
            if (cbIDdoibong.Text == "")
            {
                Error error = new Error("Chưa chọn đội bóng");
                error.ShowDialog();
                return;
            }
            Addnewusers addnewusers = new Addnewusers();
            addnewusers.getID(cbIDdoibong.Text);
            addnewusers.ShowDialog();
        }

        //private void UserControl_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if(cbIDdoibong.Text != "")
        //    {
        //        ReadOrderData(connectstr);
        //        DTG.ItemsSource = bLDs;
        //        DTG.Items.Refresh();
        //    }
        //}

        //private void cbIDdoibong_DropDownClosed(object sender, EventArgs e)
        //{
        //    if (cbIDdoibong.Text != "")
        //    {
        //        ReadOrderData(connectstr);
        //        DTG.ItemsSource = bLDs;
        //        DTG.Items.Refresh();
        //    }
        //}

        //private void _Delete_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
