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
using ClassLibrary.MainClasses;
using ClassLibrary.Database;
using ClassLibrary.Exception;
using ClassLibrary.DataLayer;

namespace SM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = e.Source as Button;
            if (txtattendanceid.Text == "" && txtemployeeId.Text == "" && txtempname.Text == "" && txtattendanceid.Text == "")
            {
                MessageBox.Show("Enter Each Details");
            }
            else
            {
                Employee emp = new Employee();
                emp.employeeName = txtempname.Text;
                emp.employeeidGivenByCompany = Int32.Parse(txtemployeeId.Text);
                emp.designation = txtDesignation.Text;
                emp.attandanceID = Int32.Parse(txtattendanceid.Text);
                emp.joiningDate = (DateTime)datepicker.SelectedDate;
                Datalayer dl = new Datalayer();
                Response res = dl.addEmpToDatabase(emp);
                if (res.success)
                {
                    MessageBox.Show("Employee Created. Add more data");
                    txtattendanceid.Text = "";
                    txtDesignation.Text = "";
                    //txtemployeeId.Text = "";
                    txtempname.Text = "";
                }
                else if (res.isException)
                {
                    MessageBox.Show("Exception occured : " + res.exception);
                }
            }
        }


        //this will trigger the bank details info fill form
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BankDetails bankDetailswindow = new BankDetails();
            bankDetailswindow.Show();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PersonalDetails personalDetailsWindow = new PersonalDetails();
            personalDetailsWindow.Show();
        }
    }
}
