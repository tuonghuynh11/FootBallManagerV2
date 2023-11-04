using FootBallProject.Model;
using FootBallProject.Service;
using FootBallProject.ViewModel;
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
    /// Interaction logic for SupplierInformation.xaml
    /// </summary>
    public partial class SupplierInformation : Window
    {
        public SUPPLIER supplierInfo { get; set; }
        public List<SERVICE> _supplierServices =new List<SERVICE>();
        public SupplierInformation()
        {
            InitializeComponent();
            this.DataContext = this;

        }

        public  SupplierInformation(int idSupplier)
        {
            InitializeComponent();

            (this.DataContext as SupplierInformationVIewModel).LoadData(idSupplier);
        }

       
    }
}
