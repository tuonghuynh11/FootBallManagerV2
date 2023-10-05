using FootBallProject.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.Service
{
    public class OTPServices
    {
        private static OTPServices s_instance;

        public static OTPServices Instance => s_instance ?? (s_instance = new OTPServices());

        public OTPServices() { }

        public bool CheckGetOTPFromEmail(string email, string OTP)
        {
            var user = UserServices.Instance.GetUserByGmail(email);
            if (user.Count == 0)
                return false;
            if (user.FirstOrDefault().IDOTP == null)
                return false;
            if (user.FirstOrDefault().OTP.Code.Equals(OTP))
                return true;
            return false;
        }
        public async Task SaveOTP(string email, string OTP)
        {
            var user = UserServices.Instance.GetUserByGmail(email);
            if (user.Count == 0)
                return;
            var otp = new OTP()
            {
                Code = OTP,
                Time = DateTime.Now,
            };
            DataProvider.Instance.Database.OTPs.AddOrUpdate(otp);
            user.FirstOrDefault().OTP = otp;
            await DataProvider.Instance.Database.SaveChangesAsync();
        }
        public void DeleteOTPOverTime()
        {
            if (DataProvider.Instance.Database.OTPs.ToList().Count == 0)
                return;
            var listOTP = DataProvider.Instance.Database.OTPs.Where(otp => DbFunctions.DiffMinutes(otp.Time, DateTime.Now) > 5).ToList();
            if (listOTP.Count < 0)
                return;
            foreach (var otp in listOTP)
            {
                DataProvider.Instance.Database.OTPs.Remove(otp);
                var user = UserServices.Instance.GetUserByOTP(otp);
                try
                {
                   if(user != null) user.IDOTP = null;
                }
                catch { }
            }
            DataProvider.Instance.Database.SaveChanges();
        }
    }

}
