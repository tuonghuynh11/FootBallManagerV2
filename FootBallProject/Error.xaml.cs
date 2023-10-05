﻿using DevExpress.Xpf.Core.Native;
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
using System.Windows.Shapes;

namespace FootBallProject
{
    /// <summary>
    /// Interaction logic for Error.xaml
    /// </summary>
    public partial class Error : Window
    {
        public Error(string text)
        {
            InitializeComponent();
            cterror.Text = text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
