using DevExpress.Data.Extensions;
using DevExpress.Xpf.Bars.Helpers;
using DevExpress.Xpf.Editors.Helpers;
using FootBallProject.Model;
using FootBallProject.Object;
using FootBallProject.Usercontrol;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace FootBallProject.ViewModel
{
    public class ConfigVongLoai2ViewModel : BaseViewModel
    {

        private static ConfigVongLoai2ViewModel _ins;
        public static ConfigVongLoai2ViewModel Instance
        {
            get { return _ins; }
            set { _ins = value; }
        }
        public ListofLeagueViewModel Ins;
        private ObservableCollection<RoundObject> listRoundObject = new ObservableCollection<RoundObject>();
        public ObservableCollection<RoundObject> ListRoundObjects
        {
            get => listRoundObject;
            set => listRoundObject = value;
        }
        private ObservableCollection<ROUND> listRound = new ObservableCollection<ROUND>();
        public ObservableCollection<ROUND> ListRound
        {
            get => listRound;
            set { listRound = value; OnPropertyChanged(); }
        }
        private ObservableCollection<FOOTBALLMATCH> listMatchs = new ObservableCollection<FOOTBALLMATCH>();
        public ObservableCollection<FOOTBALLMATCH> ListMatchs
        {
            get => listMatchs;
            set { listMatchs = value; OnPropertyChanged(); }
        }
        private bool _enable;
        public bool Enable
        {
            get => _enable;
            set { _enable = value; OnPropertyChanged(); }
        }
        private DateTime? _startTime;
        public DateTime? StartTime
        {
            get => _startTime;
            set { _startTime = value; OnPropertyChanged(); }
        }
        private DateTime? _endtime;
        public DateTime? EndTime
        {
            get => _endtime;
            set { _endtime = value; OnPropertyChanged(); }
        }
        public ICommand Complete { get; set; }
        public ICommand GoBack { get; set; }
        public ConfigVongLoai2ViewModel(ListofLeagueViewModel ins)
        {

            Instance = this;
            string name = "Vòng số ";
            Ins = ins;
            int k = 1;
            for (int i = 1; i <= Math.Log(ConfigVongLoai1ViewModel.Instance.numofTeam, 2); i++)
            {
                ROUND temp = new ROUND()
                {
                    TENVONGDAU = name + i.ToString(),
                    IDDISPLAY = i.ToString(),
                    SOLUONGDOI = ConfigVongLoai1ViewModel.Instance.numofTeam / k
                };
                //DataProvider.Instance.Database.ROUNDs.AddOrUpdate(temp);
                //DataProvider.Instance.Database.SaveChanges();
                for (int j = 0; j < ConfigVongLoai1ViewModel.Instance.numofTeam / (2 * k); j++)
                {
                    string namematch = "Trận số ";
                    FOOTBALLMATCH tempmatch = new FOOTBALLMATCH()
                    {
                        ROUND = temp,
                        TENTRANDAU = namematch + (j + 1).ToString()
                    };
                    listMatchs.Add(tempmatch);
                }
                listRound.Add(temp);
                k *= 2;
            }
            ListRound = listRound;
            ListMatchs = ListMatchs;
            foreach (var item in ListRound)
            {
                RoundObject temp = new RoundObject(item);
                ListRoundObjects.Add(temp);
            }
            Enable = false;
            Complete = new RelayCommand<object>((p) => { return true; }, (p) => { StartTime = CreateNewLeague.Instance.StartTime; EndTime = CreateNewLeague.Instance.EndTime; CompleteFuntion(); });
            GoBack = new RelayCommand<object>((p) => { return true; }, (p) => { ListofLeagueViewModel.Instance.ReturnConfig1(); });
        }
        private byte[] ConvertBitmaptoByteArray(BitmapImage bitmapImage)
        {
            if (bitmapImage == null)
            {
                return null;
            }

            byte[] data;
            BitmapEncoder encoder = new PngBitmapEncoder();
            var extension = Path.GetExtension( CreateNewLeague.Instance.LinkAvatar);
            switch (extension)
            {
                case ".png":
                    encoder = new PngBitmapEncoder(); break;
                case ".jpg": case ".jpeg": encoder = new JpegBitmapEncoder(); break;
                case ".bmp": encoder = new BmpBitmapEncoder(); break;
                default: break;
            }
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }
        public bool CheckWithLeague(RoundObject round, LEAGUE league)
        {
            DateTime starttime = league.NGAYBATDAU.TryConvertToDateTime();
            DateTime endtime = league.NGAYKETTHUC.TryConvertToDateTime();
            DateTime roundtime = round.StartTime.TryConvertToDateTime();
            if (DateTime.Compare(starttime, roundtime) <= 0 && DateTime.Compare(roundtime, endtime) <= 0)
            {
                return true;
            }
            return false;
        }
        public bool CheckRound()
        {
            foreach (var item in ListRoundObjects)
            {
                if (CheckWithLeague(item, CreateNewLeague.Instance.League) == false)
                {
                    return false;
                }
                else if (item == ListRoundObjects.First())
                {
                    continue;
                }
                else if (item == ListRoundObjects.Last())
                {
                    if (DateTime.Compare(item.StartTime.TryConvertToDateTime(), ListRoundObjects[ListRoundObjects.IndexOf(item) - 1].StartTime.TryConvertToDateTime()) < 0)
                        return false;
                    else return true;
                }
                else if (DateTime.Compare(item.StartTime.TryConvertToDateTime(), ListRoundObjects[ListRoundObjects.IndexOf(item) + 1].StartTime.TryConvertToDateTime()) > 0 ||
                    DateTime.Compare(item.StartTime.TryConvertToDateTime(), ListRoundObjects[ListRoundObjects.IndexOf(item) - 1].StartTime.TryConvertToDateTime()) < 0
                    ) return false;
            }
            return true;
        }
        public void CompleteFuntion()
        {
            if (CheckRound() == true)
            {
                Enable = false;
                CreateNewLeague.Instance.League.HINHANH = ConvertBitmaptoByteArray(CreateNewLeague.Instance.img);
                DataProvider.Instance.Database.LEAGUEs.Add(CreateNewLeague.Instance.League);
                DataProvider.Instance.Database.SaveChanges();
                foreach (var item in ConfigVongLoai1ViewModel.Instance.Teams)
                {
                    if (item.Selected == true)
                    {
                        TEAMOFLEAGUE temp = new TEAMOFLEAGUE()
                        {
                            IDDOIBONG = item.Team.ID,
                            IDGIAIDAU = CreateNewLeague.Instance.League.ID
                        };
                        DataProvider.Instance.Database.TEAMOFLEAGUEs.Add(temp);
                    }
                }
                DataProvider.Instance.Database.SaveChanges();
                foreach (var item in ListRoundObjects)
                {
                    item.CurrentRound.IDGIAIDAU = CreateNewLeague.Instance.League.ID;
                    item.CurrentRound.NGAYBATDAU = item.StartTime;
                    item.CurrentRound.TENVONGDAU = item.NameOfRound;
                    DataProvider.Instance.Database.ROUNDs.Add(item.CurrentRound);
                }
                DataProvider.Instance.Database.SaveChanges();
                foreach (var item in ListMatchs)
                {
                    DataProvider.Instance.Database.FOOTBALLMATCHes.Add(item);
                }
                DataProvider.Instance.Database.SaveChanges();
                //Success f = new Success();
                //f.Show();
                ListofLeagueViewModel.Instance.Leagues.Add(new LeagueCardOb(CreateNewLeague.Instance.League));
                ListofLeagueViewModel.Instance.GoSuccess();
            }
            else
            {
                Enable = true;
            }
        }
    }
}
