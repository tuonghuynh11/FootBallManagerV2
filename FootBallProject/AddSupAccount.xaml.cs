using FootBallProject.Model;
using FootBallProject.Utils;
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
using System.Windows.Shapes;

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for AddSupAccount.xaml
    /// </summary>
    public partial class AddSupAccount : Window
    {
        public AddSupAccount()
        {
            InitializeComponent();

            var list = DataProvider.ins.DB.SUPPLIERs.ToList();
            List<string> list2 = new List<string>();
            foreach (var item in list)
            {
                list2.Add(item.idSupplier.ToString() + ". " + item.supplierName);
            }
            cbht.ItemsSource = list2;
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbht.Text == "" || tbuser.Text == "" || tbpass.Text == "")
                {
                    Error error = new Error("Các trường không được bỏ trống!");
                    error.ShowDialog();
                    return;
                }
                string[] part = cbht.Text.Split('.');
                int idsup = Convert.ToInt32(part[0].Trim());
                var user = DataProvider.ins.DB.USERS.FirstOrDefault(x => x.USERNAME.Equals(tbuser.Text));
                if (user != null)
                {
                    Error error = new Error("Tên đăng nhập đã tồn tại");
                    error.ShowDialog();
                    return;
                }
                var sup = DataProvider.ins.DB.USERS.FirstOrDefault(x => x.IDAVATAR == idsup);
                if (sup != null)
                {
                    Error error = new Error("Thành viên đã được cấp tài khoản");
                    error.ShowDialog();
                    return;
                }
                string passEncode = SHA256Cryptography.Instance.EncryptString(tbpass.Text);
                USER uSER = new USER
                {
                    IDUSERROLE = 5,
                    USERNAME = tbuser.Text,
                    PASSWORD = passEncode,
                    DISPLAYNAME = part[1].Trim(),
                    IDAVATAR = idsup,
                };
                DataProvider.ins.DB.USERS.Add(uSER);
                DataProvider.ins.DB.SaveChanges();
                Success success = new Success();
                success.ShowDialog();
                this.Close();
            }
            catch
            {
                Error error = new Error("Đã có lỗi xảy ra");
                error.ShowDialog();
            }

        }

        private void Close_but_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
