using FootBallProject.Class.Format_team;
using FootBallProject.Model;
using FootBallProject.PopUp;
using FootBallProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
using System.Drawing;
namespace FootBallProject.UserControlBar.ScreenNavigation
{
    /// <summary>
    /// Interaction logic for TeamBuilder.xaml
    /// </summary>
    public partial class TeamBuilder : UserControl
    {
       

        //Giữ đội hình dự bị ban đầu không thay đổi
        List<CAUTHU> mainsubteam;

        //Giữ đội hình ban đầu không thay đổi
        List<CAUTHU> mainteam;

        CAUTHU termPlayer;
        CAUTHU termPlayer1;
        int term = 0;
        int term1 = 0;

        //id của đội đang đăng nhập
        public string id_doi =USER.IDDB;
        //id của đội đang đăng nhập


        //Lưu vị trí thay đổi trong đội hình chính
        Dictionary<int, string> changeteam;

        public TeamBuilderViewModel teamBuilderViewModel;


        //Lưu lần đầu đăng nhập
        int dhfirst = 0;
        public TeamBuilder()
        {
            InitializeComponent();
            this.DataContext = teamBuilderViewModel = new TeamBuilderViewModel(USER.IDDB);

          //  this.DataContext = new TeamBuilderViewModel(USER.IDDB);
            mainteam = new List<CAUTHU>();

            mainsubteam = new List<CAUTHU>();

            termPlayer = new CAUTHU();
            termPlayer1 = new CAUTHU();

          changeteam = new Dictionary<int, string>();  

         
        

        }
        //Thanh tìm kiếm cầu thủ
       
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tbx = (TextBox)sender;
            if (tbx.Text!="")
            {
                var filterList = teamBuilderViewModel.MainTeamPlayers.Where(x=>x.HOTEN.ToLower().Contains(tbx.Text.ToLower()));
                dtgDSCauThu.ItemsSource = null;
                dtgDSCauThu.ItemsSource = filterList;
            }
            else
            {
                dtgDSCauThu.ItemsSource = teamBuilderViewModel.MainTeamPlayers;
            }
        }

       
        //Binding Thông tin cầu thủ
        private void dtgDSCauThu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgDSCauThu.SelectedItems.Count>0)
             {
                //Thêm chuỗi path để truyền image cho ImageBrush//
                const string path = "pack://application:,,,";
                //Thêm chuỗi path để truyền image cho ImageBrush//
                DataGrid dataGrid = (DataGrid)sender;
                CAUTHU pl = dataGrid.SelectedItem as CAUTHU;
                //Lưu vị trí cầu thủ đã chọn để đổi chỗ
                if (termPlayer.HOTEN == null)
                {
                    termPlayer = pl;
                    term = 1;
                    return;
                }
                else if (termPlayer.HOTEN != null && term != 1)
                {
                    termPlayer = pl;
                    term = 1;
                    term1 = 0;
                    return;
                }



                if (termPlayer1.HOTEN == null && term != 0)
                {
                    termPlayer1 = pl;
                    term1 = 1;
                    term = 0;
                }
                else if (termPlayer1.HOTEN != null && term1 != 1)
                {
                    termPlayer1 = pl;
                    term1 = 1;
                    term = 0;
                }

            }

        }

        private void bttPlayerExchange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Thêm chuỗi path để truyền image cho ImageBrush//
                const string path = "pack://application:,,,";
                //Thêm chuỗi path để truyền image cho ImageBrush//
                if (dtgDSCauThu.SelectedItems.Count > 0)
                {
                    CAUTHU a = dtgDSCauThu.SelectedItems[0] as CAUTHU;
                    CAUTHU term2 = a;

                    if (dtgDSCauThuDuBi.SelectedItems.Count > 0)
                    {
                        CAUTHU sub = dtgDSCauThuDuBi.SelectedItems[0] as CAUTHU;


                        if (a.VITRIAO == "GK" && sub.VITRI != "GK")
                        {
                            PopUpCustom popUpCustom = new PopUpCustom();
                            popUpCustom.Show();
                        }
                        else
                        {
                            int main = mainteam.IndexOf(a);
                            int subindex = 0;
                            foreach (CAUTHU item in mainsubteam)
                            {
                                if (item.ID == sub.ID)
                                {
                                    break;
                                }
                                subindex++;
                            }

                            mainteam[main] = sub;
                            mainteam[main].VITRIAO = term2.VITRIAO;

                            mainsubteam[subindex] = a;


                            dtgDSCauThuDuBi.Items.Refresh();
                            dtgDSCauThuDuBi.UnselectAll();
                            dtgDSCauThuDuBi.InvalidateProperty(DataGrid.SelectedItemProperty);
                            dtgDSCauThuDuBi.SetValue(DataGrid.SelectedItemProperty, a);
                            dtgDSCauThuDuBi.Items.Refresh();

                            dtgDSCauThu.InvalidateProperty(DataGrid.SelectedItemProperty);
                            dtgDSCauThu.SetValue(DataGrid.SelectedItemProperty, mainteam[main]);
                            dtgDSCauThu.Items.Refresh();

                        }

                    }
                    else
                    {

                        int curentitem = mainteam.IndexOf(a);
                        string vitriao3 = a.VITRIAO;
                        string vitriao2 = "";
                        int curentreplaceitem2 = 0;
                        CAUTHU current = new CAUTHU();
                        current = a;
                        CAUTHU replace = new CAUTHU();
                        int flag = 0;
                        if (term == 0)
                        {
                            //a.exchange(termplayer);
                            //termplayer = a;
                            replace = termPlayer;
                            int curentreplaceitem = curentreplaceitem2 = mainteam.IndexOf(termPlayer);
                            string vitriao = vitriao2 = termPlayer.VITRIAO;
                            if ((a.VITRIAO == "GK" && mainteam[curentreplaceitem].VITRIAO != "GK") || (a.VITRIAO != "GK" && mainteam[curentreplaceitem].VITRIAO == "GK"))
                            {
                                flag = 1;
                                PopUpCustom popUpCustom = new PopUpCustom();
                                popUpCustom.Show();
                            }
                            else
                            {
                                //Set vi trí mặc định trong đội hình chiến thuật
                                mainteam[curentitem] = termPlayer;
                                mainteam[curentreplaceitem] = a;



                                a.VITRIAO = vitriao;
                                termPlayer = a;

                                term = 1;
                                term1 = 0;

                                changeteam[mainteam[curentitem].ID] = vitriao3;
                                changeteam[mainteam[curentreplaceitem].ID] = vitriao2;

                                flag = 0;
                            }

                        }

                        //nếu term1 đang =0
                        else
                        {
                            replace = termPlayer1;
                            int curentreplaceitem1 = curentreplaceitem2 = mainteam.IndexOf(termPlayer1);
                            string vitriao1 = vitriao2 = termPlayer1.VITRIAO;

                            if ((a.VITRIAO == "GK" && mainteam[curentreplaceitem1].VITRIAO != "GK") || (a.VITRIAO != "GK" && mainteam[curentreplaceitem1].VITRIAO == "GK"))
                            {
                                flag = 1;
                                PopUpCustom popUpCustom = new PopUpCustom();
                                popUpCustom.Show();
                            }
                            else
                            {
                                mainteam[curentitem] = termPlayer1;
                                mainteam[curentreplaceitem1] = a;
                                a.VITRIAO = vitriao1;
                                termPlayer1 = a;
                                term1 = 1;
                                term = 0;

                                changeteam[mainteam[curentitem].ID] = vitriao3;
                                changeteam[mainteam[curentreplaceitem1].ID] = vitriao2;

                                flag = 1;
                            }



                        }
                        TeamBuilderViewModel newVm1 = new TeamBuilderViewModel();
                        // newVm.MainTeamPlayers = mainteam;
                        newVm1.SubTeamPlayers = mainsubteam;

                        ///
                        if (DoiHinhChienThuat433UC.Visibility == Visibility.Visible)
                        {
                            // team_433 = new Team_433(mainteam);
                            if (flag != 1)
                            {
                                int j = 0;
                                foreach (CAUTHU item in mainteam)
                                {
                                    if (item.ID == replace.ID)
                                    {
                                        item.VITRIAO = vitriao3;
                                        j++;
                                    }
                                    if (item.ID == current.ID)
                                    {
                                        item.VITRIAO = vitriao2;
                                        j++;
                                    }
                                    if (j == 2)
                                    {
                                        break;
                                    }
                                }

                            }
                            foreach (CAUTHU item in mainteam)
                            {
                                if (changeteam.ContainsKey(item.ID))
                                {
                                    item.VITRIAO = changeteam[item.ID];
                                }
                            }
                            newVm1.Teamformat = new Team_433(mainteam, "basic");
                            newVm1.MainTeamPlayers = newVm1.Teamformat.team;

                            DoiHinhChienThuat433UC.DataContext = newVm1.Teamformat;
                        }
                        else if (DoiHinhChienThuat442UC.Visibility == Visibility.Visible)
                        {
                            if (flag != 1)
                            {
                                int j = 0;
                                foreach (CAUTHU item in mainteam)
                                {
                                    if (item.ID == replace.ID)
                                    {
                                        item.VITRIAO = vitriao3;
                                        j++;
                                    }
                                    if (item.ID == current.ID)
                                    {
                                        item.VITRIAO = vitriao2;
                                        j++;
                                    }
                                    if (j == 2)
                                    {
                                        break;
                                    }
                                }
                            }
                            foreach (CAUTHU item in mainteam)
                            {
                                if (changeteam.ContainsKey(item.ID))
                                {
                                    item.VITRIAO = changeteam[item.ID];
                                }
                            }
                            newVm1.Teamformat = new Team_442(mainteam, "basic");
                            newVm1.MainTeamPlayers = newVm1.Teamformat.team;

                            DoiHinhChienThuat442UC.DataContext = newVm1.Teamformat;
                        }
                        else if (DoiHinhChienThuat4231UC.Visibility == Visibility.Visible)
                        {
                            if (flag != 1)
                            {
                                int j = 0;
                                foreach (CAUTHU item in mainteam)
                                {
                                    if (item.ID == replace.ID)
                                    {
                                        item.VITRIAO = vitriao3;
                                        j++;
                                    }
                                    if (item.ID == current.ID)
                                    {
                                        item.VITRIAO = vitriao2;
                                        j++;
                                    }
                                    if (j == 2)
                                    {
                                        break;
                                    }
                                }
                            }
                            foreach (CAUTHU item in mainteam)
                            {
                                if (changeteam.ContainsKey(item.ID))
                                {
                                    item.VITRIAO = changeteam[item.ID];
                                }
                            }
                            newVm1.Teamformat = new Team_4231(mainteam, "basic");
                            newVm1.MainTeamPlayers = newVm1.Teamformat.team;

                            DoiHinhChienThuat4231UC.DataContext = newVm1.Teamformat;
                        }


                        // Xử lý khi trao đổi phải trỏ đến cầu thủ đang đổi
                        ///

                        this.DataContext = newVm1;

                        dtgDSCauThu.InvalidateProperty(DataGrid.SelectedItemProperty);
                        dtgDSCauThu.SetValue(DataGrid.SelectedItemProperty, a);
                        dtgDSCauThu.Items.Refresh();

                        return;
                    }

                    //Gán lại đội hình mới
                    TeamBuilderViewModel newVm = new TeamBuilderViewModel();
                    // newVm.MainTeamPlayers = mainteam;
                    newVm.SubTeamPlayers = mainsubteam;

                    ///
                    if (DoiHinhChienThuat433UC.Visibility == Visibility.Visible)
                    {
                        // team_433 = new Team_433(mainteam);
                        foreach (CAUTHU item in mainteam)
                        {
                            if (changeteam.ContainsKey(item.ID))
                            {
                                item.VITRIAO = changeteam[item.ID];
                            }
                        }
                        newVm.Teamformat = new Team_433(mainteam, "basic");
                        newVm.MainTeamPlayers = newVm.Teamformat.team;

                        DoiHinhChienThuat433UC.DataContext = newVm.Teamformat;
                    }
                    else if (DoiHinhChienThuat442UC.Visibility == Visibility.Visible)
                    {
                        foreach (CAUTHU item in mainteam)
                        {
                            if (changeteam.ContainsKey(item.ID))
                            {
                                item.VITRIAO = changeteam[item.ID];
                            }
                        }
                        newVm.Teamformat = new Team_442(mainteam, "basic");
                        newVm.MainTeamPlayers = newVm.Teamformat.team;

                        DoiHinhChienThuat442UC.DataContext = newVm.Teamformat;
                    }
                    else if (DoiHinhChienThuat4231UC.Visibility == Visibility.Visible)
                    {
                        foreach (CAUTHU item in mainteam)
                        {
                            if (changeteam.ContainsKey(item.ID))
                            {
                                item.VITRIAO = changeteam[item.ID];
                            }
                        }
                        newVm.Teamformat = new Team_4231(mainteam, "basic");
                        newVm.MainTeamPlayers = newVm.Teamformat.team;

                        DoiHinhChienThuat4231UC.DataContext = newVm.Teamformat;
                    }


                    // Xử lý khi trao đổi phải trỏ đến cầu thủ đang đổi
                    ///

                    this.DataContext = newVm;

                    //dtgDSCauThu.InvalidateProperty(DataGrid.SelectedItemProperty);
                    //dtgDSCauThu.SetValue(DataGrid.SelectedItemProperty, a);
                    //dtgDSCauThu.Items.Refresh();
                }
            }
            catch (Exception)
            {

                return;
            }
            
        }
       
        private void tbSearch1_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tbx = (TextBox)sender;
            if (tbx.Text != "")
            {
                var filterList = teamBuilderViewModel.SubTeamPlayers.Where(x => x.HOTEN.ToLower().Contains(tbx.Text.ToLower()));
                dtgDSCauThuDuBi.ItemsSource = null;
                dtgDSCauThuDuBi.ItemsSource = filterList;
            }
            else
            {
                dtgDSCauThuDuBi.ItemsSource = teamBuilderViewModel.SubTeamPlayers;
            }
        }

        private void DHChienThuatcbb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                
                if (dhfirst == 0)
                {
                    dhfirst = 1;
                    return;
                }

                // Xử lý chọn chiến thuật cho 3 màn hình chồng lên,chọn chiến thuật nào thì collaped 2 chiến thuật còn lại
                ComboBox combo = (ComboBox)sender;
                ComboBoxItem select = (ComboBoxItem)combo.SelectedItem;

                //id của đội đang đăng nhập
                // id_doi = "mc";
                List<CAUTHU> mainteamremain = (from a in DataProvider.ins.DB.CAUTHUs
                                               join b in DataProvider.ins.DB.DOIHINHCHINHs on a.ID equals b.IDCAUTHU
                                               where b.IDDOIBONG == id_doi
                                               select (a)).ToList<CAUTHU>();

                List<CAUTHU> mainsubteamremain = DataProvider.ins.DB.Database.SqlQuery<CAUTHU>("SELECT * FROM CAUTHU WHERE IDDOIBONG=@ID1 AND ID NOT IN (SELECT IDCAUTHU FROM DOIHINHCHINH WHERE IDDOIBONG = @ID1 ) ", new SqlParameter("@ID1", id_doi)).ToList<CAUTHU>();
                //id của đội đang đăng nhập

                if (mainsubteamremain==null || mainsubteamremain==null)
                {
                    return;
                }

                TeamBuilderViewModel newVm1 = new TeamBuilderViewModel();
                newVm1.SubTeamPlayers = mainsubteamremain;
                if (select.Content.ToString() == "4-3-3")
                {
                    DoiHinhChienThuat442UC.Visibility = Visibility.Collapsed;
                    DoiHinhChienThuat4231UC.Visibility = Visibility.Collapsed;
                    DoiHinhChienThuat433UC.Visibility = Visibility.Visible;
                    // team_433 = new Team_433(listPlayer);
                    //mainteam = team_433.team;

                    newVm1.Teamformat = new Team_433(mainteamremain, 1);
                    newVm1.MainTeamPlayers = newVm1.Teamformat.team;

                    DoiHinhChienThuat433UC.DataContext = newVm1.Teamformat;
                    mainteam = newVm1.MainTeamPlayers;
                    mainsubteam = newVm1.SubTeamPlayers;
                    dtgDSCauThuDuBi.Items.Refresh();
                }
                if (select.Content.ToString() == "4-4-2")
                {
                    DoiHinhChienThuat433UC.Visibility = Visibility.Collapsed;
                    DoiHinhChienThuat4231UC.Visibility = Visibility.Collapsed;
                    DoiHinhChienThuat442UC.Visibility = Visibility.Visible;
                    newVm1.Teamformat = new Team_442(mainteamremain, 1);
                    newVm1.MainTeamPlayers = newVm1.Teamformat.team;

                    DoiHinhChienThuat442UC.DataContext = newVm1.Teamformat;
                    mainteam = newVm1.MainTeamPlayers;
                    mainsubteam = newVm1.SubTeamPlayers;
                    dtgDSCauThuDuBi.Items.Refresh();
                }
                if (select.Content.ToString() == "4-2-3-1")
                {
                    DoiHinhChienThuat433UC.Visibility = Visibility.Collapsed;
                    DoiHinhChienThuat442UC.Visibility = Visibility.Collapsed;
                    DoiHinhChienThuat4231UC.Visibility = Visibility.Visible;
                    newVm1.Teamformat = new Team_4231(mainteamremain, 1);
                    newVm1.MainTeamPlayers = newVm1.Teamformat.team;

                    DoiHinhChienThuat4231UC.DataContext = newVm1.Teamformat;
                    mainteam = newVm1.MainTeamPlayers;
                    mainsubteam = newVm1.SubTeamPlayers;
                    dtgDSCauThuDuBi.Items.Refresh();
                }
                changeteam.Clear();
                this.DataContext = newVm1;
            }
            catch (Exception)
            {

                return ;
            }
          

        }
        ///hiển thị thông tin cầu thủ đầu tiên
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (teamBuilderViewModel.MainTeamPlayers.Count == 11 )
                {
                    if (teamBuilderViewModel.SubTeamPlayers.Count != 0)
                    {
                        CAUTHU mainplayer = teamBuilderViewModel.MainTeamPlayers[0];
                        CAUTHU subplayer = teamBuilderViewModel.SubTeamPlayers[0];

                        //main player
                        PlayerName.Text = mainplayer.HOTEN;
                        if (mainplayer.HINHANH.Length != 0)
                        {
                            PlayerImage.ImageSource = LoadImage(mainplayer.HINHANH);

                        }
                        PlayerNumber.Text = mainplayer.SOAO.ToString();
                        QuocTichtb.Text = mainplayer.QUOCGIA;
                        ChieuCaotb.Text = mainplayer.CHIEUCAO;
                        CanNangtb.Text = mainplayer.CANNANG;
                        TheTrangtb.Text = mainplayer.THETRANG;
                        ChanThuantb.Text = mainplayer.CHANTHUAN;

                        //sub palyer
                        PlayerNameSub.Text = subplayer.HOTEN;
                        if (subplayer.HINHANH.Length != 0)
                        {
                            PlayerImageSub.ImageSource = LoadImage(subplayer.HINHANH);

                        }
                        PlayerNumberSub.Text = subplayer.SOAO.ToString();
                        QuocTichSubtb.Text = subplayer.QUOCGIA;
                        ChieuCaoSubtb.Text = subplayer.CHIEUCAO;
                        CanNangSubtb.Text = subplayer.CANNANG;
                        TheTrangSubtb.Text = subplayer.THETRANG;
                        ChanThuanSubtb.Text = subplayer.CHANTHUAN;
                        //Thêm lựa chọn đội hình chiến thuật
                        if (teamBuilderViewModel.Team.SODOCHIENTHUAT == "4-3-3")
                        {
                            DoiHinhChienThuat442UC.Visibility = Visibility.Collapsed;
                            DoiHinhChienThuat4231UC.Visibility = Visibility.Collapsed;
                            DoiHinhChienThuat433UC.Visibility = Visibility.Visible;
                            DoiHinhChienThuat433UC.DataContext = teamBuilderViewModel.Teamformat;

                        }
                        else if (teamBuilderViewModel.Team.SODOCHIENTHUAT == "4-4-2")
                        {
                            DoiHinhChienThuat433UC.Visibility = Visibility.Collapsed;
                            DoiHinhChienThuat4231UC.Visibility = Visibility.Collapsed;
                            DoiHinhChienThuat442UC.Visibility = Visibility.Visible;

                            DoiHinhChienThuat442UC.DataContext = teamBuilderViewModel.Teamformat;
                        }
                        else
                        {
                            DoiHinhChienThuat433UC.Visibility = Visibility.Collapsed;
                            DoiHinhChienThuat442UC.Visibility = Visibility.Collapsed;
                            DoiHinhChienThuat4231UC.Visibility = Visibility.Visible;
                            DoiHinhChienThuat4231UC.DataContext = teamBuilderViewModel.Teamformat;

                        }

                        //Gán đội hình cho biến phụ để giữ nguyên đội hình
                        mainteam = (teamBuilderViewModel.MainTeamPlayers);
                        mainsubteam = teamBuilderViewModel.SubTeamPlayers;



                        //team
                        if (teamBuilderViewModel.Team.HINHANHHLV != null && teamBuilderViewModel.Team.HINHANHHLV.Length != 0)
                        {
                            HLVImage.ImageSource = LoadImage(teamBuilderViewModel.Team.HINHANHHLV);

                        }
                        HLVname.Text = teamBuilderViewModel.Team.HLV;
                        GTDH.Text = teamBuilderViewModel.Team.GIATRI.ToString();
                        DHChienThuatcbb.Text = teamBuilderViewModel.Team.SODOCHIENTHUAT;
                        id_doi = teamBuilderViewModel.Team.ID;
                        this.DataContext = teamBuilderViewModel;

                    }
                    else
                    {
                       // this.DataContext = new TeamBuilderViewModel() { MainTeamPlayers = teamBuilderViewModel.MainTeamPlayers };

                        CAUTHU mainplayer = teamBuilderViewModel.MainTeamPlayers[0];

                        //main player
                        PlayerName.Text = mainplayer.HOTEN;
                        if (mainplayer.HINHANH.Length != 0)
                        {
                            PlayerImage.ImageSource = LoadImage(mainplayer.HINHANH);

                        }
                        PlayerNumber.Text = mainplayer.SOAO.ToString();
                        QuocTichtb.Text = mainplayer.QUOCGIA;
                        ChieuCaotb.Text = mainplayer.CHIEUCAO;
                        CanNangtb.Text = mainplayer.CANNANG;
                        TheTrangtb.Text = mainplayer.THETRANG;
                        ChanThuantb.Text = mainplayer.CHANTHUAN;

                       
                        //Thêm lựa chọn đội hình chiến thuật
                        if (teamBuilderViewModel.Team.SODOCHIENTHUAT == "4-3-3")
                        {
                            DoiHinhChienThuat442UC.Visibility = Visibility.Collapsed;
                            DoiHinhChienThuat4231UC.Visibility = Visibility.Collapsed;
                            DoiHinhChienThuat433UC.Visibility = Visibility.Visible;
                            DoiHinhChienThuat433UC.DataContext = teamBuilderViewModel.Teamformat;

                        }
                        else if (teamBuilderViewModel.Team.SODOCHIENTHUAT == "4-4-2")
                        {
                            DoiHinhChienThuat433UC.Visibility = Visibility.Collapsed;
                            DoiHinhChienThuat4231UC.Visibility = Visibility.Collapsed;
                            DoiHinhChienThuat442UC.Visibility = Visibility.Visible;

                            DoiHinhChienThuat442UC.DataContext = teamBuilderViewModel.Teamformat;
                        }
                        else
                        {
                            DoiHinhChienThuat433UC.Visibility = Visibility.Collapsed;
                            DoiHinhChienThuat442UC.Visibility = Visibility.Collapsed;
                            DoiHinhChienThuat4231UC.Visibility = Visibility.Visible;
                            DoiHinhChienThuat4231UC.DataContext = teamBuilderViewModel.Teamformat;

                        }

                        //Gán đội hình cho biến phụ để giữ nguyên đội hình
                        mainteam = (teamBuilderViewModel.MainTeamPlayers);



                        //team
                        if (teamBuilderViewModel.Team.HINHANHHLV != null && teamBuilderViewModel.Team.HINHANHHLV.Length != 0)
                        {
                            HLVImage.ImageSource = LoadImage(teamBuilderViewModel.Team.HINHANHHLV);

                        }
                        HLVname.Text = teamBuilderViewModel.Team.HLV;
                       GTDH.Text = teamBuilderViewModel.Team.GIATRI.ToString();
                        DHChienThuatcbb.Text = teamBuilderViewModel.Team.SODOCHIENTHUAT;
                        id_doi = teamBuilderViewModel.Team.ID;
                        this.DataContext = teamBuilderViewModel;

                        PopUpCustom popUp = new PopUpCustom("Chưa có cầu thủ dự bị");
                        popUp.ShowDialog();
                    }
                }
                else
                {
                    if (teamBuilderViewModel.MainTeamPlayers.Count==0)
                    {
                        this.DataContext = new TeamBuilderViewModel();
                        PopUpCustom popUp = new PopUpCustom("Chưa có cầu thủ trong đội");
                        popUp.ShowDialog();
                    }
                    else
                    {
                        this.DataContext = new TeamBuilderViewModel();
                        PopUpCustom popUp = new PopUpCustom("Đội bóng chưa đủ 11 người");
                        popUp.ShowDialog();
                    }
                   
                }
               
            }
            catch (Exception)
            {
                this.DataContext = new TeamBuilderViewModel();
                PopUpCustom popUp = new PopUpCustom("Chưa có cầu thủ trong đội");
                popUp.ShowDialog();
                
            }
           
          

        }
        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        private void GTDH_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (teamBuilderViewModel.Team.GIATRI != null)
                {
                    
                   // DOIBONG temp = DataProvider.ins.DB.DOIBONGs.Where(p => p.ID == id_doi).FirstOrDefault();
                    DOIBONG temp = DataProvider.ins.DB.Database.SqlQuery<DOIBONG>("SELECT * FROM DOIBONG WHERE ID = @ID ", new SqlParameter("@ID", id_doi)).FirstOrDefault<DOIBONG>();


                    GTDH.Text = String.Format("${0:n0}", temp.GIATRI);

                }
            }
            catch (Exception)
            {

                return;
            }
         
        }

        private void addrole(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            CAUTHU ct = dataGrid.SelectedItem as CAUTHU;

            PopUpAddRolePosition pu;
            if (ct.VAITRO != "" && ct.VAITRO != null && (ct.VAITROAO == "" || ct.VAITROAO == null))
            {
                ct.VAITROAO = ct.VAITRO;
                 pu = new PopUpAddRolePosition(ct.VAITROAO);

            }
            else
            {
                if (ct.VAITROAO!="")
                {
                    pu = new PopUpAddRolePosition(ct.VAITROAO);

                }
                else
                {
                    pu = new PopUpAddRolePosition("");
                }
            }
            pu.ShowDialog();
            if (pu.ok==1)
            {
                ct.VAITROAO = pu.role;
                foreach (CAUTHU item in mainteam)
                {
                    if (item.ID == ct.ID)
                    {
                        item.VAITROAO = ct.VAITROAO;
                        break;
                    }
                }
            }
            if (pu.cancel==1)
            {
                return;
            }
           
        }

        private void SaveTeam(object sender, RoutedEventArgs e)
        {
            List<DOIHINHCHINH> removeteam = (from a in DataProvider.ins.DB.CAUTHUs
                                           join b in DataProvider.ins.DB.DOIHINHCHINHs on a.ID equals b.IDCAUTHU
                                           where b.IDDOIBONG == id_doi
                                           select (b)).ToList<DOIHINHCHINH>();
            DataProvider.ins.DB.DOIHINHCHINHs.RemoveRange(removeteam);
            foreach (CAUTHU item in mainteam)
            {
                if (changeteam.ContainsKey(item.ID))
                {
                    item.VITRIAO = changeteam[item.ID];
                }
            }
            foreach (CAUTHU item in mainteam) {
                DOIHINHCHINH dh = new DOIHINHCHINH() { IDDOIBONG = item.IDDOIBONG, IDCAUTHU = item.ID, VITRI = item.VITRIAO, VAITRO = item.VAITROAO };
                DataProvider.ins.DB.DOIHINHCHINHs.Add(dh);

            }

            ComboBoxItem select = (ComboBoxItem)DHChienThuatcbb.SelectedItem;

            DOIBONG db= DataProvider.ins.DB.DOIBONGs.Where(d => d.ID == id_doi).First();
            try
            {
                db.SODOCHIENTHUAT = select.Content.ToString();

            }
            catch (Exception)
            {

                return ;
            }

            DataProvider.ins.DB.SaveChanges();
            PopUpCustom popUp = new PopUpCustom("Thông báo", "Đã lưu đội hình",1);
            popUp.Show();

        }

        private void dtgDSCauThuDuBi_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dtgDSCauThuDuBi.UnselectAll();
        }

        private void bttPlayerInfo_MainTeam(object sender, RoutedEventArgs e)
        {
            CAUTHU player = dtgDSCauThu.SelectedItem as CAUTHU;
            PlayerProfile playerProfile = new PlayerProfile(player);
            playerProfile.ShowDialog();
        }

        private void bttPlayerInfo_Subteam(object sender, RoutedEventArgs e)
        {
            CAUTHU player = dtgDSCauThuDuBi.SelectedItem as CAUTHU;
            PlayerProfile playerProfile = new PlayerProfile(player);
            playerProfile.ShowDialog();
        }
    }
}