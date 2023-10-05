using FootBallProject.Component.Login;
using FootBallProject.Model;
using FootBallProject.Object;
using FootBallProject.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Security.Cryptography;
using FootBallProject.Utils;
using FootBallProject.Class;

namespace FootBallProject.ViewModel
{
    class LoginViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        #region property
        private readonly ErrorBaseViewModel _errorBaseViewModel;

        private string _oTP;
        public string OTP { get => _oTP; set => _oTP = value; }

        private string _timeCountDown;
        public string TimeCountDown
        {
            get => _timeCountDown;
            set
            {
                _timeCountDown = value;
                OnPropertyChanged();
            }
        }
        private bool _isGetCode;
        public bool IsGetCode
        {
            get => _isGetCode;
            set
            {
                _isGetCode = value;
                OnPropertyChanged();
            }
        }


        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;

                // Validation
                _errorBaseViewModel.ClearErrors();
                if (!IsValid(Username))
                {
                    _errorBaseViewModel.AddError(nameof(Username), "Vui lòng nhập tên đăng nhập!");
                }

                OnPropertyChanged();
            }
        }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;

                // Validation
                _errorBaseViewModel.ClearErrors();
                if (!IsValid(Password))
                {
                    _errorBaseViewModel.AddError(nameof(Password), "Vui lòng nhập mật khẩu!");
                }

                OnPropertyChanged();
            }
        }
        private string _gmail;
        public string Gmail
        {
            get => _gmail;
            set
            {
                _gmail = value;

                // Validation
                _errorBaseViewModel.ClearErrors();
                if (!IsValid(Gmail))
                {
                    _errorBaseViewModel.AddError(nameof(Gmail), "Vui lòng nhập gmail!");
                }
                if (!IsValidEmail(Gmail))
                {
                    _errorBaseViewModel.AddError(nameof(Gmail), "Địa chỉ mail không đúng định dạng!");
                }
                OnPropertyChanged();
            }
        }
        private string _oTPInView;
        public string OTPInView
        {
            get => _oTPInView;
            set
            {
                _oTPInView = value;

                // Validation
                _errorBaseViewModel.ClearErrors();
                if (!IsValid(OTPInView))
                {
                    _errorBaseViewModel.AddError(nameof(OTPInView), "Vui lòng nhập mã OTP!");
                }

                OnPropertyChanged();
            }
        }
        private string _newPassWord;
        public string NewPassWord
        {
            get => _newPassWord;
            set
            {
                _newPassWord = value;

                // Validation
                _errorBaseViewModel.ClearErrors();
                if (!IsValid(NewPassWord))
                {
                    _errorBaseViewModel.AddError(nameof(NewPassWord), "Vui lòng nhập mật khẩu mới!");
                }

                OnPropertyChanged();
            }
        }
        private string _reNewPassWord;
        public string ReNewPassWord
        {
            get => _reNewPassWord;
            set
            {
                _reNewPassWord = value;

                // Validation
                _errorBaseViewModel.ClearErrors();
                if (!IsValid(ReNewPassWord))
                {
                    _errorBaseViewModel.AddError(nameof(ReNewPassWord), "Vui lòng nhập lại mật khẩu mới!");
                }
                if (IsValid(ReNewPassWord))
                {
                    if (!ReNewPassWord.Equals(NewPassWord))
                    {
                        _errorBaseViewModel.AddError(nameof(ReNewPassWord), "Mật khẩu nhập lại không trùng với mật khẩu mới");
                    }
                }
                OnPropertyChanged();
            }
        }
        private bool _isToRemember;
        public bool IsToRemember { get => _isToRemember; set => _isToRemember = value; }

        private Account _rememberedAccount;
        public Account RememberedAccount { get => _rememberedAccount; set { _rememberedAccount = value; } }
        public bool HasErrors => _errorBaseViewModel.HasErrors;
        public object CurrentView { get => _currentView; set { _currentView = value; OnPropertyChanged(); } }
        private object _currentView;
        private ICommand _switchview;
        public ICommand SwichView
        {
            get => _switchview;
            set { _switchview = value; }
        }
        #endregion
        #region command
        private ICommand _rememberUserCommand;
        private ICommand _goToMainWindowCommand;
        public ICommand RememberUserCommand { get => _rememberUserCommand; set { _rememberUserCommand = value; } }
        public ICommand GoToMainWindowCommand { get => _goToMainWindowCommand; set { _rememberUserCommand = value; } }
        public ICommand GetOTPCodeCommand { get => _getOTPCodeCommand; set => _getOTPCodeCommand = value; }

        public ICommand Hoangmang { get; set; }
        private ICommand _getOTPCodeCommand;

        public ICommand ConFirmCommand { get => _conFirmCommand; set => _conFirmCommand = value; }

        private ICommand _conFirmCommand;
        #endregion
        public static USER user;
        public LoginViewModel()
        {
            IsGetCode = false;
            TimeCountDown = null;
            _errorBaseViewModel = new ErrorBaseViewModel();
            _errorBaseViewModel.ErrorsChanged += ErrorBaseViewModel_ErrorsChanged;
            InitRememberedAccount();
            if (RememberedAccount != null)
            {
                Username = RememberedAccount.UserName;
                Password = RememberedAccount.PassWord;
            }
            CurrentView = new Login();
            InitCommand();
        }
        public void InitRememberedAccount()
        {
            RememberedAccount = null;
            string filePath = LoginServices.FilePathRememberedAccount;
            if (File.Exists(filePath))
            {
                string fileContent = "";
                using (StreamReader sr = new StreamReader(filePath))
                {
                    fileContent = sr.ReadToEnd();
                    string accountRow = fileContent.Split('\n')[0];
                    if (accountRow == "")
                        return;
                    string[] account = accountRow.Split('\t');
                    RememberedAccount = new Account(account[0], LoginServices.Decrypt(account[1], "S7uMan"));
                }
            }
            else
            {
                //File.CreateText(LoginServices.FilePathRememberedAccount);
            }
        }
        public void RememberUser()
        {
            try
            {
                if (!IsToRemember)
                {
                }
                else
                {
                    // Note Remember Account to RAM and disk
                    if (LoginServices.Instance.IsUserAuthentic(Username, Password))
                    {
                        try
                        {
                            RememberedAccount = new Account(Username, Password);
                            using (StreamWriter sw = new StreamWriter(LoginServices.FilePathRememberedAccount))
                            {
                                sw.Write(RememberedAccount.UserName + '\t' + LoginServices.Encrypt(RememberedAccount.PassWord, "S7uMan"));
                            }
                        }
                        catch { }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Đã có lỗi trong việc ghi nhớ tài khoản", "Thông báo", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
        private void InitCommand()
        {
            SwichView = new RelayCommand<object>((p) => { return true; }, (p) => { SwichView1(); });
            GoToMainWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => { DoOpenMainWindow(); });
            Hoangmang = new RelayCommand<object>((p) => { return true; }, (p) => { RememberUser(); });
            GetOTPCodeCommand = new RelayCommand<object>((p) => { return true; }, async (p) => await GetOPTAsync());
            ConFirmCommand = new RelayCommand<object>((p) => true, (p) => ConFirm());
        }
        private void DoOpenMainWindow()
        {

            if (IsExistAccount())
            {
                LoginSuccessful();
                return;
            }
        }
        private void LoginSuccessful()
        {
            USER user = DataProvider.Instance.Database.USERS.Where(x => x.USERNAME == Username).First();
            USER.ROLE = user.ROLENAME;
            USER.USERN = user.USERNAME;
            AccessUser.userLogin = user;
            if (user.ROLENAME != "Admin")
            {
                //lấy id đội bóng của hlv đang đăng nhập trừ admin
                USER.IDDB = DataProvider.ins.Database.HUANLUYENVIENs.Where(x => x.ID == user.IDNHANSU).FirstOrDefault().IDDOIBONG;
            }

            //Window window = Application.Current.MainWindow as Window;
            //MainWindow2 mainWindow = new MainWindow2();
            //Application.Current.MainWindow = mainWindow;
            //Application.Current.MainWindow.Show();
            //window.Close();
            Window window = Application.Current.MainWindow as Window;
            AdminScreen mainWindow = new AdminScreen();
            Application.Current.MainWindow = mainWindow;
            Application.Current.MainWindow.Show();
            window.Close();
        }
        public bool IsExistAccount()
        {
            try
            {
                if (LoginServices.Instance.IsUserAuthentic(Username, Password))
                {
                    LoginServices.Instance.Login(Username);
                    return true;
                }

                Error f = new Error("Tên đăng nhập hoặc mật khẩu không đúng!\nVui lòng thử lại!");
                f.ShowDialog();
                return false;
            }
            catch
            {
                Error f = new Error("Đã có lỗi trong việc xác nhận tài khoản");
                f.ShowDialog();
                //MessageBox.Show("Đã có lỗi trong việc xác nhận tài khoản", "Thông báo", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }

            /*try
            {
                
            }
            catch
            {
                _ = MyMessageBox.Show("Xảy ra lỗi kết nối đến cơ sở dữ liệu!\nVui lòng thử lại!", "Đăng nhập thất bại", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }*/
        }

        private void SwichView1()
        {
            if (CurrentView == null || CurrentView.GetType() == typeof(Login))
            {
                CurrentView = new ForgotPassword();
            }
            else if (CurrentView.GetType() == typeof(ForgotPassword))
            {
                CurrentView = new Login();
            }
        }
        public void ResetView()
        {
            SwichView1();
        }
        bool IsValidEmail(string email)
        {
            if (email.Trim().EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private bool IsValid(string propertyName)
        {
            return !string.IsNullOrEmpty(propertyName) && !string.IsNullOrWhiteSpace(propertyName);
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            var tmp = Convert.ToInt32(TimeCountDown.Split(' ')[0]);
            tmp -= 1;
            TimeCountDown = tmp.ToString() + " Giây";
            if (tmp == 0)
            {
                (sender as DispatcherTimer).Stop();
                TimeCountDown = null;
            }
        }
        public async Task GetOPTAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(Gmail))
                {
                    Error f = new Error("Cần phải nhập địa chỉ mail trước khi lấy mã");
                    f.ShowDialog();
                    return;
                }
                if (!IsValidEmail(Gmail))
                {
                    Error f = new Error("Email không hợp lệ");
                    f.ShowDialog();
                    return;
                }

                StartCountdown();
                OTP = RandomOTP();
                await OTPServices.Instance.SaveOTP(Gmail, SHA256Cryptography.Instance.EncryptString(OTP));
                await SetupAndSendOTPForEmailAsync();
            }
            catch
            {
                Error f = new Error("Có lỗi trong việc tạo OTP");
                f.ShowDialog();
            }
        }
        public string RandomOTP()
        {
            Random generator = new Random();
            return generator.Next(0, 1000000).ToString("D6");
        }
        public void StartCountdown()
        {
            IsGetCode = true;
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            TimeCountDown = "60 Giây";
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();
        }
        public async Task SetupAndSendOTPForEmailAsync()
        {
            var body = File.ReadAllText("../../ResourceXAML/mail.html");
            var from = new MailAddress("footballmanagement111@gmail.com", "Football Manager");
            var to = new MailAddress(Gmail.Trim());
            MailMessage mm = new MailMessage(from, to)
            {
                Subject = OTP + " là mã khôi phục tài khoản của bạn",
                IsBodyHtml = true,
                Body = body.Replace("OTP_CODE", OTP),
                Priority = MailPriority.High
            };
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new System.Net.NetworkCredential("footballmanagement111@gmail.com", "upovphfgbfmhacux"),
                EnableSsl = true
            };
            try
            {
                await smtp.SendMailAsync(mm);
            }
            catch (Exception)
            {
                Error f = new Error("Không thể gửi OTP");
                f.ShowDialog();
            }
        }
        public void ConFirm()
        {
            try
            {
                OTPServices.Instance.DeleteOTPOverTime();
                if (!OTPServices.Instance.CheckGetOTPFromEmail(Gmail, SHA256Cryptography.Instance.EncryptString(OTPInView)))
                {
                    Error f = new Error("Mã xác nhận không chính xác");
                    f.ShowDialog();
                    return;
                }
                if (UserServices.Instance.ChangePassWord(NewPassWord, Gmail))
                {
                    Success f = new Success();
                    f.ShowDialog();
                    CurrentView = new Login();
                }
                else
                {
                    Error f = new Error("Cập nhật mật khẩu thất bại");
                    f.ShowDialog();
                }
            }
            catch
            {
                Error f = new Error("Có lỗi trong việc cập nhật mật khẩu mới");
                f.ShowDialog();

            }


        }
        private void ErrorBaseViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorBaseViewModel.GetErrors(propertyName);
        }
    }

}
