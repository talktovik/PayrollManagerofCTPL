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
using ClassLibrary.DataLayer;

namespace SM
{
    /// <summary>
    /// Interaction logic for BankDetails.xaml
    /// </summary>
    public partial class BankDetails : Window
    {
        
        
        public BankDetails()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EmployeeBankDetails bankDetails = new EmployeeBankDetails();
            bankDetails.employeeID = Int32.Parse(txtemployeeID.Text);
            bankDetails.bankName = txtBankName.Text;
            bankDetails.accountNumber = txtAccountNumber.Text;
            bankDetails.ifscCode = txtifscdetails.Text;
            bankDetails.nameInBank = TxtNameinBank.Text;
            Datalayer dl = new Datalayer();
            Response res = dl.addbankdetailstodatabase(bankDetails);
            if (res.success)
            {
                MessageBox.Show("Details Saved");
                txtAccountNumber.Text = "";
                txtBankName.Text = "";
                txtemployeeID.Text = "";
                txtifscdetails.Text = "";
                TxtNameinBank.Text = "";
                TxtPanNumber.Text = "";
                
            }
            else if (res.isException)
            {
                MessageBox.Show("Exception occured : " + res.exception);
            }
        }

       
    }
}
