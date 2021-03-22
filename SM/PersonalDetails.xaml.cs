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
using ClassLibrary.DataLayer;
using ClassLibrary.Exception;
using ClassLibrary.MainClasses;

namespace SM
{
    /// <summary>
    /// Interaction logic for PersonalDetails.xaml
    /// </summary>
    public partial class PersonalDetails : Window
    {
        public PersonalDetails()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This would save the Personal details in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClassLibrary.MainClasses.PersonalDetails personalDetails = new ClassLibrary.MainClasses.PersonalDetails();
            personalDetails.address1 = txtAddress1.Text;
            personalDetails.address2 = txtaddress2.Text;
            personalDetails.address3 = txtaddress3.Text;
            personalDetails.adhaarNumber = txtAdharrNumber.Text;
            personalDetails.age = txtAge.Text;
            personalDetails.attendanceId = Int32.Parse(txtattendanceid.Text);
            personalDetails.emialaddress = txtEmailAddress.Text;
            personalDetails.employeeID = Int32.Parse(txtempid.Text);
            personalDetails.EmployeeName = txtempname.Text;
            personalDetails.passportNumer = txtPassPortNumber.Text;
            personalDetails.age = txtAge.Text;
            personalDetails.fatherName = txtFathersName.Text;
            personalDetails.phone1 = txtphone1.Text;
            personalDetails.phone2 = txtphone2.Text;
            personalDetails.phone3 = txtphone3.Text;
            personalDetails.panNumber = txtPannumber.Text;
            Datalayer dl = new Datalayer();
            Response res = dl.PersonalDetailsInTheDataBAse(personalDetails);
            if (res.success)
            {
                MessageBox.Show("Details Saved");
                txtAddress1.Text = "";
                txtaddress2.Text = "";
                 txtaddress3.Text = "";
                 txtAdharrNumber.Text = "";
                txtAge.Text = "";
                txtattendanceid.Text = "";
               txtEmailAddress.Text = "";
              txtempid.Text = "";
                 txtempname.Text = "";
                 txtPassPortNumber.Text = "";
                 txtAge.Text = "";
                 txtFathersName.Text = "";
                txtphone1.Text = "";
                 txtphone2.Text = "";
                txtphone3.Text = "";
                txtPannumber.Text = "";

            }
            else if (res.isException)
            {
                MessageBox.Show("Exception occured : " + res.exception);
            }


        }
    }
}
