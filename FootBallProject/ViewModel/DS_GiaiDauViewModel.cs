using FootBallProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.ViewModel
{
    public class DS_GiaiDauViewModel:BaseViewModel
    {
        private ObservableCollection<LEAGUE> _Leagues;
        public ObservableCollection<LEAGUE> Leagues { get => _Leagues; set { _Leagues = value; OnPropertyChanged(); } }

        public DS_GiaiDauViewModel()
        {
            Leagues = new ObservableCollection<LEAGUE>(DataProvider.ins.DB.LEAGUEs.Where(p=>p.NGAYBATDAU!=null));
        }
    }
}
