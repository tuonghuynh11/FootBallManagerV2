using FootBallProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallProject.ViewModel
{
    public class StatisticalChartViewModel:BaseViewModel
    {
        
        private ObservableCollection<DataPoint> _TeamsOfNations;
        public ObservableCollection<DataPoint> TeamsOfNations { get => _TeamsOfNations; set { _TeamsOfNations = value; OnPropertyChanged(); } }
        private ObservableCollection<DataPoint> _NumberOfWinsTeam;
        public ObservableCollection<DataPoint> NumberOfWinsTeam { get => _NumberOfWinsTeam; set { _NumberOfWinsTeam = value; OnPropertyChanged(); } }

        private string _NameOfMostExpensiveTeam;
        public string NameOfMostExpensiveTeam { get => _NameOfMostExpensiveTeam; set { _NameOfMostExpensiveTeam = value; OnPropertyChanged(); } }  

        private string _ValueOfMostExpensiveTeam;
        public string ValueOfMostExpensiveTeam { get => _ValueOfMostExpensiveTeam; set { _ValueOfMostExpensiveTeam = value; OnPropertyChanged(); } }

        public byte[] image { get; set; }


        public StatisticalChartViewModel()
        {
            TeamsOfNations = new ObservableCollection<DataPoint>(DataProvider.ins.DB.Database.SqlQuery<DataPoint>("Select  qt.TENQUOCGIA AS'Argument', COUNT(*) AS 'Value' FROM doibong as db join quoctich as qt on db.IDQUOCTICH = qt.ID Group by db.IDQUOCTICH, qt.TENQUOCGIA"));
            NumberOfWinsTeam = new ObservableCollection<DataPoint>(DataProvider.ins.DB.Database.SqlQuery<DataPoint>("SELECT DB.TEN AS'Argument', SUM(T1.WIN) AS 'Value' FROM THONGTINGIAIDAU T1 JOIN DOIBONG DB ON T1.IDDOIBONG=DB.ID GROUP BY DB.TEN"));
            DOIBONG dOIBONG = DataProvider.ins.DB.Database.SqlQuery<DOIBONG>("SELECT  * FROM doibong ORDER BY GIATRI desc").FirstOrDefault<DOIBONG>();

            NameOfMostExpensiveTeam = dOIBONG.TEN;
            ValueOfMostExpensiveTeam = String.Format("${0:n0}",dOIBONG.GIATRI);
            image = dOIBONG.HINHANH;

        }

    }
    public class DataPoint
    {
        public string Argument { get; private set; }
        public int Value { get; private set; }
        ///Phải gán bằng tên 2 thuộc tính này
    }
}
