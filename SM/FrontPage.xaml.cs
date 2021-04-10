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

namespace SM
{
    /// <summary>
    /// Interaction logic for FrontPage.xaml
    /// </summary>
    public partial class FrontPage : Window
    {
        public FrontPage()
        {
            InitializeComponent();
        }


        /// <summary>
        /// For adding new Employees
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }


        /// <summary>
        /// Genearating the Salaries for the employees.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GenerateSalary generateSalary = new GenerateSalary();
            generateSalary.Show();
        }
    }
}
