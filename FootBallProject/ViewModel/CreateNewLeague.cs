using DevExpress.Xpf.Editors.Helpers;
using FootBallProject.Model;
using FootBallProject.Object;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace FootBallProject.ViewModel
{
    public class CreateNewLeague:BaseViewModel, INotifyDataErrorInfo
    {
        #region error
        public bool HasErrors => _errorBaseViewModel.HasErrors;
        private readonly ErrorBaseViewModel _errorBaseViewModel;
        private void ErrorBaseViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorBaseViewModel.GetErrors(propertyName);
        }
        private bool IsValid(string propertyName)
        {
            return !string.IsNullOrEmpty(propertyName) && !string.IsNullOrWhiteSpace(propertyName);
        }
        #endregion
        private string _displayName;
        private DateTime? _startTime;
        private DateTime? _endTime;
        private string _soDoi;
        private LEAGUE league;
        private static CreateNewLeague _instance;
        public static CreateNewLeague Instance
        {
            get { return _instance; }
            set { _instance = value; }
        }
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value;
                _errorBaseViewModel.ClearErrors();
                if (!IsValid(DisplayName))
                {
                    _errorBaseViewModel.AddError(nameof(DisplayName), "Vui lòng nhập tên giải đấu!");
                }
                CanGoNext();
                OnPropertyChanged(); }
        }
        public LEAGUE League
        {
            get { return league; }
            set { league = value;
                OnPropertyChanged(); }
        }
        public DateTime? StartTime
        {
            get { return _startTime; }
            set { _startTime = value;
                _errorBaseViewModel.ClearErrors();
                if (!IsValid(StartTime.ToString()))
                {
                    _errorBaseViewModel.AddError(nameof(StartTime), "Vui lòng chọn thời gian bắt đầu!");
                }
                CanGoNext();
                OnPropertyChanged();
            }
        }
        public DateTime? EndTime
        {
            get => _endTime;
            set { _endTime = value;
                _errorBaseViewModel.ClearErrors();
                if (!IsValid(EndTime.ToString()))
                {
                    _errorBaseViewModel.AddError(nameof(EndTime), "Vui lòng chọn thời gian kết thúc!");
                }
                else if (DateTime.Compare(StartTime.TryConvertToDateTime(), EndTime.TryConvertToDateTime()) > 0)
                {
                    _errorBaseViewModel.AddError(nameof(EndTime), "Thời gian kết thúc không hợp lệ");
                }
                CanGoNext();
                OnPropertyChanged();
            }
        }
        public string SoDoi
        {
            get { return _soDoi; }
            set { _soDoi = value;
                _errorBaseViewModel.ClearErrors();
                if (!IsValid(SoDoi))
                {
                    _errorBaseViewModel.AddError(nameof(SoDoi), "Vui lòng chọn số đội!");
                }
                CanGoNext();
                OnPropertyChanged(); }
        }
        private QUOCTICH quocTich;
        public QUOCTICH QuocTich
        {
            get { return quocTich; }
            set { quocTich = value;
                _errorBaseViewModel.ClearErrors();
                if (QuocTich == null)
                {
                    _errorBaseViewModel.AddError(nameof(SoDoi), "Vui lòng chọn quốc gia!");
                }

                OnPropertyChanged(); CanGoNext();
            }
        }
        private DIADIEM diadiem;
        public DIADIEM Diadiem
        {
            get { return diadiem; }
            set { diadiem = value;
                _errorBaseViewModel.ClearErrors();
                if (Diadiem == null)
                {
                    _errorBaseViewModel.AddError(nameof(Diadiem), "Vui lòng chọn địa điểm!");
                }
                
                OnPropertyChanged(); CanGoNext();
            }
        }
        private ObservableCollection<string> soluongdois;
        public ObservableCollection<string> SoluongDois
        {
            get => soluongdois;
            set { soluongdois = value; OnPropertyChanged();  }
        }
        private string selectedSoluong;
        public string SelectedSoluong
        {
            get { return selectedSoluong; }
            set { selectedSoluong = value;
                _errorBaseViewModel.ClearErrors();
                if (!IsValid(SelectedSoluong))
                {
                    _errorBaseViewModel.AddError(nameof(SoDoi), "Vui lòng chọn quốc gia!");
                }
                OnPropertyChanged();
                CanGoNext();
            }
        }
        private ObservableCollection<DIADIEM> diadiemlist = new ObservableCollection<DIADIEM>();
        public ObservableCollection<DIADIEM> DiaDiemList
        {
            get { return diadiemlist; }
            set { diadiemlist = value;}
        }
        private ObservableCollection<QUOCTICH> quocgialist = new ObservableCollection<QUOCTICH>();
        public ObservableCollection<QUOCTICH> QuocGiaList
        {
            get { return quocgialist; }
            set {   quocgialist = value; }
        }

        public ICommand Next { get; set; }
        public ICommand Return { get; set; }
        public ICommand AddAvatar { get; set; }
        private string linkAvatar;
        public string LinkAvatar
        {
            get { return linkAvatar; }
            set { linkAvatar = value; OnPropertyChanged(); }
        }
        public BitmapImage img;
        public CreateNewLeague()
        {
            Instance= this;
            _errorBaseViewModel = new ErrorBaseViewModel();
            _errorBaseViewModel.ErrorsChanged += ErrorBaseViewModel_ErrorsChanged;
            soluongdois= new ObservableCollection<string>() { "4", "8", "16" };
            SoluongDois = soluongdois;
            var list = DataProvider.ins.DB.DIADIEMs.ToList();
            foreach(var item in list)
            {
                diadiemlist.Add(item);
            }
            DiaDiemList = diadiemlist;
            var list1 = DataProvider.ins.DB.QUOCTICHes.ToList();
            foreach (var item in list1)
            {
                quocgialist.Add(item);
            }
            Enable = false;
            QuocGiaList = quocgialist;
            Next = new RelayCommand<object>((p) => { return CanGoNext(); }, (p) => { GoNext(); ListofLeagueViewModel.Instance.GoNext(); });
            Return = new RelayCommand<object>((p) => { return true; }, (p) => { ListofLeagueViewModel.Instance.Return(); });
            AddAvatar = new RelayCommand<object>(p => { return true; }, p => AddAvatarFuntion());
        }
        private void AddAvatarFuntion()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                img = new BitmapImage(new Uri(openFileDialog.FileName));
                LinkAvatar = System.IO.Path.GetFileName( openFileDialog.FileName);
            }
        }
        private bool enable;
        public bool Enable
        {
            get { return enable; }
            set { enable = value; OnPropertyChanged(); }
        }
        public bool CanGoNext()
        {
            int x = DateTime.Compare(StartTime.TryConvertToDateTime(), EndTime.TryConvertToDateTime());
            if (DisplayName != "" && QuocTich != null && x < 0 && Diadiem != null && StartTime != null && EndTime != null && SelectedSoluong != null) { Enable = true; return true; }
            return false;
        }
        public void GoNext()
        {
            Instance = this;
            if (CreateNewLeague.Instance.League == null)
            {
                CreateNewLeague.Instance.League = new LEAGUE()
                {
                    TENGIAIDAU = CreateNewLeague.Instance.DisplayName,
                    NGAYBATDAU = CreateNewLeague.Instance.StartTime,
                    NGAYKETTHUC = CreateNewLeague.Instance.EndTime,
                    IDQUOCGIA = CreateNewLeague.Instance.QuocTich.ID,
                };
            }
            else
            {
                CreateNewLeague.Instance.League.TENGIAIDAU = CreateNewLeague.Instance.DisplayName;
                CreateNewLeague.Instance.League.NGAYBATDAU = CreateNewLeague.Instance.StartTime;
                CreateNewLeague.Instance.League.NGAYKETTHUC = CreateNewLeague.Instance.EndTime;
                CreateNewLeague.Instance.League.IDQUOCGIA = CreateNewLeague.Instance.QuocTich.ID;
            }
        }
    }
}
