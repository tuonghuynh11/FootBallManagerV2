using FootBallProject.Model;
using FootBallProject.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.Service
{
    public class UserServices
    {
        private static UserServices s_instance;

        public static UserServices Instance => s_instance ?? (s_instance = new UserServices());

        public UserServices() { }

        public USER GetUserInfo()
        {
            USER a = DataProvider.Instance.Database.USERS.FirstOrDefault();
            return a;
        }
        public USER GetUserById(int id)
        {
            USER a = DataProvider.Instance.Database.USERS.FirstOrDefault(user => user.ID == id);
            return a;
        }

        public USER FindUserByUsername(string username)
        {
            USER user = DataProvider.Instance.Database.USERS.FirstOrDefault(account => account.USERNAME == username);

            return user;
        }
        public string GetDisplayNameById(int id)
        {
            var user = GetUserById(id);
            return user.DISPLAYNAME;
        }
        //public string GetAvatarById(int id)
        //{
        //    var user = GetUserById(id);
        //    return user.?.Image;
        //}
        //public string GetFacultyById(Guid id)
        //{
        //    var user = GetUserById(id);
        //    return user.Faculty.DisplayName;
        //}
        public List<USER> GetUserByGmail(string email)
        {
            return DataProvider.Instance.Database.USERS.Where(user => user.EMAIL.Equals(email)).ToList();
        }
        public USER GetUserByOTP(OTP otp)
        {
            return DataProvider.Instance.Database.USERS.FirstOrDefault(tmpUser => tmpUser.IDOTP == otp.Id);
        }

        public bool CheckAdminByIdUser(int id)
        {
            return DataProvider.Instance.Database.USERS.FirstOrDefault(user => user.ID == id).USERROLE.ROLE.Contains("Admin");
        }

        public bool IsUsedEmail(string email)
        {
            foreach (var user in DataProvider.Instance.Database.USERS.ToList())
            {
                if (user.EMAIL.Equals(email))
                    return true;
            }
            return false;
        }

        public USER FindUserbyUserId(int id)
        {
            USER a = DataProvider.Instance.Database.USERS.Where(userItem => userItem.ID == id).FirstOrDefault();
            return a;
        }

        public bool SaveUserToDatabase(USER user)
        {
            try
            {
                USER savedUser = FindUserbyUserId(user.ID);

                if (savedUser == null)
                {
                    DataProvider.Instance.Database.USERS.AddOrUpdate(user);
                }
                else
                {
                    //savedFaculty = (faculty.ShallowCopy() as Faculty);
                    //Reflection.CopyProperties(user, savedUser);
                }
                DataProvider.Instance.Database.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //public async Task SaveImageToUser(string image)
        //{
        //    var imgId = await DatabaseImageTableServices.Instance.SaveImageToDatabaseAsync(image);
        //    LoginServices.CurrentUser.IdAvatar = imgId;
        //    DataProvider.Instance.Database.SaveChanges();
        //}
        public bool ChangePassWord(string passWord, string gmail)
        {
            var user = GetUserByGmail(gmail);
            if (user.Count == 0)
                return false;
            user.FirstOrDefault().PASSWORD = SHA256Cryptography.Instance.EncryptString(passWord);
            DataProvider.Instance.Database.SaveChanges();
            return true;
        }
        public void ChangePassWordOfCurrentUser(string passWord, USER user)
        {
            user.PASSWORD = SHA256Cryptography.Instance.EncryptString(passWord);
            DataProvider.Instance.Database.SaveChanges();
        }
        public bool CheckLogin(string userName, string passWord)
        {
            var user = DataProvider.Instance.Database.USERS.Where(tmpUser => tmpUser.USERNAME == userName && tmpUser.PASSWORD == passWord).ToList();
            if (user.Count() > 0)
                return true;
            return false;
        }
    }

}
