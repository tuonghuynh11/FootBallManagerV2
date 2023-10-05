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
using System.Configuration;
using FootBallProject.Model;
using Microsoft.Win32;
using System.IO;
using System.Net;
using static FootBallProject.UserControlBar.UserControl_DS_BLD;
using System.Text.RegularExpressions;
using FootBallProject.UserControlBar;
using FootBallProject.Class;

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for UserAccount.xaml
    /// </summary>
    public partial class UserAccount : Window
    {
        public string connectstr = ConfigurationManager.ConnectionStrings["connectstr"].ConnectionString;

        public string usr = USER.USERN; // Lay du lieu tu luc dang nhap

        public UserAccount()
        {
            InitializeComponent();
            ReadOrderData(connectstr);

        }

        public class User_Account
        {
            public string username { get; set; }
            public string password { get; set; }
            public string name { get; set; }
            public string gmail { get; set; }
            public User_Account(string username, string password, string name, string gmail)
            {
                this.username = username;
                this.password = password;
                this.name = name;
                this.gmail = gmail;
            }
        }

        private void Close_but_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ReadOrderData(string connectionString)
        {
            string queryString = "SELECT * FROM USERS WHERE USERNAME=" + "'" + usr + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    txbhoten.Text = reader.GetString(4);
                    txbhusername.Text = reader.GetString(2);

                   
                    pbpass.Password = "password";
                    txbemail.Text = reader.GetString(5);
                    ReadSingleRow((IDataRecord)reader);
                }
                reader.Close();
            }
        }
        private void ReadSingleRow(IDataRecord dataRecord)
        {
            
            byte[] pic;
            if (dataRecord[9].ToString() == "")
            {
                pic = null;
            }
            else
            {
                pic = (byte[])dataRecord[9];
            }
            if (pic != null)
            {
                using (var stream = new MemoryStream(pic))
                {
                    avtuser.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                }
            }
        }

        private void Change_Pass_Click_1(object sender, RoutedEventArgs e)
        {
            ChangePass change = new ChangePass();
            change.getMail = txbemail.Text;
            string queryString = "SELECT * FROM USERS WHERE USERNAME=" + "'" + usr + "'";
            using (SqlConnection connection = new SqlConnection(connectstr))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    change.oldpass = reader.GetString(3);
                    
                }
                reader.Close();
            }
            change.ShowDialog();
            //ReadOrderData(connectstr);
        }
        private void HandleInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            txbhoten.IsReadOnly = false;
            txbemail.IsReadOnly = false;
            editbtt.Visibility = Visibility.Hidden;
            savebtt.Visibility = Visibility.Visible;
            uploadbtt.Visibility = Visibility.Visible; 
        }

        private void savebtt_Click(object sender, RoutedEventArgs e)
        {
            string commandText = "UPDATE dbo.USERS SET DISPLAYNAME=@displayname, EMAIL=@email, avatar=@avatar WHERE USERNAME = @username";
            Regex mailr = new Regex(@"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (txbhoten.Text == "" || txbemail.Text == "")
            {
                Error error = new Error("Chưa đủ thông tin");
                error.ShowDialog();
                return;
            }
            else if (!mailr.IsMatch(txbemail.Text))
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

                        command.Parameters.Add("@USERNAME", SqlDbType.VarChar);
                        command.Parameters["@username"].Value = usr;

                        command.Parameters.Add("@displayname", SqlDbType.NVarChar);
                        command.Parameters["@displayname"].Value = txbhoten.Text;

                        command.Parameters.Add("@email", SqlDbType.VarChar);
                        command.Parameters["@email"].Value = txbemail.Text;

                        byte[] buffer;

                        var bitmap = avtuser.Source as BitmapSource;
                        var encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(bitmap));

                        using (var stream = new MemoryStream())
                        {
                            encoder.Save(stream);
                            buffer = stream.ToArray();
                        }

                        command.Parameters.Add("@avatar", SqlDbType.Image);
                        command.Parameters["@avatar"].Value = buffer;

                        command.ExecuteNonQuery();
                       ControlBarUC barUC = new ControlBarUC();
                        barUC.setNewavatar(buffer);
                    }
                }
                
                Success success = new Success();
                success.ShowDialog();
                txbhoten.IsReadOnly = true;
                txbemail.IsReadOnly = true;
                editbtt.Visibility = Visibility.Visible;
                savebtt.Visibility = Visibility.Hidden;
                uploadbtt.Visibility = Visibility.Hidden;

            }
            catch (Exception)
            {
                Error error = new Error("");
                error.ShowDialog();
            }
        }

        private void uploadbtt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                avtuser.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}
