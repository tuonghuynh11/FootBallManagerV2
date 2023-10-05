using DevExpress.Xpf.Charts;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for StatisticalChart.xaml
    /// </summary>
    public partial class StatisticalChart : UserControl
    {
        public StatisticalChart()
        {
            InitializeComponent();
        }
        private void chartControl1_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {

            if (e.SeriesPoint.Value <= 10)
            {
                e.DrawOptions.Color = Color.FromArgb(0xFF, 0x51, 0x89, 0x03);
            }

            else if ((e.SeriesPoint.Value > 10) & (e.SeriesPoint.Value <= 20))
            {
                e.DrawOptions.Color = Color.FromArgb(0xFF, 0xF9, 0xAA, 0x0F);
            }

            else if (e.SeriesPoint.Value > 20)
            {
                e.DrawOptions.Color = Color.FromArgb(0xFF, 0xC7, 0x39, 0x0C);
            }
        }
    }
}
