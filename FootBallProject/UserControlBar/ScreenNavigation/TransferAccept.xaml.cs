using FootBallProject.Model;
using FootBallProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FootBallProject.UserControlBar.ScreenNavigation
{
    /// <summary>
    /// Interaction logic for TransferAccept.xaml
    /// </summary>
    public partial class TransferAccept : UserControl
    {
        public TransferAccept()
        {
            InitializeComponent();
        }

        private void AcceptTransferdgrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            CHUYENNHUONG teams = dataGrid.SelectedItem as CHUYENNHUONG;
            if (teams==null)
            {
                return;
            }
            else
            {
                if (dataGrid.CurrentCell.Column.DisplayIndex == 0)
                {
                    DOIBONG db = DataProvider.ins.DB.DOIBONGs.Find(teams.IDDOIMUA);
                    ThongTinCLB thongTinCLB = new ThongTinCLB(teams.TENDOIMUA);
                    ClubInfomationViewModel club = new ClubInfomationViewModel(db);

                    thongTinCLB.DataContext = club;
                    thongTinCLB.Show();
                }
                else if (dataGrid.CurrentCell.Column.DisplayIndex == 1)
                {
                    DOIBONG db = DataProvider.ins.DB.DOIBONGs.Find(teams.IDDOIBAN);
                    ThongTinCLB thongTinCLB = new ThongTinCLB(teams.TENDOIBAN);
                    ClubInfomationViewModel club = new ClubInfomationViewModel(db);

                    thongTinCLB.DataContext = club;
                    thongTinCLB.Show();
                }
                else if (dataGrid.CurrentCell.Column.DisplayIndex == 2)
                {
                    CAUTHU player = DataProvider.ins.DB.CAUTHUs.Find(teams.IDCAUTHU);
                    PlayerProfile playerProfile = new PlayerProfile(player);
                    playerProfile.ShowDialog();
                }
                else
                {
                    return;
                }

            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            CHUYENNHUONG teams = AcceptTransferdgrid.SelectedItem as CHUYENNHUONG;
            if (teams!=null)
            {
               List<HUANLUYENVIEN> bhldoimua = DataProvider.ins.DB.HUANLUYENVIENs.Where(p => p.IDDOIBONG == teams.IDDOIMUA && (p.CHUCVU == "HLV Trưởng" || p.CHUCVU == "Chủ tịch CLB")).ToList();
                //CAUTHU transfer = DataProvider.ins.DB.CAUTHUs.Find(teams.IDCAUTHU);
                //transfer.IDDOIBONG = teams.IDDOIMUA;
             
                foreach (HUANLUYENVIEN item in bhldoimua)
                {
                    DataProvider.ins.DB.Notifications.Add(new Notification() { IDHLV = item.ID, NOTIFY = $"Mua cầu thủ {teams.TENCAUTHU} không thành công", CHECKED = "Chưa xem" });
                }

                Notification rmnotify = DataProvider.ins.DB.Notifications.Where(p => p.NOTIFY.Contains(teams.CAUTHU.HOTEN.ToString()) && p.NOTIFY.Contains(teams.DOIBONG.TEN.ToString())).FirstOrDefault();

                DataProvider.ins.DB.Notifications.Remove(rmnotify);


                DataProvider.ins.DB.CHUYENNHUONGs.Remove(teams);
                DataProvider.ins.DB.SaveChanges();

                //Refresh lại transfer list
                TransferConfirmViewModel transferConfirm = new TransferConfirmViewModel();
                transferConfirm.TransferPlayers = new ObservableCollection<CHUYENNHUONG>(DataProvider.ins.DB.CHUYENNHUONGs.Where(p => p.IDDOIMUA != null));
                AcceptTransferdgrid.DataContext = transferConfirm;

                AcceptTransferdgrid.Items.Refresh();
            }
            else
            {
                return;
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            CHUYENNHUONG teams = AcceptTransferdgrid.SelectedItem as CHUYENNHUONG;

            if (teams != null)
            {
                List<HUANLUYENVIEN> bhldoiban = DataProvider.ins.DB.HUANLUYENVIENs.Where(p => p.IDDOIBONG == teams.IDDOIBAN && (p.CHUCVU == "HLV Trưởng" || p.CHUCVU == "Chủ tịch CLB")).ToList();
                List<HUANLUYENVIEN> bhldoimua = DataProvider.ins.DB.HUANLUYENVIENs.Where(p => p.IDDOIBONG == teams.IDDOIMUA && (p.CHUCVU == "HLV Trưởng" || p.CHUCVU == "Chủ tịch CLB")).ToList();
                CAUTHU transfer = DataProvider.ins.DB.CAUTHUs.Find(teams.IDCAUTHU);
                transfer.IDDOIBONG = teams.IDDOIMUA;
                foreach (HUANLUYENVIEN item in bhldoiban)
                {
                    DataProvider.ins.DB.Notifications.Add(new Notification() { IDHLV = item.ID, NOTIFY = $"Đã bán thành công cầu thủ {teams.TENCAUTHU}", CHECKED = "Chưa xem" });
                }
                foreach (HUANLUYENVIEN item in bhldoimua)
                {
                    DataProvider.ins.DB.Notifications.Add(new Notification() { IDHLV = item.ID, NOTIFY = $"Đã mua thành công cầu thủ {teams.TENCAUTHU}", CHECKED = "Chưa xem" });
                }

                Notification rmnotify = DataProvider.ins.DB.Notifications.Where(p => p.NOTIFY.Contains(teams.CAUTHU.HOTEN.ToString()) && p.NOTIFY.Contains(teams.DOIBONG.TEN.ToString())).FirstOrDefault();

                DataProvider.ins.DB.Notifications.Remove(rmnotify);
                DataProvider.ins.DB.CHUYENNHUONGs.Remove(teams);
                DataProvider.ins.DB.SaveChanges();

                //Refresh lại transfer list
                TransferConfirmViewModel transferConfirm = new TransferConfirmViewModel();
                transferConfirm.TransferPlayers=  new ObservableCollection<CHUYENNHUONG>(DataProvider.ins.DB.CHUYENNHUONGs.Where(p => p.IDDOIMUA != null));
                AcceptTransferdgrid.DataContext= transferConfirm;

                AcceptTransferdgrid.Items.Refresh();

                
            }
            else
            {
                return;
            }
        }
    }
}
