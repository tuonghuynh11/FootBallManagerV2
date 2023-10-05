using FootBallProject.Model;
using FootBallProject.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.Service
{
    public class LoginServices
    {
        public class LoginEvent : EventArgs
        {
            private USER _user;

            public USER User { get => _user; set => _user = value; }

            public LoginEvent(USER user)
            {
                this.User = user;
            }
        }

        static private event EventHandler<LoginEvent> _updateCurrentUser;
        static public event EventHandler<LoginEvent> UpdateCurrentUser
        {
            add { _updateCurrentUser += value; }
            remove { _updateCurrentUser -= value; }
        }

        private static LoginServices s_instance;

        public static LoginServices Instance => s_instance ?? (s_instance = new LoginServices());

        private static USER s_currentUser;
        public static USER CurrentUser { get => s_currentUser; set => s_currentUser = value; }

        public static string FilePathRememberedAccount = "D:\\accountStuMan.txt";

        public int CountPeriodTodayOfUser;

        public officialleagueEntities1 db = DataProvider.Instance.Database;


        public LoginServices() { }

        public bool IsUserAuthentic(string username, string password)
        {
            string passEncode = SHA256Cryptography.Instance.EncryptString(password);
            //string passEncode = password;

            int accCount = db.USERS.Where(user => user.USERNAME == username && user.PASSWORD == passEncode).Count();

            if (accCount > 0)
            {
                return true;
            }
            return false;
        }

        public void Login(string username)
        {
            USER user = UserServices.Instance.FindUserByUsername(username);

            CurrentUser = user;

            _updateCurrentUser?.Invoke(this, new LoginEvent(user));
        }

        public static string Base64Encode(string plainText)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static string Encrypt(string input, string hash)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(input);
            using (MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5provider.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                byte[] iv = md5provider.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider() { Key = keys, Mode = CipherMode.CFB, Padding = PaddingMode.PKCS7, IV = iv })
                {
                    ICryptoTransform transform = aes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }
        public static string Decrypt(string input, string hash)
        {
            byte[] data = Convert.FromBase64String(input);
            using (MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5provider.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                byte[] iv = md5provider.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider() { Key = keys, Mode = CipherMode.CFB, Padding = PaddingMode.PKCS7, IV = iv })
                {
                    ICryptoTransform transform = aes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return UTF8Encoding.UTF8.GetString(results);
                }
            }
        }
    }

}
