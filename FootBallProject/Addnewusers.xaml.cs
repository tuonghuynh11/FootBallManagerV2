using FootBallProject.Model;
using FootBallProject.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.RightsManagement;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for Addnewusers.xaml
    /// </summary>
    public partial class Addnewusers : Window
    {
        public string connectstr = ConfigurationManager.ConnectionStrings["connectstr"].ConnectionString;
        public List<string> users = new List<string>();
        public List<string> signedusers = new List<string>();
        public List<Int32> idnhansu = new List<int>();
        public Addnewusers()
        {
            InitializeComponent();
            //cbcv.Items.Add("HLV Trưởng");
            //cbcv.Items.Add("Trợ lí HLV");
            //cbcv.Items.Add("Chủ tịch CLB");
            string queryString = "SELECT * FROM dbo.USERS";
            using (SqlConnection connection = new SqlConnection(connectstr))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    signedusers.Add(reader.GetString(2));
                    ReadSingleRow((IDataRecord)reader);
                }
                reader.Close();
            }

        }
        private void ReadSingleRow(IDataRecord dataRecord)
        {
            if (dataRecord[8].ToString() != "")
            {
                idnhansu.Add((Int32)dataRecord[8]);
            }
        }

        public void getID(string id)
        {
            tbIDdoibong.Text = id;
            ReadOrderData(connectstr);
        }

        private void Close_but_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            if (cbht.Text == "" || tbuser.Text == "" || tbpass.Text == "")
            {
                Error error = new Error("Chưa đủ thông tin");
                error.ShowDialog();
                return;
            }
            foreach(string s in signedusers)
            {
                if(tbuser.Text == s)
                {
                    Error error = new Error("Tên đăng nhập đã tồn tại");
                    error.ShowDialog();
                    return;
                }
            }
            string chosenid = cbht.Text.Substring(0, cbht.Text.IndexOf('.'));
            foreach(Int32 i in idnhansu)
            {
                if(Convert.ToInt32(chosenid) == i)
                {
                    Error error = new Error("Thành viên đã được cấp tài khoản");
                    error.ShowDialog();
                    return;
                }
            }
            string passEncode = SHA256Cryptography.Instance.EncryptString(tbpass.Text);
            string queryString = "SELECT * FROM dbo.HUANLUYENVIEN WHERE ID = " + cbht.Text.Substring(0, cbht.Text.IndexOf('.'));
            using (SqlConnection connection = new SqlConnection(connectstr))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(reader.GetString(7));
                    users.Add(reader.GetString(3));
                    users.Add(reader.GetString(5));
                    users.Add(Convert.ToString(reader.GetInt32(0)));
                }
                reader.Close();
            }
            string tmp = cbht.Text.Substring(0, cbht.Text.IndexOf('.'));
            string commandText = "INSERT INTO dbo.USERS (IDUSERROLE, USERNAME, PASSWORD, DISPLAYNAME, EMAIL, IDNHANSU) VALUES (@iduserrole, @username, @password, @displayname, @email, @idnhansu)";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectstr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        int idrole;
                        if (users[0] == "HLV Trưởng")
                        {
                            idrole = 1;
                        }
                        else if (users[0] == "Chủ tịch CLB")
                        {
                            idrole = 4;
                        }
                        else
                        {
                            idrole = 3;
                        }

                        command.Parameters.Add("@iduserrole", SqlDbType.Int);
                        command.Parameters["@iduserrole"].Value = idrole;

                        command.Parameters.Add("@username", SqlDbType.VarChar);
                        command.Parameters["@username"].Value = tbuser.Text;

                        command.Parameters.Add("@password", SqlDbType.VarChar);
                        command.Parameters["@password"].Value = passEncode;

                        command.Parameters.Add("@displayname", SqlDbType.NVarChar);
                        command.Parameters["@displayname"].Value = users[1];

                        command.Parameters.Add("@email", SqlDbType.NVarChar);
                        command.Parameters["@email"].Value = users[2];

                        command.Parameters.Add("@idnhansu", SqlDbType.Int);
                        command.Parameters["@idnhansu"].Value = users[3];

                        command.ExecuteNonQuery();
                    }
                }
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("footballmanagement111@gmail.com");
                msg.To.Add(users[2]);
                msg.Subject = "Thông báo được cấp tài khoản";
                msg.Body = "Bạn đã được cấp tài khoản. Tên tài khoản: " + tbuser.Text + " - Mật khẩu: " + tbpass.Text;

                SmtpClient smt = new SmtpClient();
                smt.Host = "smtp.gmail.com";
                System.Net.NetworkCredential ntcd = new NetworkCredential();
                ntcd.UserName = "footballmanagement111@gmail.com";
                ntcd.Password = "upovphfgbfmhacux";
                smt.Credentials = ntcd;
                smt.EnableSsl = true;
                smt.Port = 587;
                smt.Send(msg);
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

        private void ReadOrderData(string connectionString)
        {
            string queryString = "SELECT * FROM dbo.HUANLUYENVIEN WHERE IDDOIBONG = '" + tbIDdoibong.Text.Substring(0, tbIDdoibong.Text.IndexOf('.')) + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Int32 id = reader.GetInt32(0);

                    string tmp = Convert.ToString(id) + ". " + reader.GetString(3) + "-" + reader.GetString(7);
                    cbht.Items.Add(tmp);

                }
                reader.Close();
            }
        }
    }
}
