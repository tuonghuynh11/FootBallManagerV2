using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;
using Microsoft.Office.Core;
using System.Windows.Interop;
using Microsoft.Win32;
using System.IO;
using FootBallProject.Model;
using System.Text.RegularExpressions;

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for BLD_Appear.xaml
    /// </summary>
    public partial class BLD_Appear : Window
    {
        public string id;
        public string connectstr = ConfigurationManager.ConnectionStrings["connectstr"].ConnectionString;
        List<string> qts = new List<string>();
        List<string> cvs = new List<string>();
        public string usr = USER.USERN;
        public BLD_Appear()
        {
            InitializeComponent();
            cbcv.Items.Add("HLV Trưởng");
            cbcv.Items.Add("Trợ lí HLV");
            cbcv.Items.Add("Chủ tịch CLB");
            cvs.Add("HLV Trưởng");
            cvs.Add("Trợ lí HLV");
            cvs.Add("Chủ tịch CLB");
            ReadOrderData(connectstr);
            Int32 role = -1;
            string queryString = "SELECT * FROM dbo.USERS WHERE USERNAME = '" + usr + "'";
            using (SqlConnection connection = new SqlConnection(connectstr))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    role = reader.GetInt32(1);
                }
                reader.Close();
            }
            if (role == 4)
            {
                delete.Visibility = Visibility.Hidden;
            }
            if(role == 1 || role == 3)
            {
                delete.Visibility = Visibility.Hidden;
                editbtt.Visibility = Visibility.Hidden;
            }
        }
        public void Init(string Iddoibong, string name, DateTime dateofbirth, string position,string mailcontact, string nationality, ImageSource imgs)
        {
            tbID.Text = Iddoibong;
            tbht.Text = name;
            string temp = dateofbirth.ToString();
            temp = temp.Substring(0, temp.IndexOf(" "));
            nsdp.Text = temp;
            dpns.SelectedDate = DateTime.Parse(temp);
            tbcv.Text = position;
            tbdc.Text = mailcontact;
            tbqt.Text = nationality;
            if (imgs != null)
            {
                bldimg.Source = imgs;
            }
        }

        private void Close_but_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            string commandText = "DELETE FROM dbo.HUANLUYENVIEN WHERE ID = @id";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectstr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {

                        command.Parameters.Add("@id", SqlDbType.VarChar);
                        command.Parameters["@id"].Value = id;

                        command.ExecuteNonQuery();
                    }
                }
                //Success success = new Success();
                //success.ShowDialog();
                //this.Close();
            }
            catch (Exception)
            {
                Error error = new Error("");
                error.ShowDialog();
                return;
            }
            string commandText2 = "DELETE FROM dbo.USERS WHERE IDNHANSU = @id";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectstr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(commandText2, connection))
                    {

                        command.Parameters.Add("@id", SqlDbType.VarChar);
                        command.Parameters["@id"].Value = id;

                        command.ExecuteNonQuery();
                    }
                }
                //Success success = new Success();
                //success.ShowDialog();
                //this.Close();
            }
            catch (Exception)
            {
                Error error = new Error("");
                error.ShowDialog();
                return;
            }
            Success success = new Success();
            success.ShowDialog();
            this.Close();
        }
        private void editbtt_Click(object sender, RoutedEventArgs e)
        {
            int index1 = 0, index2 = 0;
            foreach (string tmp in qts)
            {
                if (tmp == tbqt.Text)
                {
                    break;
                }
                index1++;
            }
            foreach (string tmp in cvs)
            {
                if (tmp == tbcv.Text)
                {
                    break;
                }
                index2++;
            }
            cbqt.SelectedIndex = index1;
            cbcv.SelectedIndex = index2;
            savebtt.Visibility = Visibility.Visible;
            editbtt.Visibility = Visibility.Hidden;
            uploadbtt.Visibility = Visibility.Visible;
            tbht.IsReadOnly = false;
            cbcv.Visibility = Visibility.Visible;
            tbcv.Visibility = Visibility.Hidden;
            cbqt.Visibility = Visibility.Visible;
            tbqt.Visibility = Visibility.Hidden;
            nsdp.Visibility = Visibility.Hidden;
            dpns.Visibility = Visibility.Visible;
            tbdc.IsReadOnly = false;
        }

        private void savebtt_Click(object sender, RoutedEventArgs e)
        {
            string commandText = "UPDATE dbo.HUANLUYENVIEN SET HOTEN=@hoten, IDQUOCTICH=@idqt, NGAYSINH=@ngaysinh, CHUCVU=@chucvu, GMAIL=@gmail, HINHANH=@hinhanh WHERE ID = @id";
            Regex mailr = new Regex(@"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (tbht.Text == "" || tbID.Text == "" || tbdc.Text == "" || dpns.ToString() == "" || cbcv.Text == "" || cbqt.Text == "")
            {
                Error error = new Error("Chưa đủ thông tin");
                error.ShowDialog();
                return;
            }
            else if (!mailr.IsMatch(tbdc.Text))
            {
                Error error = new Error("Địa chỉ mail không hợp lệ");
                error.ShowDialog();
                return;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectstr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {

                        command.Parameters.Add("@id", SqlDbType.VarChar);
                        command.Parameters["@id"].Value = id;

                        command.Parameters.Add("@hoten", SqlDbType.NVarChar);
                        command.Parameters["@hoten"].Value = tbht.Text;

                        command.Parameters.Add("@ngaysinh", SqlDbType.Date);
                        command.Parameters["@ngaysinh"].Value = dpns.SelectedDate.Value.ToString();

                        command.Parameters.Add("@chucvu", SqlDbType.NVarChar);
                        command.Parameters["@chucvu"].Value = cbcv.Text;

                        command.Parameters.Add("@gmail", SqlDbType.VarChar);
                        command.Parameters["@gmail"].Value = tbdc.Text;

                        string tmp = cbqt.Text.Substring(0, cbqt.Text.IndexOf('.'));

                        command.Parameters.Add("@idqt", SqlDbType.Int);
                        command.Parameters["@idqt"].Value = tmp;

                        byte[] buffer;

                        var bitmap = bldimg.Source as BitmapSource;
                        var encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bitmap));

                        using (var stream = new MemoryStream())
                        {
                            encoder.Save(stream);
                            buffer = stream.ToArray();
                        }

                        command.Parameters.Add("@hinhanh", SqlDbType.Image);
                        command.Parameters["@hinhanh"].Value = buffer;

                        command.ExecuteNonQuery();
                    }
                }
                Success success = new Success();
                success.ShowDialog();
                editbtt.Visibility = Visibility.Visible;
                savebtt.Visibility = Visibility.Hidden;
                uploadbtt.Visibility = Visibility.Hidden;
                tbht.IsReadOnly = true;
                cbcv.Visibility = Visibility.Hidden;
                tbcv.Visibility = Visibility.Visible;
                tbcv.Text = cbcv.Text;
                cbqt.Visibility = Visibility.Hidden;
                tbqt.Visibility = Visibility.Visible;
                tbqt.Text = cbqt.Text.Substring(cbqt.Text.IndexOf('.') + 1);
                nsdp.Visibility = Visibility.Visible;
                dpns.Visibility = Visibility.Hidden;
                string date = dpns.SelectedDate.Value.ToString();
                nsdp.Text = date.Substring(0, date.IndexOf(' '));
                tbdc.IsReadOnly = true;
            }
            catch (Exception)
            {
                Error error = new Error("");
                error.ShowDialog();
            }
        }
        private void ReadOrderData(string connectionString)
        {
            string queryString = "SELECT * FROM dbo.QUOCTICH";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int tmpid = reader.GetInt32(0);
                    string tmp = tmpid.ToString() + ". " + reader.GetString(1);
                    qts.Add(reader.GetString(1));
                    cbqt.Items.Add(tmp);
                }
                reader.Close();
            }
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
    }
}
