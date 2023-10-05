using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for TeamsPlayers.xaml
    /// </summary>
    public partial class TeamsPlayers : Window
    {
        public List<Player> players;
        public TeamsPlayers()
        {
            InitializeComponent();
            //players = new List<Player>();
            //for(int i = 0; i < 10; i++)
            //    players.Add(new Player("MABUU","MCI", 9, "Erling Haaland", 22, 195, 120, "Norway", "Forward", 6, 182, 1, "Healthy"));
            //players.Add(new Player("TribalChief", "WWE", 11, "Roman Reigns", 37, 191, 120, "USA", "Leader", 0, 0, 0, "Healthy"));
            //Players_List.ItemsSource = players;
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void TextBlock_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void Players_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int x = Players_List.SelectedIndex;
            //object row = Players_List.SelectedItem;
            //string Name = (Players_List.SelectedCells[2].Column.GetCellContent(row) as TextBlock).Text;
            //MessageBox.Show(Name);
            PlayerProfile pp = new PlayerProfile();
            pp.HorizontalAlignment = HorizontalAlignment.Right;
            
            //pp.DataContext = players[x];
            pp.Show();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditPlayerForm edit = new EditPlayerForm();

            edit.Show();
        }
    }
    public class Player
    {
        string id;
        string clubID;
        int number;
        string name;
        int age;
        int height;
        int weight;
        int leaguesNum;
        int goals;
        int foot;
        string physique;
        string nationality;
        string position;
        public Player(string id = "", string clubID = " ", int number = 0, string name = "", int age = 0, int height = 0, int weight = 0, 
            string nationality = "", string position = "", int leagesNum = 0, int goals = 0, int foot = 0, string physique = "Healthy")
        {
            this.id = id;
            this.clubID = clubID;
            this.number = number;
            this.name = name;
            this.age = age;
            this.height = height;
            this.weight = weight;
            this.leaguesNum = leagesNum;
            this.goals = goals;
            this.foot = foot;
            this.physique = physique;
            this.nationality = nationality;
            this.position = position;
            
            
        }
        public string Id { get { return id; } set { id = value; } }

        public int Number { get { return number; } set { number = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int Age { get { return age; } set { age = value; } }

        public string Nationality { get { return nationality; } set { nationality = value; } }
        public string Position { get { return position; } set { position = value; } }
        public int Height { get { return height; } set { height = value; } }
        public int Weight { get { return weight; } set { weight = value; } }
        public int LeaguesNum { get { return leaguesNum; } set { leaguesNum = value; } }
        public int Goals { get { return goals; } set { goals = value; } }
        public int Foot { get { return foot; } set { foot = value; } }
        public string Physique { get { return physique; } set { physique = value; } }


    }
}
