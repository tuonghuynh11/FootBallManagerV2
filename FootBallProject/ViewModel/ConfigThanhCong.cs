using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace FootBallProject.ViewModel
{
    public class ConfigThanhCong : BaseViewModel
    {
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); } 
        }
        public ICommand ThanhCong { get; set; }
        public ConfigThanhCong(string message)
        {
            ThanhCong = new RelayCommand<object>(p => { return true; }, p => { ListofLeagueViewModel.Instance.Refresh(); });
            Message = message;
        }
    }
}
