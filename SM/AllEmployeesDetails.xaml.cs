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
using ClassLibrary.MainClasses;
using ClassLibrary.Database;
using ClassLibrary.DataLayer;
using ClassLibrary.Exception;
using System.Diagnostics;

namespace SM
{
    /// <summary>
    /// Interaction logic for AllEmployeesDetails.xaml
    /// </summary>
    public partial class AllEmployeesDetails : Window
    {
        public AllEmployeesDetails()
        {
            InitializeComponent();
            populatetheGrid();
        }

        private void populatetheGrid()
        {
           
        }
    }
}
