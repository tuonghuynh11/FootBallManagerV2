using DevExpress.Xpf.Editors.Helpers;
using FootBallProject.Model;
using FootBallProject.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.Object
{
    public class RoundObject : BaseViewModel, INotifyDataErrorInfo
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
        private ROUND _currentRound;
        public ROUND CurrentRound
        {
            get { return _currentRound; }
            set { _currentRound = value; }
        }
        private string _nameofRound;
        public string NameOfRound
        {
            get { return _nameofRound; }
            set
            {
                _nameofRound = value;
                _errorBaseViewModel.ClearErrors();

                if (!IsValid(NameOfRound.ToString()))
                {
                    _errorBaseViewModel.AddError(nameof(NameOfRound), "Vui lòng điền tên vòng đấu");
                }

                OnPropertyChanged(nameof(NameOfRound));
            }
        }
        private DateTime? _startTime;
        public DateTime? StartTime
        {
            get { return _startTime; }
            set { _startTime = value;
                _errorBaseViewModel.ClearErrors();

                if (!IsValid(StartTime.ToString()))
                {
                    _errorBaseViewModel.AddError(nameof(StartTime), "Vui lòng chọn thời gian");
                }
                OnPropertyChanged(nameof(StartTime));}
        }
        private int? soluong;
        public int? SoLuong
        {
            get { return soluong; }
            set
            {
                soluong = value;
                _errorBaseViewModel.ClearErrors();

                
                OnPropertyChanged();
            }
        }
        public RoundObject(ROUND round)
        {
            _errorBaseViewModel= new ErrorBaseViewModel();
            _errorBaseViewModel.ErrorsChanged += ErrorBaseViewModel_ErrorsChanged;
            CurrentRound = round;
            NameOfRound = round.TENVONGDAU;
            StartTime = round.NGAYBATDAU;
            SoLuong = round.SOLUONGDOI;
        }
    }
}

