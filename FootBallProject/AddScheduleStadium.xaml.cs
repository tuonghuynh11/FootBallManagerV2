﻿using System;
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
using System.Windows.Shapes;

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for AddScheduleStadium.xaml
    /// </summary>
    public partial class AddScheduleStadium : Window
    {
        public AddScheduleStadium()
        {
            InitializeComponent();
        }

        private void Close_but_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
