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
using System.Windows.Shapes;
using static FootBallProject.UserControlBar.UserControl_DS_BLD;
using System.Configuration;
using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for DS_BLD_ADD.xaml
    /// </summary>
    public partial class DS_BLD_ADD : Window
    {
        public string connectstr = ConfigurationManager.ConnectionStrings["connectstr"].ConnectionString;
        public DS_BLD_ADD()
        {
            InitializeComponent();
            ReadOrderData(connectstr);
            //ReadOrderData2(connectstr);
            cbCV.Items.Add("HLV Trưởng");
            cbCV.Items.Add("Trợ lí HLV");
            cbCV.Items.Add("Chủ tịch CLB");
        }

        public void GetIdDB(string id)
        {
            tbID.Text = id;
        }
        private void AddNewPerson_2_Click(object sender, RoutedEventArgs e)
        {
            string commandText = "INSERT INTO dbo.HUANLUYENVIEN (IDDOIBONG,HOTEN, GMAIL, NGAYSINH, CHUCVU, IDQUOCTICH, HINHANH) VALUES " +
                "(@iddoibong,@hoten, @gmail, @ngaysinh, @chucvu, @idquoctich, @hinhanh);";
            Regex mailr = new Regex(@"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (tbht.Text == "" || tbID.Text == "" || tbdc.Text == "" || nsdp.ToString() == "" || cbCV.Text == "" || cbqt.Text == "")
            {
                Error error = new Error("Chưa đủ thông tin");
                error.ShowDialog();
            }
            else if (!mailr.IsMatch(tbdc.Text))
            {
                Error error = new Error("Địa chỉ mail không hợp lệ");
                error.ShowDialog();
            }
            else
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectstr))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(commandText, connection))
                        {
                            string tmp = tbID.Text;
                            tmp = tmp.Substring(0, tmp.IndexOf('.'));

                            command.Parameters.Add("@iddoibong", SqlDbType.VarChar);
                            command.Parameters["@iddoibong"].Value = tmp;

                            command.Parameters.Add("@hoten", SqlDbType.NVarChar);
                            command.Parameters["@hoten"].Value = tbht.Text;

                            command.Parameters.Add("@gmail", SqlDbType.VarChar);
                            command.Parameters["@gmail"].Value = tbdc.Text;

                            command.Parameters.Add("@ngaysinh", SqlDbType.SmallDateTime);
                            command.Parameters["@ngaysinh"].Value = nsdp.ToString();

                            command.Parameters.Add("@chucvu", SqlDbType.NVarChar);
                            command.Parameters["@chucvu"].Value = cbCV.Text;

                            string tmpstr = cbqt.Text;
                            tmpstr = tmpstr.Substring(0, tmpstr.IndexOf('.'));

                            command.Parameters.Add("@idquoctich", SqlDbType.Int);
                            command.Parameters["@idquoctich"].Value = tmpstr;

                            byte[] buffer;
                            var bitmap = bldimage.Source as BitmapSource;
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
                    this.Close();
                }
                catch (Exception)
                {
                    Error error = new Error("");
                    error.ShowDialog();
                }
            }
        }

        private void Close_but_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                    cbqt.Items.Add(tmp);
                }
                reader.Close();
            }
        }

        //private void ReadOrderData2(string connectionString)
        //{
        //    string queryString = "SELECT * FROM dbo.DOIBONG";
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlCommand command = new SqlCommand(queryString, connection);
        //        connection.Open();

        //        SqlDataReader reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            string tmp = reader.GetString(0) + ". " + reader.GetString(4);
        //            cbID.Items.Add(tmp);
        //        }
        //        reader.Close();
        //    }
        //}

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFileDialog.FilterIndex = 1;
            if(openFileDialog.ShowDialog() == true)
            {
                bldimage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}
