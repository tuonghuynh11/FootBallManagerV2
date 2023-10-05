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
using FootBallProject.Utils;
using System.Net.Mail;
using System.Net;

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for ChangePass.xaml
    /// </summary>
    public partial class ChangePass : Window
    {
        public string connectstr = ConfigurationManager.ConnectionStrings["connectstr"].ConnectionString;
        public string usr = USER.USERN; // Lay du lieu tu luc dang nhap
        public string oldpass = "";
        public string getMail;
        public ChangePass()
        {
            InitializeComponent();
        }

        private void Close_but_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        Random random = new Random();
        int otp;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SHA256Cryptography.Instance.EncryptString(Oldpass.Password) != oldpass)
            {
                Error error = new Error("Mật khẩu cũ không đúng");
                error.ShowDialog();
            }
            else if (SHA256Cryptography.Instance.EncryptString(newPass.Password) == oldpass)
            {
                Error error = new Error("Mật khẩu mới trùng mật khẩu cũ");
                error.ShowDialog();
            }
            else if (newPass.Password != xacnhan.Password)
            {
                Error error = new Error("Mật khẩu xác nhận không trùng khớp");
                error.ShowDialog();
            }
            else if(otp.ToString() != tbOTP.Text)
            {
                Error error = new Error("OTP không chính xác");
                error.ShowDialog();
            }
            else
            {
                oldpass = SHA256Cryptography.Instance.EncryptString(xacnhan.Password);
                string commandText = "UPDATE dbo.USERS SET PASSWORD = @password WHERE USERNAME = " + "'" + usr + "'";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectstr))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(commandText, connection))
                        {
                            command.Parameters.Add("@password", SqlDbType.VarChar);
                            command.Parameters["@password"].Value = SHA256Cryptography.Instance.EncryptString(xacnhan.Password);

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

        private void getOTP_Click(object sender, RoutedEventArgs e)
        {
            otp = random.Next(10000, 100000);
            try
            {

                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("footballmanagement111@gmail.com");
                msg.To.Add(getMail);
                msg.Subject = "Mã OTP xác nhận đổi mật khẩu";
                msg.Body = otp.ToString() + " là mã dùng để xác nhận việc đổi mật khẩu của bạn.";

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
                success.Show();

            }
            catch (Exception)
            {

                Error error = new Error("Gửi OTP không thành công");
                error.Show();
            }
        }
    }
}
