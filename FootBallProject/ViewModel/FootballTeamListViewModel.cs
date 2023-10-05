using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using DevExpress.Xpf.Grid;
using FootBallProject.Model;
using FootBallProject.UserControlBar.ScreenNavigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FootBallProject.ViewModel
{
    public class FootballTeamListViewModel:BaseViewModel
    {
        private ObservableCollection<DOIBONG> _DoiBongList;
        public ObservableCollection<DOIBONG> DoiBongList { get => _DoiBongList; set { _DoiBongList = value; OnPropertyChanged(); } }

        private List<string> _QuocTichList;
        public List<string> QuocTichList { get => _QuocTichList; set { _QuocTichList = value; OnPropertyChanged(); } }

        private List<string> _ThanhPhoList;
        public List<string> ThanhPhoList { get => _ThanhPhoList; set { _ThanhPhoList = value; OnPropertyChanged(); } }

        //tạo command trong gridcontrol 
        public DelegateCommand<NewRowArgs> AddingNewRowCommand { get; private set; }
        public DelegateCommand<RowValidationArgs> ValidateRowCommand { get; private set; }
        
         

        //tạo command trong gridcontrol 
        public FootballTeamListViewModel()
        {
            LoadDoiBong();
            LoadQuocTich();
            //tạo command trong gridcontrol 
            AddingNewRowCommand = new DelegateCommand<NewRowArgs>(AddingNewRow);
            ValidateRowCommand = new DelegateCommand<RowValidationArgs>(ValidateRow);
        
            //tạo command trong gridcontrol 
        }
        void LoadDoiBong()
        {
            DoiBongList = new ObservableCollection<DOIBONG>(DataProvider.ins.DB.DOIBONGs);
        }

        void LoadQuocTich()
        {
            QuocTichList = DataProvider.ins.DB.QUOCTICHes.Select(p=>p.TENQUOCGIA).ToList();

        }
        ///Thêm đội bóng mới

        [Command]//tạo command trong gridcontrol 
        public void AddingNewRow(NewRowArgs args)
        {
            args.Item = new DOIBONG()
            {
                ID = "",
                TEN = "",
                TENTHANHPHO = "",
                NGAYTHANHLAP = new DateTime(2022,1,1),
                SANNHA = "",
                QUOCGIA = "",
                IDQUOCTICH = 1,
                THANHPHO=1,
                SODOCHIENTHUAT = "4-3-3",
                SOLUONGTHANHVIEN = 0,
                GIATRI = 0,

            };
        }
        [Command]
        public void ValidateRow(RowValidationArgs args)
        {
            //var item = (DOIBONG)args.Item;
            //if (args.IsNewItem)
            //{
            //    try
            //    {
            //        item.HINHANH = FootballTeamList.buffer;
            //        DataProvider.ins.DB.DOIBONGs.Add(item);
            //        DataProvider.ins.DB.SaveChanges();

            //    }
            //    catch (Exception)
            //    {

            //        PopUpCustom popUpCustom = new PopUpCustom("Lỗi","ID đã tồn tại");
            //        popUpCustom.ShowDialog();
            //        DataProvider.ins.DB.DOIBONGs.Remove(item);
            //        DataProvider.ins.DB.SaveChanges();
                    
            //    }
                

            //}
        }
        

    }
}
